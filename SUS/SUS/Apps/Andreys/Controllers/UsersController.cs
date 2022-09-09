using Andreys.Services;
using Andreys.ViewModels;
using Andreys.ViewModels.Home;
using Andreys.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ErrorViewModel errorModel;

        public UsersController(IUsersService usersService, ErrorViewModel errorModel)
        {
            this.usersService = usersService;
            this.errorModel = errorModel;
        }

        // GET /users/login
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var userId = this.usersService.GetUserId(model.Username, model.Password);
            if (userId == null)
            {
                this.errorModel.Error = "Invalid username or password";
                return this.View(errorModel, "Error");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        // GET /users/register
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (input.Username == null || input.Username.Length < 4 || input.Username.Length > 10)
            {
                this.errorModel.Error = "Invalid username. The username should be between 4 and 10 characters.";
                return this.View(errorModel, "Error");
            }

            if (!Regex.IsMatch(input.Username, @"^[a-zA-Z0-9\.]+$"))
            {
                this.errorModel.Error = "Invalid username. Only alphanumeric characters are allowed.";
                return this.View(errorModel, "Error");
            }

            if (string.IsNullOrWhiteSpace(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                this.errorModel.Error = "Invalid email.";
                return this.View(errorModel, "Error");
            }

            if (input.Password == null || input.Password.Length < 6 || input.Password.Length > 20)
            {
                this.errorModel.Error = "Invalid password. The password should be between 6 and 20 characters.";
                return this.View(errorModel, "Error");
            }

            if (input.Password != input.ConfirmPassword)
            {
                this.errorModel.Error = "Passwords should match.";
                return this.View(errorModel, "Error");
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                this.errorModel.Error = "Username already taken.";
                return this.View(errorModel, "Error");
            }

            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                this.errorModel.Error = "Email already taken.";
                return this.View(errorModel, "Error");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                this.errorModel.Error = "Only logged-in users can logout.";
                return this.View(errorModel, "Error");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
