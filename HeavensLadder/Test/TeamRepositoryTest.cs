using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data;

namespace Test
{
    [TestClass]
   public class TeamRepositoryTest
    {
        Data.Entities.HLContext _db;
        Data.TeamRepository test;

   

        

        [TestMethod]
        public void AddAndRemoveTeamTest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.TeamRepository test = new Data.TeamRepository(_db);
            bool success; //variable to determine if a team was added or removed successfully.
            Domain.Team x = new Domain.Team();
            x.teamname = "XXstarstrikersXX1113452435x4";
            success = test.DeleteTeam(x);
            _db.SaveChanges();
            success = test.AddTeam(x);
            _db.SaveChanges();
            //assert that the team was added to the database
            Assert.AreEqual(success, true);

            success = test.AddTeam(x);
            _db.SaveChanges();
            //assert that the team was not added to the database because it already exists
            Assert.AreEqual(success, false);

            //assert that the team was successfuly deleted from the database
            success = test.DeleteTeam(x);
            _db.SaveChanges();
            Assert.AreEqual(success, true);

            //assert that the team was not deleted from the database because it did not exist
            success = test.DeleteTeam(x);
            _db.SaveChanges();
            Assert.AreEqual(success, false);

        }
    }
}
