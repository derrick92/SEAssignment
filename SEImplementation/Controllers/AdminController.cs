using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;
using SEImplementation.Models;
using System.Web.Security;

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

        public ActionResult Delete(int userid)
        {
            try
            {
                new UserBL().DeleteUserDropAllRoles(userid);
                return Redirect("~/admin/userlist?deleted=successful");
            }
            catch
            {
                return Redirect("~/admin/userlist?deleted=unsuccessful");
            }
        }

        public ActionResult Edit(int userid)
        {
            RegisterModel rm = new RegisterModel();
            User u = new UserBL().GetUserByID(userid);
            rm.UserID = u.UserID;
            rm.UserName = u.Username;
            rm.FirstName = u.FirstName;
            rm.Surname = u.Surname;
            rm.Password = "";
            rm.Email = u.Email;
            rm.MobileNum = u.MobileNum;



            return View(rm);
        }

        [HttpPost]
        public ActionResult Edit(RegisterModel rm)
        {
            try
            {
                User u = new UserBL().GetUserByID(rm.UserID);
                if ((new UserBL().DoesUserNameExist(rm.UserName)) && (u.Username != rm.UserName))
                { return Redirect("/admin/userlist?errormsg=usernametaken"); }
                else
                {
                    u = new User();
                    u.UserID = rm.UserID;
                    u.Username = rm.UserName;
                    u.FirstName = rm.FirstName;
                    u.Surname = rm.Surname;
                    u.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(rm.Password, "MD5");
                    u.Email = rm.Email;
                    u.MobileNum = rm.MobileNum;
                    new UserBL().Update(u);
                    return RedirectToAction("userlist", "admin");
                }
            }
            catch
            {
                return Redirect("/admin/userlist?errormsg=nochangessaved");
            }
        }

    }
}
