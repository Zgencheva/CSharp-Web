using Suls.Sevices;
using Suls.ViewModels.Submissions;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService service;

        public SubmissionsController(ISubmissionService service)
        {
            this.service = service;
        }
        public HttpResponse Create(string Id)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            SubmissionViewModel model = service.CreateSubmission(Id);
            return this.View(model);
        }
        [HttpPost]
        public HttpResponse Create(CreateSubmissionInputModel model)
        {
            Console.WriteLine(model.Code);
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            if (string.IsNullOrEmpty(model.Code) || model.Code.Length < 30 || model.Code.Length > 800)
            {
                return this.Error("Code should be between 30 and 800 characters long.");
            }

            var userId = this.GetUserId();
            service.AddSubmissionToProblem(model, userId);
            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            service.DeleteSubmission(id);
            return this.Redirect("/");
        }
    }
}
