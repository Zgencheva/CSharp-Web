
using SUS.HTTP;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View([CallerMemberName]string ViewPath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.html");

            var viewContent = System.IO.File.ReadAllText("Views/" + 
                this.GetType().Name.Replace("Controller", string.Empty).TrimEnd() + 
                "/" +
                ViewPath + 
                ".html");
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
    }
}
