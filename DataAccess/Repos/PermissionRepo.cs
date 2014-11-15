using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess.Repos
{
    public class PermissionRepo : ConnectionClass
    {
        public PermissionRepo()
            : base()
        {
        }

        public IEnumerable<Permission> GetPermissions()
        {
            return Entity.Permissions;
        }

        public Permission GetPermissionById(int id)
        {
            return Entity.Permissions.SingleOrDefault(x => x.PermissionID == id);
        }

        public void AllocatePermission(Permission p, Role r)
        {
            p.Roles.Add(r);
            Entity.SaveChanges();
        }

        public void DeallocatePermission(Permission p, Role r)
        {
            p.Roles.Remove(r);
            Entity.SaveChanges();
        }

        public bool IsInPermission(Permission p, Role r)
        {
            if (p.Roles.SingleOrDefault(x => x.RoleID == r.RoleID) == null)
                return false;
            else return true;
        }

        public IEnumerable<Permission> GetRolePermissions(Role r)
        {
            return r.Permissions;
        }

    }
}
