using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Text.RegularExpressions;
using Common.CustomExceptions;

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
            if (entry.RoleName == "")
            {
                throw new NullValueException();
            }

            if (!Regex.IsMatch(entry.RoleName, @"^[a-zA-Z]+$"))
            {
                throw new NoNumberAndSymbolsAllowedException();
            }

            if (!Regex.IsMatch(entry.RoleDesc, @"^[A-Za-z ]+$"))
            {
                throw new NoNumberAndSymbolsAllowedException();
            }

            if (Entity.Roles.Count(role => role.RoleName.ToLower() == entry.RoleName.ToLower()) >= 1)
            {
                throw new ValueAlreadyExistsException();
            }

            if (entry.RoleName.Length >= 50)
            {
                throw new ExceedCharacterLimitExecption();
            }

            if (entry.RoleDesc.Length >= 50)
            {
                throw new ExceedCharacterLimitExecption();
            }

            if (entry.RoleName.Length <= 3)
            {
                throw new BeneathLimitAcceptedException();
            }

            if (entry.RoleDesc.Length <= 3)
            {
                throw new BeneathLimitAcceptedException();
            }

            Entity.AddToRoles(entry);
            Entity.SaveChanges();
        }


        public IEnumerable<Role> GetRoles()
        {
            return Entity.Roles;
        }

        public void DeleteRole(int roleid)
        {
            if (Entity.Roles.Count(role => role.RoleID == roleid) == 0)
            {
                throw new ValueDoesNotExistExeception();
            }
            Entity.DeleteObject(GetRoleById(roleid)); //applies the changes
            Entity.SaveChanges();
        }

        public void UpdateRole(Role gb)
        {
            if (gb.RoleName == "")
            {
                throw new NullValueException();
            }

            if (!Regex.IsMatch(gb.RoleName, @"^[a-zA-Z]+$"))
            {
                throw new NoNumberAndSymbolsAllowedException();
            }

            if (!Regex.IsMatch(gb.RoleDesc, @"^[a-zA-Z]+$"))
            {
                throw new NoNumberAndSymbolsAllowedException();
            }

            if (gb.RoleName.Length >= 50)
            {
                throw new ExceedCharacterLimitExecption();
            }

            if (gb.RoleDesc.Length >= 50)
            {
                throw new ExceedCharacterLimitExecption();
            }

            if (gb.RoleName.Length <= 3)
            {
                throw new BeneathLimitAcceptedException();
            }

            if (gb.RoleDesc.Length <= 3)
            {
                throw new BeneathLimitAcceptedException();
            }


            Entity.Roles.Attach(GetRoleById(gb.RoleID)); //gets current values
            Entity.Roles.ApplyCurrentValues(gb); //over write with the new values
            Entity.SaveChanges(); //update the changes
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
