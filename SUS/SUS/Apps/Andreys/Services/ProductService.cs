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
        public int AddProduct(AddProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Category = Enum.Parse<Category>(model.Category),
                Gender = Enum.Parse<Gender>(model.Gender),
            };
            db.Products.Add(product);
            db.SaveChanges();

            return product.Id;
        }

        public Product GetProduct(int id)
        {
            return db.Products.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<ProductsViewModel> GetProducts() 
        {
            return db.Products.Select(x => new ProductsViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price

            }).ToList();
        }

        public void RemoveProduct(int Id)
        {
            var product = db.Products.FirstOrDefault(x=> x.Id == Id);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}
