using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess.Repos;

namespace BusinessLayer
{
    public class RoleBL
    {

        public bool IsInRole(int userid, int roleId)
        {

            RoleRepo rr = new RoleRepo();
            UserRepo ur = new UserRepo();
            rr.Entity = ur.Entity;

            return rr.IsInRole(
                ur.GetUserByID(userid),
                rr.GetRoleById(roleId));
        }

        public IEnumerable<Role> GetUserRoles(User u)
        {
            return new RoleRepo().GetUserRoles(u);
        }

        public void dropRole(int userid, int roleid)
        {
            UserRepo ur = new UserRepo();
            RoleRepo rr = new RoleRepo();
            ur.Entity = rr.Entity;
            User u = ur.GetUserByID(userid);
            Role r = rr.GetRoleById(roleid);
            rr.DeallocateUser(u, r);
        }



    }
}
