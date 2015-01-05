using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;


namespace SEImplementation.Controllers
{
    public class OrderController : Controller
    {

        public ActionResult PayNow()
        {
            User x = new UserBL().GetUserByUsername(User.Identity.Name);
            List<ShoppingCart> c = new CartBL().GetShoppingCartOfUser(x.UserID).ToList();

            string urlRedirect = "https://www.paypal.com/cgi-bin/webscr?";
            string itemName = "";
            Product p;
            foreach (ShoppingCart cx in c)
            {
                p = new ProductBL().GetProductById(cx.ProductID);
                itemName = itemName + "&cmd=_cart&business=derrickagius92@gmail.com&currency_code=EUR" + "&item_name=" + p.ProductName + "&amount=" + p.ProductPrice + "&add=1" + "&item_number=" + p.ProductID + "&quantity=" + cx.Qty; 
                new CartBL().DeleteCartItem(cx.ShoppingCartID);
            }
            string fullURL = urlRedirect + itemName;
            return Redirect(fullURL);
        }

    }
}
