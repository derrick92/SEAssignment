using DataAccess.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Common;
using System.Collections.Generic;
using DataAccess.Repos;
namespace SEImplementation.Tests
{
    
    
    /// <summary>
    ///This is a test class for UserRepoTest and is intended
    ///to contain all UserRepoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserRepoTest
    {
        public User generateUser(UserRepo target)
        {
            User actual = new User();
            actual.Username = "unitTestingUsername";
            actual.Password = "unitTestingPassword";
            actual.FirstName = "unitTestingFirstName";
            actual.Surname = "unitTestingSurname";
            actual.Email = "unitTestingEmail";
            actual.MobileNum = 122;

            target.CreateUser(actual);

            return actual;
        }


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetAllUsers
        ///</summary>
        [TestMethod()]
        public void GetAllUsersTest()
        {
            UserRepo target = new UserRepo();
            IEnumerable<User> actual;
            actual = target.GetAllUsers();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetUserByID
        ///</summary>
        [TestMethod()]
        public void GetUserByIDTest()
        {
            UserRepo target = new UserRepo();

            User u = generateUser(target);

            int userID = u.UserID; 
            User expected = u; 
            User actual;
            actual = target.GetUserByID(userID);
            Assert.AreEqual(expected, actual);
            target.DeleteUser(actual.UserID);//Deletes
        }

        /// <summary>
        ///A test for GetUserByUsername
        ///</summary>
        [TestMethod()]
        public void GetUserByUsernameTest()
        {
            UserRepo target = new UserRepo(); 

            User u = generateUser(target);

            string userName = u.Username; 
            User expected = u; 
            User actual;
            actual = target.GetUserByUsername(userName);
            Assert.AreEqual(expected, actual);
            target.DeleteUser(actual.UserID);//Deletes
        }





        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserTest()
        {
            UserRepo target = new UserRepo();
            User actual = generateUser(target);

            //Created Exoected Local user
            User expected = new User();
            expected = actual;
            expected.FirstName = "FirstNameChange";

            //Updates the user inside the database
            actual.FirstName = "FirstNameChange";
            target.UpdateUserTesting(actual);
            User uActual = target.GetUserByID(actual.UserID);

            Assert.AreEqual(expected, uActual); //Compares
            target.DeleteUser(uActual.UserID);//Deletes
        }

        /// <summary>
        ///A test for DeleteUser
        ///</summary>
        [TestMethod()]
        public void DeleteUserTest()
        {

            UserRepo target = new UserRepo();
            IEnumerable<User> expectedUserList = target.GetAllUsers();
            User u = generateUser(target);
            int userId = u.UserID;
            target.DeleteUser(userId);
            IEnumerable<User> actualUserList = target.GetAllUsers();
            Assert.AreEqual(expectedUserList, actualUserList);
        }

        /// <summary>
        ///A test for CreateUser
        ///</summary>
        [TestMethod()]
        public void CreateUserTest()
        {
            UserRepo target = new UserRepo(); 
            User u = generateUser(target);
            bool found = false;
            try
            {
                User x = target.GetUserByID(u.UserID);
                if (x != null)
                {
                    found = true;
                }
            }
            catch (Exception)
            {

                found = false;
            }
            finally
            {
                Assert.IsTrue(found);
            }

            target.DeleteUser(u.UserID);
        }
    }
}
