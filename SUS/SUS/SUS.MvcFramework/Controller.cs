
using SUS.HTTP;
using System.IO;
using System.Text;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View(string ViewPath)
        {
            var resposeHtml = File.ReadAllText(ViewPath);
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
