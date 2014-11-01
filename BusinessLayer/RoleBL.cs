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
    }
}
