using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;

namespace SEImplementation.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (User.Identity.Name != string.Empty)
            {
                User u = new UserBL().GetUserByUsername(User.Identity.Name);
                if (new RoleBL().IsInRole(u.UserID, 1))
                {
                    return View();
                }
            }

            return Redirect("~/?access=noaccess");
        }

        public ActionResult UserList()
        {
            if (User.Identity.Name != string.Empty)
            {
                User u = new UserBL().GetUserByUsername(User.Identity.Name);
                if (new RoleBL().IsInRole(u.UserID, 1))
                {
                    List<User> users = new UserBL().GetAllUsers().ToList();
                    return View("userlist", users);
                }
            }

            return Redirect("~/?access=noAccess");
        }

        public ActionResult Delete(int userID)
        {
            try
            {
                new UserBL().DeleteUserDropAllRoles(userID);
                return Redirect("~/admin/userlist?deleted=successful");
            }
            catch
            {
                return Redirect("~/admin/userlist?deleted=unsuccessful");
            }
        }



    }
}
