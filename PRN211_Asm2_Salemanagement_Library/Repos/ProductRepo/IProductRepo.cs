using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.Models;

namespace PRN211_Asm2_Salemanagement_Library.Repos.ProductRepo
{
    public interface IProductRepo
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProductById(int id);
        public Product GetProductByName(string name);
        public bool AddProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int id);
        public IEnumerable<Product> SearchProductByName(string name);
        public IEnumerable<Product> SearchProductById(int id);
        public IEnumerable<Product> SearchProductByPriceRange(int min, int max);
        public IEnumerable<Product> SearchProductByUnitInStockRange(int min, int max);

        public bool CheckIdDuplicated(int id);
    }
}
