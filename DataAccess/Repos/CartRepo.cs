using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess.Repos
{
    public class CartRepo : ConnectionClass
    {
        public CartRepo() : base() { }

        public void AddToCart(ShoppingCart cart)
        {

            if (Entity.ShoppingCarts.Count(user => user.UserID == cart.UserID && user.ProductID == cart.ProductID) == 0)
            {
                Entity.ShoppingCarts.AddObject(cart);
                Entity.SaveChanges();
            }
            else
            {
                int newQty = cart.Qty;
                ShoppingCart sc = GetShoppingCart(cart.UserID, cart.ProductID);
                sc.Qty += newQty;
                Entity.SaveChanges();
            }
        }

        public IEnumerable<ShoppingCart> GetShoppingCartOfUser(int userID)
        {
            return (
                from c in Entity.ShoppingCarts
                join p in Entity.Products
                on c.ProductID equals p.ProductID
                join u in Entity.Users
                on c.UserID equals u.UserID
                where c.UserID == userID
                select c
                ).AsEnumerable();
        }

        public ShoppingCart GetShoppingCart(int userid, int productid)
        {
            return Entity.ShoppingCarts.SingleOrDefault(x => x.UserID == userid && x.ProductID == productid);
        }

        public ShoppingCart GetShoppingCartbyID(int cartID)
        {
            return Entity.ShoppingCarts.SingleOrDefault(x => x.ShoppingCartID == cartID);
        }

        public void UpdateCart(int userid, int productId, int newQty)
        {
            ShoppingCart sc = GetShoppingCart(userid, productId);
            sc.Qty += newQty;
            Entity.SaveChanges();
        }

        public void UpdateCartActualValue(int userid, int productId, int newQty)
        {
            ShoppingCart sc = GetShoppingCart(userid, productId);
            sc.Qty = newQty;
            Entity.SaveChanges();
        }

        public int GetNoOfItemsInCart(int userid)
        {
            // return Entity.ShoppingCarts.Count(x => x.Username_FK == username); //returns the rows only
            if (new UserRepo().GetUserByID(userid).ShoppingCarts.Count > 0)
            {
                return (from sc in Entity.ShoppingCarts
                        where sc.UserID == userid
                        select sc.Qty).Sum();
            }
            else { return 0; }
        }

        public void DeleteCartItem(int id)
        {
            Entity.DeleteObject(GetShoppingCartbyID(id)); //applies the changes
            Entity.SaveChanges();
        }

        public void Update(ShoppingCart gb)
        {
            Entity.ShoppingCarts.Attach(GetShoppingCartbyID(gb.ShoppingCartID)); //gets current values
            Entity.ShoppingCarts.ApplyCurrentValues(gb); //over write with the new values
            Entity.SaveChanges(); //update the changes
        }

    }
}
