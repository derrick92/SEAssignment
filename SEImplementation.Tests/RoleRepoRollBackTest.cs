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
using System.Data.Common;
namespace SEImplementation.Tests
{
    [TestClass()]
    public class RoleRepoRollBackTest
    {

        DbTransaction trans;
        RoleRepo TargetRepo;
        SEshopEntities entity;
        List<Role> expected = new List<Role>();
        
        //when test starts
        [TestInitialize()]
        public void Init()
        {
            entity = new SEshopEntities(); //database initialization 
            TargetRepo = new RoleRepo(); // role initialization
            expected = entity.Roles.ToList(); //expected list

            TargetRepo.Entity.Connection.Open(); //target connection
            trans = TargetRepo.Entity.Connection.BeginTransaction(); //begains transaction
        }

        //when test is done
        [TestCleanup()]
        public void Cleanup()
        {
            trans.Rollback();//rollback any change
            TargetRepo.Entity.Connection.Close(); //close the connection
        }



        [TestMethod()]
        public void CreateRoleSuccessNotEqualListTest()
        {
            Role entry = new Role();
            entry.RoleName = "TestingName";
            entry.RoleDesc = "TestingDesc";
            TargetRepo.CreateRole(entry);
            List<Role> actual = TargetRepo.GetRoles().ToList();
            Assert.AreNotEqual(expected.Count, actual.Count);
        }
    }
}
