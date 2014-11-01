using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess.Repos
{
    public class UserRepo:ConnectionClass
    {
        public UserRepo()
            : base()
        {

        }

        public void Create(User entry)
        {
            Entity.AddToUsers(entry);
            Entity.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return Entity.Users.SingleOrDefault(u => u.Username == username);
        }

        public bool DoesUsernameExist(string Username)
        {
            if (Entity.Users.Count(user => user.Username == Username) == 0)
                return false;
            else
                return true;
        }


        public bool DoesEmailExist(string email)
        {
            if (Entity.Users.Count(user => user.Email == email) == 0)
                return false;
            else
                return true;
        }
    }
}
