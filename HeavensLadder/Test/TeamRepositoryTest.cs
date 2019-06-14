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


        [TestMethod]
        public void UpdateTeamTest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.TeamRepository test = new Data.TeamRepository(_db);
            bool what; //random bool to hold data about success of methods.
            bool success; //initialize boolean for asserts
            //first case, we pass the method a faulty team check for null case and count case.
            Domain.Team team = new Domain.Team();
            success = test.UpdateTeam(team);
            Assert.AreEqual(success, false);


            //second test case for we have an empty team in the database and it is updated to contain a team.
            team.teamname = "testteamname";

            Domain.User user = new Domain.User("username1","password1");
            team.Roles.Add(true);
            team.Userlist.Add(user);
            what = test.DeleteTeam(team);
            what = test.AddTeam(team);

            success = test.UpdateTeam(team);
            Assert.AreEqual(success, true);
            //keep database clean and undo my add.
            what = test.DeleteTeam(team);

            //third test case for when we have a faulty team with more roles than users, it should fail
            team.Roles.Add(false);
            what = test.AddTeam(team);
            success = test.UpdateTeam(team);
            Assert.AreEqual(success, false);
            //keep database clean by undoing the add
            what = test.DeleteTeam(team);
        }

        [TestMethod]
        public void GetByTeamNameTest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.TeamRepository test = new Data.TeamRepository(_db);

            Domain.Team miteam = new Domain.Team();
            miteam.teamname = "grisaia";
            Domain.User user = new Domain.User("username1", "password1");
            miteam.Userlist.Add(user);
            miteam.Roles.Add(true);
            bool success = test.AddTeam(miteam);

            Domain.Team newteam = test.GetByTeamName("grisaia");
            

        }


    }
}
