using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;
using SEImplementation.Models;
using System.Web.Security;
using SEImplementation.Classes;

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
                if (new RoleChecker().checkIfAdmin(User.Identity.Name))
                {
                    return View();
                }
            }

            return Redirect("~/?msg=noaccess");
        }

        public ActionResult UserList()
        {
            if (User.Identity.Name != string.Empty)
            {
                if (new RoleChecker().checkIfAdmin(User.Identity.Name))
                {
                    List<User> users = new UserBL().GetAllUsers().ToList();
                    return View("userlist", users);
                }
            }

            return Redirect("~/?msg=noaccess");
        }

        public ActionResult DeleteUser(int userid)
        {
            try
            {
                new UserBL().DeleteUserDropAllRoles(userid);
                return Redirect("~/admin/userlist?msg=deletedsuccessful");
            }
            catch
            {
                return Redirect("~/admin/userlist?msg=deleteunsuccessful");
            }
        }

        public ActionResult EditUser(int userid)
        {
            if (new RoleChecker().checkIfAdmin(User.Identity.Name))
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
            else
            {
                return Redirect("~/?msg=noaccess");
            }
        }

        [HttpPost]
        public ActionResult EditUser(RegisterModel rm)
        {
            try
            {
                User u = new UserBL().GetUserByID(rm.UserID);
                if ((new UserBL().DoesUserNameExist(rm.UserName)) && (u.Username != rm.UserName))
                { return Redirect("/admin/userlist?msg=usernametaken"); }
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
                return Redirect("/admin/userlist?msg=nochangessaved");
            }
        }


        public ActionResult RoleList()
        {
            List<Role> allRoles = new RoleBL().GetAllRoles().ToList();

            RoleModel rModel = new RoleModel();
            List<RoleModel> rModelList = new List<RoleModel>();

            foreach (Role r in allRoles)
            {
                rModel.RoleName = r.RoleName;
                rModel.RoleID = r.RoleID;
                rModel.RoleDesc = r.RoleDesc;

                rModelList.Add(rModel);
                rModel = new RoleModel();
            }

            return View(rModelList);
        }



        public ActionResult CreateRole()
        {
            return View(new RoleModel());
        }

        [HttpPost]
        public ActionResult CreateRole(RoleModel rm)
        {
            try
            {
                Role r = new Role();
                r.RoleName = rm.RoleName;
                r.RoleDesc = rm.RoleDesc;
                new RoleBL().CreateRole(r);
                return Redirect("/admin/rolelist/?msg=success");
            }
            catch
            {
                return Redirect("/admin/rolelist/?msg=error");
            }

        }


        public ActionResult EditRole(int roleid)
        {
            RoleModel rm = new RoleModel();
            Role rr = new RoleBL().GetRoleByID(roleid);

            rm.RoleID = rr.RoleID;
            rm.RoleName = rr.RoleName;
            rm.RoleDesc = rr.RoleDesc;


            return View(rm);
        }



        [HttpPost]
        public ActionResult EditRole(RoleModel rm)
        {
            try
            {
                Role r = new Role();
                r.RoleID = rm.RoleID;
                r.RoleName = rm.RoleName;
                r.RoleDesc = rm.RoleDesc;
                new RoleBL().UpdateRole(r);
                return Redirect("/admin/rolelist/?msg=success");
            }
            catch
            {
                return Redirect("/admin/rolelist/?msg=error");
            }

        }


        public ActionResult ProductList()
        {
            List<Product> pList = new ProductBL().AllProducts().ToList();
            List<ProductModel> pmList = new List<ProductModel>();

            foreach (Product p in pList)
            {
                ProductModel pm = new ProductModel();
                User u = new UserBL().GetUserByID(p.CreatedBy);
                pm.productID = p.ProductID;
                pm.ProductName = p.ProductName;
                pm.ProductStock = p.ProductStock;
                pm.CreatedBy = p.CreatedBy;
                pm.DateAdded = p.DateAdded;
                pm.userName = u.Username;
                pm.ProductImage = p.ProductImage;
                pm.ProductDesc = p.ProductDesc;
                pm.ProductPrice = p.ProductPrice;
                pmList.Add(pm);

            }


            return View(pmList);
        }



        public ActionResult DeleteRole(int roleid)
        {
            try
            {
                new RoleBL().dropRoleandPermissionsAllocated(roleid);
                return Redirect("/admin/rolelist?msg=removedsuccessfully");
            }
            catch
            {
                return Redirect("/admin/rolelist?msg=error");
            }
        }
    }
}
