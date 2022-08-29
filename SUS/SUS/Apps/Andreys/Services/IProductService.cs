using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andreys.Services
{
    public interface IProductService
    {
        void AddProduct(AddProductModel model);

        public ICollection<ProductsViewModel> GetProducts();

        public void RemoveProduct(int Id);
        public ProductDetailsViewModel GetProductDetails(int id);
    }
}
