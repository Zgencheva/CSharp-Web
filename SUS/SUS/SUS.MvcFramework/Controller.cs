
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
        public HttpResponse View(
            object viewModel = null,
            [CallerMemberName]string ViewPath = null)
        {


            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");

            var viewContent = System.IO.File.ReadAllText("Views/" + 
                this.GetType().Name.Replace("Controller", string.Empty).TrimEnd() + 
                "/" +
                ViewPath + 
                ".cshtml");
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
