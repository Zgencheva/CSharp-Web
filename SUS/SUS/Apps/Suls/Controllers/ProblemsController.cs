using Suls.Sevices;
using Suls.ViewModels;
using Suls.ViewModels.Problems;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService service;

        public ProblemsController(IProblemService service)
        {
            this.service = service;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }
        [HttpPost]
        public HttpResponse Create(AddProblemModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (model.Points < 50 || model.Points >300)
            {
                return this.Error("Points must be in range 50 and 300");
            }
            if (model.Name.Length < 5 || model.Name.Length > 20)
            {
                return this.Error("Problem name should be between 5 and 20 symbols");
            }
            this.service.CreateProblem(model);
            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            ProblemViewModel model = service.GetProblemDetils(id);

            return this.View(model);
        }
    }
}
