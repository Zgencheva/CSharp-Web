using SUS.HTTP;
using SUS.MvcFramework.ViewEngine;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        private SusViewEngine viewEngine;
        public Controller()
        {
            this.viewEngine = new SusViewEngine();
        }
        public HttpRequest Request { get; set; }
        public HttpResponse View(
            object viewModel = null,
            [CallerMemberName] string ViewPath = null)
        {


            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");
            //layout = layout.Replace("@RenderBody", "____View_Goes_Here____");
            //layout = this.viewEngine.GetHtml(layout, viewModel);

            var viewContent = System.IO.File.ReadAllText("Views/" +
                this.GetType().Name.Replace("Controller", string.Empty).TrimEnd() +
                "/" +
                ViewPath +
                ".cshtml");
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel);
            var resposeHtml = layout.Replace("@RenderBody", viewContent);
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

        public HttpResponse File(string filePath, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileBytes);
            return response;
        }

        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("location", url));
            return response;
        }
    }
}