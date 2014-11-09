using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;
using SEImplementation.Classes;


namespace SEImplementation.Controllers
{
    public class MenuController : Controller
    {
        public string MenuMenu()
        {

            string allUser = "<li> <a href=\"/\">Home</a></li>" +
                    "<li> <a href=\"/product\">Browse Items</a></li>";

            string seller = allUser + "<li> <a href=\"/ProductManage\">Product Management</a></li>";
            string buyer = allUser + "<li> <a href=\"/becomeseller\">Become a Seller</a></li>";


            //if admin
            if (User.Identity.Name != string.Empty)
            {
                if (new RoleChecker().checkIfAdmin(User.Identity.Name))
                {
                    return seller + "<li> <a href=\"/admin\">Admin Tools</a></li>";
                }
                //else if seller
                else if (new RoleChecker().checkIfSeller(User.Identity.Name))
                {
                    return seller;
                }
                //else if buyer
                else if (new RoleChecker().checkIfBuyer(User.Identity.Name))
                {
                    return buyer;
                }
            }
            //if anoymous or no roles
            else
            {
                return allUser;
            }

            return allUser;
        }
    }
}
