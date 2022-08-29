using Andreys.Services;
using Andreys.ViewModels.Home;
using Andreys.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService service;
        private readonly ErrorViewModel errorModel;

        public ProductsController(IProductService service, 
            ErrorViewModel errorModel)
        {
            this.service = service;
            this.errorModel = errorModel;
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
            
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }
            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 4 || model.Name.Length > 20)
            {
                this.errorModel.Error="Name should be between 4 and 20 characters long!!!";
                return this.View(errorModel, "Error");
            }
            if (!string.IsNullOrEmpty(model.ImageUrl) && !Uri.TryCreate(model.ImageUrl, UriKind.Absolute, out _))
            {
                this.errorModel.Error = "Image url should be valid.";
                return this.View(errorModel, "Error");
            }

            if (model.Description != null && model.Description.Length >10)
            {
                this.errorModel.Error = "Description should be maximum 10 characters long.";
                return this.View(errorModel, "Error");
            }
            this.service.AddProduct(model);
            return this.Redirect("/");
        }

        public HttpResponse Details(int Id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var model = service.GetProductDetails(Id);
            return this.View(model);
        }
        public HttpResponse Delete(int Id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            service.RemoveProduct(Id);
            return this.Redirect("/");
        }
    }
}
