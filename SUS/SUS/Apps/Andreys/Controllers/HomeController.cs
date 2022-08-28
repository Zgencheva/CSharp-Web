using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}
