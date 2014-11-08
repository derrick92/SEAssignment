using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess.Repos
{
    public class RoleRepo : ConnectionClass
    {
        public RoleRepo()
            : base()
        {
        }

        public void CreateRole(Role entry)
        {
            Entity.AddToRoles(entry);
            Entity.SaveChanges();
        }

        public IEnumerable<Role> GetRoles()
        {
            return Entity.Roles;
        }


        public IEnumerable<Role> GetUserRoles(User u)
        {
            return u.Roles;
        }

        public Role GetRoleById(int id)
        {
            return Entity.Roles.SingleOrDefault(x => x.RoleID == id);
        }

        public void AllocateRole(User u, Role r)
        {
            u.Roles.Add(r);
            Entity.SaveChanges();
        }

        public void DeallocateRole(User u, Role r)
        {
            u.Roles.Remove(r);
            Entity.SaveChanges();
        }

        public void DeallocateUser(User u, Role r)
        {
            r.Users.Remove(u);
            Entity.SaveChanges();
        }


        public bool IsInRole(User u, Role r)
        {
            if (u.Roles.SingleOrDefault(x => x.RoleID == r.RoleID) == null)
                return false;
            else return true;
        }

    }
}
