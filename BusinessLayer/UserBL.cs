using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess.Repos;
using DataAccess.CustomExceptions;
using System.Data.Common;
namespace BusinessLayer
{
    public class UserBL
    {
        public void Create(User gb)
        {
            UserRepo x = new UserRepo();
            if (!x.DoesUsernameExist(gb.Username) && !x.DoesEmailExist(gb.Email))
            {
                x.Create(gb);
                RoleRepo rr = new RoleRepo();
                x.Entity = rr.Entity;
                x.Entity.Connection.Open();

                DbTransaction transaction = x.Entity.Connection.BeginTransaction();
                try
                {
                    AddDefaultRole(gb.Username);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw new RegistrationException();
                }
                finally
                {
                    x.Entity.Connection.Close();
                }
            }
        }

        public void AddDefaultRole(string username)
        {
            UserRepo ur = new UserRepo();
            RoleRepo rr = new RoleRepo();
            ur.Entity = rr.Entity;
            User u = ur.GetUserByUsername(username);
            Role r = rr.GetRoleById(2);
            rr.AllocateRole(u, r);
        }

        public bool DoesUserNameExist(string username)
        {
            return new UserRepo().DoesUsernameExist(username);
        }


        public bool DoesEmailExist(string email)
        {
            return new UserRepo().DoesEmailExist(email);
        }




    }
}
