using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;
using SEImplementation.Models;
using System.Data.SqlClient;

namespace SEImplementation.Controllers
{
    public class RolesController : Controller
    {
        //
        // GET: /Roles/

        public ActionResult UserRoleList(int userid)
        {
            User u = new UserBL().GetUserByID(userid);
            List<Role> userRoles = new RoleBL().GetUserRoles(u).ToList();

            RoleModel rModel = new RoleModel();
            List<RoleModel> rModelList = new List<RoleModel>();

            foreach (Role r in userRoles)
            {
                rModel.RoleName = r.RoleName;
                rModel.RoleID = r.RoleID;
                rModel.RoleDesc = r.RoleDesc;
                rModel.UserID = userid;

                rModelList.Add(rModel);
                rModel = new RoleModel();
            }

            return View(rModelList);
        }


        public ActionResult DeallocateRole(int userid, int roleid)
        {
            new RoleBL().dropRole(userid, roleid);
            return Redirect("/admin/userlist?msg=removedsuccessfully");
        }


        public ActionResult AllocateRole()
        {
            return View(new RoleAllocationModel());
        }


        [HttpPost]
        public ActionResult AllocateRole(RoleAllocationModel rm)
        {
            int roleid = rm.roleID;
            int userid = rm.userID;
            try
            {
                new RoleBL().AllocateRole(userid, roleid);
            }
            catch
            {
                return Redirect("/admin?msg=rolealreadyallocatedwithuser");
            }
            return Redirect("/admin?msg=roleadded");
        }

        public ActionResult AllocatePermission()
        {
            return View(new RoleAllocationModel());
        }


        [HttpPost]
        public ActionResult AllocatePermission(RoleAllocationModel rm)
        {
            int roleid = rm.roleID;
            int permissionid = rm.permissionID;
            try
            {
                new RoleBL().AllocatePermission(permissionid, roleid);
            }
            catch
            {
                return Redirect("/admin?msg=permissionalreadyallocatedwithuser");
            }
            return Redirect("/admin?msg=permissionadded");
        }






    }
}
