using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;


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
                User u = new UserBL().GetUserByUsername(User.Identity.Name);
                if (new RoleBL().IsInRole(u.UserID, 1))
                {
                    return seller + "<li> <a href=\"/admin\">Admin Tools</a></li>";
                }
                //else if seller
                else if (new RoleBL().IsInRole(u.UserID, 4))
                {
                    return seller;
                }
                //else if buyer
                else if (new RoleBL().IsInRole(u.UserID, 2))
                {
                    return buyer;
                }
            }
            //if anoymous
            else
            {
                return allUser;
            }

            return allUser;
        }
    }
}
