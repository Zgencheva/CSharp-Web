using Andreys.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }
        
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                var products = productService.GetProducts();
                return this.View(products, "Home");
            }
            return this.View();
        }
    }
}
