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



    }
}
