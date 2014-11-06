using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using BusinessLayer;
using SEImplementation.Models;
using System.Web.Security;

namespace SEImplementation.Classes
{
    public class RoleChecker
    {

        public bool checkIfAdmin(string Username)
        {
            User u = new UserBL().GetUserByUsername(Username);
            List<Role> userRoles = new RoleBL().GetUserRoles(u).ToList();

            bool userIsInRole = false;
            foreach (Role r in userRoles)
            {
                if (new PermissionBL().IsInPermission(6, r.RoleID))
                {
                    userIsInRole = true;
                }
            }

            return userIsInRole;
        }

        public bool checkIfSeller(string Username)
        {
            User u = new UserBL().GetUserByUsername(Username);
            List<Role> userRoles = new RoleBL().GetUserRoles(u).ToList();

            bool userIsInRole = false;
            foreach (Role r in userRoles)
            {
                if (new PermissionBL().IsInPermission(2, r.RoleID))
                {
                    userIsInRole = true;
                }
            }

            return userIsInRole;
        }

        public bool checkIfBuyer(string Username)
        {
            User u = new UserBL().GetUserByUsername(Username);
            List<Role> userRoles = new RoleBL().GetUserRoles(u).ToList();

            bool userIsInRole = false;
            foreach (Role r in userRoles)
            {
                if (new PermissionBL().IsInPermission(5, r.RoleID))
                {
                    userIsInRole = true;
                }
            }

            return userIsInRole;
        }



    }
}