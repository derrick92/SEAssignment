﻿using System;
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

        public Product GetProductById(int id)
        {
            return new ProductRepo().GetProductById(id);
        }


        public IEnumerable<Product> RecentProductList()
        {
            return new ProductRepo().GetAllProductsSortByDateTake10();
        }

        public IEnumerable<Product> AllProducts()
        {
            return new ProductRepo().GetAllProductsBySortByName();
        }


        public IEnumerable<Product> GetProductsByCreator(int userid)
        {
            return new ProductRepo().GetProductsByCreator(userid);
        }


        public void DeleteProduct(int productid)
        {
            new ProductRepo().DeleteProduct(productid);
        }

        public void Update(Product gb)
        {
            new ProductRepo().UpdateProduct(gb);
        }


    }
}
