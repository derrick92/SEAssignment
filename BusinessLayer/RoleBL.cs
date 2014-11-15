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
        public void CreateRole(Role r)
        {
            new RoleRepo().CreateRole(r);
        }

        public Role GetRoleByID(int id)
        {
            return new RoleRepo().GetRoleById(id);
        }

        public void UpdateRole(Role gb)
        {
            new RoleRepo().UpdateRole(gb);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return new RoleRepo().GetRoles();
        }

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

        public void AllocateRole(int userid, int roleid)
        {
            UserRepo ur = new UserRepo();
            RoleRepo rr = new RoleRepo();
            ur.Entity = rr.Entity;
            User u = ur.GetUserByID(userid);
            Role r = rr.GetRoleById(roleid);
            rr.AllocateRole(u, r);
        }

        public void AllocatePermission(int permissionid, int roleid)
        {
            RoleRepo role = new RoleRepo();
            PermissionRepo permission = new PermissionRepo();
            role.Entity = permission.Entity;


            Role r = role.GetRoleById(roleid);
            Permission p = permission.GetPermissionById(permissionid);

            permission.AllocatePermission(p, r);
        }

    }
}
