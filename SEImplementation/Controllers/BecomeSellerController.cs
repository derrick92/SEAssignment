using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;

namespace SEImplementation.Controllers
{
    public class BecomeSellerController : Controller
    {
        //
        // GET: /BecomeSeller/

        public ActionResult Index()
        {

            if (User.Identity.Name != string.Empty)
            {
                User u = new UserBL().GetUserByUsername(User.Identity.Name);
                if (!new RoleBL().IsInRole(u.UserID, 4))
                {
                    new UserBL().BecomeSeller(u.Username);
                    return Redirect("~/?access=successfully");
                }
                else
                {
                    return Redirect("~/?access=alreadyaseller");
                }
            }

            return Redirect("~/?errormsg=notlogged");
        }

    }
}
