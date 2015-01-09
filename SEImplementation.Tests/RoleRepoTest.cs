using DataAccess.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Common;
using Common.CustomExceptions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Data;

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


        //Using an ambient transaction when you use the TransactionScope type. 
        //By default, the Execution and Privileged connections will not use the ambient 
        //transaction, because the connections were created before the method is executed.
        //The Connection has an EnlistTransaction method, which associates an active
        //connection with a transaction. When an ambient transaction is created, it 
        //registers itself as the current transaction, and you can access it through 
        //the Current property.
        TransactionScope _trans;

        [TestInitialize()]
        public void Init()
        {
            rList = entity.Roles.ToList();
            _trans = new TransactionScope();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _trans.Dispose();
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
        [ExpectedException(typeof(ExceedCharacterLimitExecption))]
        public void CreateRoleTestExeedCharacterLimitonName()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "THUNDERSTORMTHUNDERSTORMTHUNDERSTORMTHUNDERSTORMTHUNDERSTORM";
            entry.RoleDesc = "Test";
            target.CreateRole(entry);
            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        [ExpectedException(typeof(ExceedCharacterLimitExecption))]
        public void CreateRoleTestExeedCharacterLimitonDesc()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "testRole";
            entry.RoleDesc = "THUNDERSTORMTHUNDERSTORMTHUNDERSTORMTHUNDERSTORMTHUNDERSTORM";
            target.CreateRole(entry);
            target.DeleteRole(entry.RoleID);
        }


        [TestMethod()]
        [ExpectedException(typeof(BeneathLimitAcceptedException))]
        public void CreateRoleTestBeneathCharacterLimitonName()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "t";
            entry.RoleDesc = "Test";
            target.CreateRole(entry);
            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        [ExpectedException(typeof(BeneathLimitAcceptedException))]
        public void CreateRoleTestBeneathCharacterLimitonDesc()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "testRole";
            entry.RoleDesc = "t";
            target.CreateRole(entry);
            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullValueException))]
        public void CreateRoleEmptyObjectTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();

            target.CreateRole(entry);

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
        public void CreateRoleSuccessGetIDNotNullTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "TestingName";
            entry.RoleDesc = "TestingDesc";
            target.CreateRole(entry);

            string x = entry.RoleID.ToString();
           
            Assert.IsNotNull(x);

            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        public void CreateRoleSuccessGetIDNotEqualNums()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "TestingName";
            entry.RoleDesc = "TestingDesc";
            target.CreateRole(entry);

            int actual = entry.RoleID;
            int expect = 0;

            Assert.AreNotEqual(expect, actual);

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
        public void CreateRoleSuccessDescAlreadyExistTest()
        {
            RoleRepo target = new RoleRepo();
            Role entry = new Role();
            entry.RoleName = "TestingName";
            entry.RoleDesc = "Able to buy only";
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

        [TestMethod()]
        [ExpectedException(typeof(ConstraintException))]
        public void createRoleNullNameTest()
        {
            RoleRepo target = new RoleRepo();

            Role entry = new Role();
            entry.RoleName = null;
            entry.RoleDesc = "test";
            target.CreateRole(entry);

            target.DeleteRole(entry.RoleID);
        }

        [TestMethod()]
        [ExpectedException(typeof(ConstraintException))]
        public void createRoleNullDescTest()
        {
            RoleRepo target = new RoleRepo();

            Role entry = new Role();
            entry.RoleName = "test";
            entry.RoleDesc = null;
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
            Role a = new Role();
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();

            //Created Exoected Local user
            Role expected = new Role();
            expected = actual;
            expected.RoleName = "zzzz";

            //Updates the user inside the database
            actual.RoleName = "zzzz";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            Assert.AreEqual(expected, uActual); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }

        [TestMethod()]
        [ExpectedException(typeof(System.Data.UpdateException))]
        public void UpdateRoleEmptyTest()
        {
            Role a = new Role();
            RoleRepo target = new RoleRepo();
            Role actual = new Role();

            entity.AddToRoles(actual);
            entity.SaveChanges();
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
            actual.RoleName = "xxxx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            int id = uActual.RoleID;
            //Created Exoected Local user

            Role expected = new Role();
            expected.RoleName = "xxxx";

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
            actual.RoleDesc = "xxxx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);


            Role expected = new Role();
            expected.RoleDesc = "xxxx";

            Assert.AreEqual(expected.RoleDesc, uActual.RoleDesc); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }


        [TestMethod()]
        public void UpdateRoleTestNotMatching()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "xxxx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            int id = uActual.RoleID;
            //Created Exoected Local user

            Role expected = new Role();
            expected.RoleName = "zzzz";

            Assert.AreNotEqual(expected.RoleName, uActual.RoleName); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }


        [TestMethod()]
        [ExpectedException(typeof(ConstraintException))]
        public void UpdateRoleTestNullName()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "xxxx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            int id = uActual.RoleID;
            //Created Exoected Local user

            Role expected = new Role();
            expected.RoleName = null;

            Assert.AreNotEqual(expected.RoleName, uActual.RoleName); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }


        [TestMethod()]
        [ExpectedException(typeof(ConstraintException))]
        public void UpdateRoleTestNullDesc()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "xxxx";
            target.UpdateRole(actual);
            Role uActual = entity.Roles.SingleOrDefault(x => x.RoleID == actual.RoleID);

            int id = uActual.RoleID;
            //Created Exoected Local user

            Role expected = new Role();
            expected.RoleDesc = null;

            Assert.AreNotEqual(expected.RoleName, uActual.RoleName); //Compares
            target.DeleteRole(uActual.RoleID);//Deletes

        }


        [TestMethod()]
        [ExpectedException(typeof(NullValueException))]
        public void UpdateRoleTestEmptyName()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = string.Empty;
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(NullValueException))]
        public void UpdateRoleTestEmptyDesc()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleDesc = string.Empty;
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }


        [TestMethod()]
        [ExpectedException(typeof(BeneathLimitAcceptedException))]
        public void UpdateRoleFewNameTest()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "X";
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ExceedCharacterLimitExecption))]
        public void UpdateRoleExceedNameTest()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(BeneathLimitAcceptedException))]
        public void UpdateRoleFewDescTest()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleDesc = "X";
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ExceedCharacterLimitExecption))]
        public void UpdateRoleExceedDescTest()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleDesc = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            try { target.UpdateRole(actual); }
            finally
            {
                target.DeleteRole(actual.RoleID);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(NoNumberAndSymbolsAllowedException))]
        public void UpdateRoleExceedNull()
        {
            RoleRepo target = new RoleRepo();
            Role actual = new Role();
            actual.RoleName = "roleTest";
            actual.RoleDesc = "roleDesc";

            entity.AddToRoles(actual);
            entity.SaveChanges();
            entity.Connection.Close();

            //Updates the user inside the database
            actual.RoleName = "T3$T";
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
