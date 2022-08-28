using Andreys.Data;
using Andreys.Data.Enums;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andreys.Services
{
    public class ProductService : IProductService
    {
        private readonly AndreysDbContext db;

        public ProductService(AndreysDbContext db)
        {
            this.db = db;
        }
        public void AddProduct(AddProductModel model)
        {
            db.Products.Add(new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Category = Enum.Parse<Category>(model.Category),
                Gender = Enum.Parse<Gender>(model.Gender),
            });
            db.SaveChanges();
        }
    }
}
