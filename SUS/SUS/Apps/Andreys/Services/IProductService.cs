using Andreys.Data;
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
        int AddProduct(AddProductModel model);

        public ICollection<ProductsViewModel> GetProducts();

        public void RemoveProduct(int Id);
        public Product GetProduct(int id);
    }
}
