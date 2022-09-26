using Microsoft.AspNetCore.Mvc;
using MVC_Demo.Models;
using System.Text;
using System.Text.Json;

namespace MVC_Demo.Controllers
{
    public class ProductsController : Controller
    {
        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Cheese",
                Price = 7.00,
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Ham",
                Price = 5.50,
            },
            new ProductViewModel()
            {
                Id = 3,
                Name = "Bread",
                Price = 1.50,
            }
        };
        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var foundProducts = this.products
                    .Where(p => p.Name.ToLower()
                    .Contains(keyword.ToLower()));
                return View(foundProducts);
            }
            return View(this.products);
        }
        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return Json(this.products, options);
        }
        public IActionResult AllAsText()
        {
            var sb = new StringBuilder();
            foreach (var product in this.products)
            { 
                    sb.AppendLine($"Product {product.Id}: {product.Name} - {product.Price}");
            }
            return Content(sb.ToString());
        }
        public IActionResult AllAsTextFile()
        {
            var sb = new StringBuilder();
            foreach (var product in this.products)
            {
                sb.AppendLine($"Product {product.Id}: {product.Name} - {product.Price}");
            }
            return Content(sb.ToString());
        }
        public IActionResult ById(int id)
        {
            var product = this.products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }
    }
}
