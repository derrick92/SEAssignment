using DataAccess.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Common;
using Common.CustomExceptions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEImplementation.Tests
{
    /// <summary>
    ///This is a test class for RoleRepoTest and is intended
    ///to contain all RoleRepoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RoleRepoTest
    {
        SEshopEntities entity = new SEshopEntities();
        List<Role> rList = new List<Role>();

        /// <summary>
        /// Initialize Method.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            rList = entity.Roles.ToList();
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


        #region Create Test Methods

        [TestMethod()]
        [ExpectedException(typeof(NoNumberAndSymbolsAllowedException))]
        public void CreateRoleTestNoNumSymbRoleNameTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "H3ll$";
            entry.RoleDesc = "Test";
            target.CreateRole(entry);
            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        [ExpectedException(typeof(NoNumberAndSymbolsAllowedException))]
        public void CreateRoleTestNoNumSymbRoleDescTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "Hello";
            entry.RoleDesc = "T3$t";
            target.CreateRole(entry);
            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        public void CreateRoleSuccessNotEqualListTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "TestingName";
            entry.RoleDesc = "TestingDesc";
            target.CreateRole(entry);


            List<Role> actual = new List<Role>();
            actual = entity.Roles.ToList();

            Assert.AreNotEqual(rList, actual);

            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        public void CreateRoleSuccessEqualListTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "TestingName";
            entry.RoleDesc = "TestingDesc";
            target.CreateRole(entry);


            List<Role> expected = new List<Role>();
            expected = entity.Roles.ToList();

            List<Role> actual = new List<Role>();
            actual = target.GetRoles().ToList();

            int iExpected = expected.Count();
            int iActual = actual.Count();

            Assert.AreEqual(iExpected, iActual);

            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        public void CreateRoleSuccessEqualValueTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "TestingName";
            entry.RoleDesc = "TestingDesc";
            target.CreateRole(entry);


            List<Role> expected = new List<Role>();
            expected = entity.Roles.ToList();

            List<Role> actual = new List<Role>();
            actual = target.GetRoles().ToList();

            Role rActual = new Role();
            Role rExpected = new Role();

            foreach (var item in expected)
            {
                if (item.RoleID == entry.RoleID)
                    rExpected = item;
            }

            foreach (var item in actual)
            {
                if (item.RoleID == entry.RoleID)
                    rActual = item;
            }

            Assert.AreEqual(rExpected.RoleName, rActual.RoleName);
            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValueAlreadyExistsException))]
        public void CreateRoleAlreadyExistTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "Admin";
            entry.RoleDesc = "test";
            target.CreateRole(entry);
            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullValueException))]
        public void createRoleEmptyTest()
        {
            RoleRepo target = new RoleRepo();

            Role entry = new Role();
            entry.RoleName = "";
            entry.RoleDesc = "test";
            target.CreateRole(entry);

            target.DeleteRole(entry.RoleID);
        }
        #endregion

        #region Get Role by Id Test Methods
        /// <summary>
        ///A test for GetRoleById
        ///</summary>
        [TestMethod()]
        public void GetRoleByIdExistTest()
        {
            RoleRepo target = new RoleRepo();
           
            Role expected = entity.Roles.SingleOrDefault(x => x.RoleID == 1);
            Role actual;
            actual = target.GetRoleById(1);

            Assert.AreEqual(expected.RoleID, actual.RoleID);
        }

        [TestMethod()]
        public void GetRoleByIdNotExistentTest()
        {
            RoleRepo target = new RoleRepo();

            Role actual;
            actual = target.GetRoleById(3948391);
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetRoleByIdBoundaryNegativeTest()
        {
            RoleRepo target = new RoleRepo();

            Role actual;
            actual = target.GetRoleById(-5);
            Assert.IsNull(actual);
        }
        #endregion


        #region Update Test Methods

        [TestMethod()]
        public void UpdateRoleTestMatching1()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();

            //Created Exoected Local user
            Role expected = new Role();
            expected = actual;
            expected.RoleName = "zz";

            //Updates the user inside the database
            actual.RoleName = "zz";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            Assert.AreEqual(expected, uActual); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes
        
        }


        [TestMethod()]
        public void UpdateRoleTestMatchingName()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "xx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            int id = uActual.RoleID;
            //Created Exoected Local user

            Role expected = new Role();
            expected.RoleName = "xx";

            Assert.AreEqual(expected.RoleName, uActual.RoleName); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }


        [TestMethod()]
        public void UpdateRoleTestMatchingDesc()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleDesc = "xx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);


            Role expected = new Role();
            expected.RoleDesc = "xx";

            Assert.AreEqual(expected.RoleDesc, uActual.RoleDesc); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }


        [TestMethod()]
        public void UpdateRoleTestNotMatching(){
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "xx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            int id = uActual.RoleID; 
            //Created Exoected Local user

            Role expected = new Role();
            expected.RoleName = "zz";

            Assert.AreNotEqual(expected.RoleName, uActual.RoleName); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }

        [TestMethod()]
        [ExpectedException(typeof(NullValueException))]
        public void UpdateRoleTestNull()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "";
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(NoNumberAndSymbolsAllowedException))]
        public void UpdateRoleTestRoleNameBad()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "%%$23fdf";
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }


        [TestMethod()]
        [ExpectedException(typeof(NoNumberAndSymbolsAllowedException))]
        public void UpdateRoleTestRoleDescBad()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleDesc = "%%$f23";
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
            
        }

        #endregion


        #region Delete Test Methods
        [TestMethod()]
        public void DeleteRoleSuccessfulTest()
        {
            Role r = new Role();
            r.RoleName = "testRName";
            r.RoleDesc = "testRDesc";

            entity.AddToRoles(r);
            entity.SaveChanges();

            RoleRepo target = new RoleRepo();
            int roleid = r.RoleID;
            target.DeleteRole(roleid);


            Assert.IsNull(entity.Roles.SingleOrDefault(x => x.RoleID == r.RoleID));
        }

        [TestMethod()]
        [ExpectedException(typeof(ValueDoesNotExistExeception))]
        public void DeleteRoleDoesNotExistTest()
        {
            RoleRepo target = new RoleRepo();
            int roleid = 343456;
            target.DeleteRole(roleid);

        }


        [TestMethod()]
        [ExpectedException(typeof(ValueDoesNotExistExeception))]
        public void DeleteRoleNegativeTest()
        {
            RoleRepo target = new RoleRepo();
            int roleid = -1;
            target.DeleteRole(roleid);

        }
        #endregion


        #region Get Roles (ListValue)
        [TestMethod()]
        public void GetRolesCountTest()
        {
            RoleRepo target = new RoleRepo();
            List<Role> expected = rList; 
            List<Role> actual;
            actual = target.GetRoles().ToList();

            int iExpected = expected.Count();
            int iActual = actual.Count();

            Assert.AreEqual(iExpected, iActual);

        }

        [TestMethod()]
        public void GetRolesValuesTest()
        {
            RoleRepo target = new RoleRepo();
            List<Role> expected = entity.Roles.ToList();
            List<Role> actual = target.GetRoles().ToList();

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).RoleID, expected.ElementAt(i).RoleID);
            }

        }
        #endregion
    }
}
