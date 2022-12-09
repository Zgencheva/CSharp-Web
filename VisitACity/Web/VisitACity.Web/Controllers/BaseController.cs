namespace VisitACity.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using VisitACity.Common;

    [Authorize]
    public abstract class BaseController : Controller
    {
        public string UserFirstName
        {
            get
            {
                string firstName = string.Empty;
                if (this.User != null && this.User.HasClaim(c => c.Type == ClaimTypeConstants.FirstName))
                {
                    firstName = this.User.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypeConstants.FirstName).Value;
                }

                return firstName;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.ViewBag.UserFirstName = this.UserFirstName;
            }

            base.OnActionExecuted(context);
        }
    }
}
