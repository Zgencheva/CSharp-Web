namespace VisitACity.Web.Areas.Administration.Controllers
{
    using VisitACity.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
