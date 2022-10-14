namespace VisitACity.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;

    [Authorize]
    public class BaseController : Controller
    {
        public string UserFirstName
        {
            get
            {
                string firstName = string.Empty;
                if (this.User != null && this.User.HasClaim(c => c.Type == "firstName"))
                {
                    firstName = this.User.Claims
                        .FirstOrDefault(c => c.Type == "firstName").Value;
                }

                return firstName;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.ViewBag.UserFirstName = this.UserFirstName;
            base.OnActionExecuted(context);
        }
    }
}
