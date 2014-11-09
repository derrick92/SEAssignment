using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess.Repos;

namespace BusinessLayer
{
    public class ProductBL
    {
        public void CreateProduct(Product p)
        {
            new ProductRepo().CreateProduct(p);
        }

        public IEnumerable<Product> RecentProductList()
        {
            return new ProductRepo().GetAllProductsSortByDateTake10();
        }

        public IEnumerable<Product> AllProducts()
        {
            return new ProductRepo().GetAllProductsBySortByName();
        }
    }
}
