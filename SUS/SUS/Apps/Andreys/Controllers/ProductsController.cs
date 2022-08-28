using Andreys.Services;
using Andreys.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService service;

        public ProductsController(IProductService service)
        {
            this.service = service;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }
        [HttpPost]
        public HttpResponse Add(AddProductModel model)
        {
            Console.WriteLine(model.Name);
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }
            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Name should be between 4 and 20 characters long.");
            }

            //if (string.IsNullOrWhiteSpace(model.ImageUrl))
            //{
            //    return this.Error("The image is required!");
            //}

            //if (string.IsNullOrEmpty(model.ImageUrl) && !Uri.TryCreate(model.ImageUrl, UriKind.Absolute, out _))
            //{
            //    return this.Error("Image url should be valid.");
            //}

            if (model.Description != null && model.Description.Length >10)
            {
                return this.Error("Description should be maximum 10 characters long.");
            }
            this.service.AddProduct(model);
            return this.Redirect("/");
        }
    }
}
