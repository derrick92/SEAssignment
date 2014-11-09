using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess.Repos
{
    public class ProductRepo:ConnectionClass
    {
        public ProductRepo()
            : base()
        {

        }

        public void CreateProduct(Product entry)
        {
            Entity.AddToProducts(entry);
            Entity.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return Entity.Products.SingleOrDefault(u => u.ProductID == id);
        }

        public IEnumerable<Product> GetAllProductsSortByDateTake10()
        {
            return (
                from p in Entity.Products
                orderby p.DateAdded descending
                select p
                ).AsEnumerable().Take(10);
        }

        public IEnumerable<Product> GetAllProductsBySortByName()
        {
            return (
                from p in Entity.Products
                orderby p.ProductName ascending
                select p
                ).AsEnumerable();
        }

        public IEnumerable<Product> GetProductsByCreator(int userid)
        {
            return (
                from p in Entity.Products
                where p.CreatedBy == userid
                select p
                ).AsEnumerable();
        }

        public void UpdateProduct(Product gb)
        {
            Entity.Products.Attach(GetProductById(gb.ProductID)); //gets current values
            Entity.Products.ApplyCurrentValues(gb); //over write with the new values
            Entity.SaveChanges(); //update the changes
        }

        public void DeleteProduct(int productid)
        {
            Entity.DeleteObject(GetProductById(productid)); //applies the changes
            Entity.SaveChanges();
        }

    }
}
