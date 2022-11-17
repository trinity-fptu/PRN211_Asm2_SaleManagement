using PRN211_Asm2_Salemanagement_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.DAOs;

namespace PRN211_Asm2_Salemanagement_Library.Repos.ProductRepo
{
    public class ProductRepo : IProductRepo
    {
        public bool AddProduct(Product product) => ProductDAO.Instance.AddProduct(product);

        public bool CheckIdDuplicated(int id) => ProductDAO.Instance.CheckIdDuplicated(id);

        public bool DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);

        public IEnumerable<Product> GetAllProducts() => ProductDAO.Instance.GetAllProducts();

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

        public Product GetProductByName(string name) => ProductDAO.Instance.GetProductByName(name);

        public IEnumerable<Product> SearchProductById(int id) => ProductDAO.Instance.SearchProductById(id);

        public IEnumerable<Product> SearchProductByName(string name) => ProductDAO.Instance.SearchProductByName(name);

        public IEnumerable<Product> SearchProductByPriceRange(int min, int max) =>
            ProductDAO.Instance.SearchProductByPriceRange(min, max);

        public IEnumerable<Product> SearchProductByUnitInStockRange(int min, int max) =>
            ProductDAO.Instance.SearchProductByUnitInStockRange(min, max);

        public bool UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);
    }
}
