using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Common;


namespace SEImplementation.Models
{
    public class RoleAllocationModel
    {
        public int userID { get; set; }
        public int roleID { get; set; }
        
        public IEnumerable<SelectListItem> RoleList
        {
            get { return new SelectList(new RoleBL().GetAllRoles(), "RoleID", "RoleName"); }
        }

        public IEnumerable<SelectListItem> UserList
        {
            get { return new SelectList(new UserBL().GetAllUsers(), "UserID", "Username"); }
        }
    }
}