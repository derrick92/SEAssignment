using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;
using SEImplementation.Models;


namespace SEImplementation.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index(string error)
        {
            if (error == "stock")
            {
                ViewBag.Message = "<div class=\"alert alert-danger bs-alert-old-docs\"><strong>Not enough stock for one or more products.</strong></div>";
            }
            User x = new UserBL().GetUserByUsername(User.Identity.Name);
            List<ShoppingCart> c = new CartBL().GetShoppingCartOfUser(x.UserID).ToList();
            return View(c);
        }

        public ActionResult AddtoCart(string username, int prodID, int qty)
        {
            User x = new UserBL().GetUserByUsername(username);
            ShoppingCart y = new ShoppingCart();
            y.UserID = x.UserID;
            y.ProductID = prodID;
            y.Qty = qty;

            new CartBL().AddToCart(y);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int cartID)
        {
            new CartBL().DeleteCartItem(cartID);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int cartID)
        {
            //get the entry by ID .. e.g. get entry number 5
            ShoppingCart e = new CartBL().GetShoppingCartbyID(cartID);
            //update the entry with the models details
            
            CartModel model = new CartModel();
            model.myCart = e;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int cartID, CartModel cm)
        {
            new CartBL().Update(cm.myCart);
            return RedirectToAction("Index");
        }


        public ActionResult Checkout()
        {
            User x = new UserBL().GetUserByUsername(User.Identity.Name);
            List<ShoppingCart> c = new CartBL().GetShoppingCartOfUser(x.UserID).ToList();
            return View(c);
        }

        public string NoOfCartItems()
        {
            User x = new UserBL().GetUserByUsername(User.Identity.Name);

            try
            {
                int c = new CartBL().GetNoOfItemsInCart(x.UserID);

                if (c == 0)
                {
                    return "No Products";
                }
                else if (c == 1)
                {
                    return "1 Product";
                }
                else
                {
                    return c + " Products";
                }
            }
            catch
            {
                return "Login to View cart";
            }
        }

        public string totalCost()
        {
            User x = new UserBL().GetUserByUsername(User.Identity.Name);

            try
            {
                List<ShoppingCart> c = new CartBL().GetShoppingCartOfUser(x.UserID).ToList();

                decimal totalcost = 0;
                foreach (ShoppingCart sc in c)
                {
                    totalcost = totalcost + (sc.Qty * sc.Product.ProductPrice);
                }

                if (totalcost == 0)
                {
                    return "";
                }
                else
                {
                    return "&euro;" + totalcost;
                }
            }
            catch
            {
                return "";
            }
        }

    }
}
