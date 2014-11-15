using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess.Repos;

namespace BusinessLayer
{
    public class PermissionBL
    {

        public IEnumerable<Permission> GetAllPermissions()
        {
            return new PermissionRepo().GetPermissions();
        }



        public bool IsInPermission(int permissionId, int roleId)
        {

            RoleRepo rr = new RoleRepo();
            PermissionRepo pr = new PermissionRepo();
            rr.Entity = pr.Entity;

            return pr.IsInPermission(
                pr.GetPermissionById(permissionId),
                rr.GetRoleById(roleId));
        }

        public IEnumerable<Permission> GetRolePermissions(Role r)
        {
            return new PermissionRepo().GetRolePermissions(r);
        }

        public void dropPermission(int permissionid, int roleid)
        {
            PermissionRepo pr = new PermissionRepo();
            RoleRepo rr = new RoleRepo();
            pr.Entity = rr.Entity;

            Permission p = pr.GetPermissionById(permissionid);
            Role r = rr.GetRoleById(roleid);
            pr.DeallocatePermission(p, r);
        }
    }
}
