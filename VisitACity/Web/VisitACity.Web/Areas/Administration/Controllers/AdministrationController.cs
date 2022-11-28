namespace VisitACity.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Common;
    using VisitACity.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        protected bool IsExtensionValid(string extension)
        {
            return extension == GlobalConstants.JpegFormat || extension == GlobalConstants.PngFormat || extension == GlobalConstants.JpgFormat;
        }
    }
}
