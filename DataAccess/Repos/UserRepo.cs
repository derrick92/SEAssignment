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

        public void CreateUser(User entry)
        {
            Entity.AddToUsers(entry);
            Entity.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Entity.Users.AsEnumerable();
        }

        public User GetUserByUsername(string username)
        {
            return Entity.Users.SingleOrDefault(u => u.Username == username);
        }

        public User GetUserByID(int id)
        {
            return Entity.Users.SingleOrDefault(u => u.UserID == id);
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

        public bool isAuthenticated(string Username, string PW)
        {
            if (Entity.Users.Count(user => user.Username == Username && user.Password == PW) > 0)
                return true;//1 exists
            else
                return false;//false does not exist
        }

        public void UpdateUser(User gb)
        {
            Entity.Users.Attach(GetUserByID(gb.UserID)); //gets current values
            Entity.Users.ApplyCurrentValues(gb); //over write with the new values
            Entity.SaveChanges(); //update the changes
        }


        public void UpdateUserTesting(User gb)
        {
       //     Entity.Users.Attach(GetUserByID(gb.UserID)); //gets current values
            Entity.Users.ApplyCurrentValues(gb); //over write with the new values
            Entity.SaveChanges(); //update the changes
        }

        public void DeleteUser(int userId)
        {
            Entity.DeleteObject(GetUserByID(userId)); //applies the changes
            Entity.SaveChanges();
        }




    }
}
