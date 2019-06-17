using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using Domain;
using Data.Entities;
using System.Linq;

namespace Test
{
    [TestClass]
    public class RankRepositoryTest
    {
        Data.Entities.HLContext _db;
        Data.RankRepository test;

        [TestMethod]
        public void TestMapDomainToData()
        {
            Domain.Team team1 = new Domain.Team();
            team1.id = 500;
            int gamemode = 1;
            Domain.Rank domrank = new Domain.Rank(team1, gamemode);

            Data.Entities.Rank datrank = Data.Mapper.Map(domrank);

            Assert.AreEqual(datrank.Teamid, 500);           
            Assert.AreEqual(datrank.Gamemodeid, 1);
        }

        [TestMethod]
        public void TestMapDataToDomain()
        {
            Data.Entities.Team team1 = new Data.Entities.Team();
            team1.Teamname = "testteam";
            int gamemode = 1;
            Data.Entities.Rank datrank = new Data.Entities.Rank();
            datrank.Gamemodeid = gamemode;
            datrank.Team = team1;

            Domain.Rank domrank = Data.Mapper.Map(datrank);

            Assert.AreEqual(domrank.team.teamname, "testteam");
            Assert.AreEqual(domrank.gamemodeid, 1);
        }

        [TestMethod]
        public void TestAlreadyExists()
        {
            _db = new Data.Entities.HLContext();
            test = new Data.RankRepository(_db);
            Data.TeamRepository teamrepo = new Data.TeamRepository(_db);
            Domain.Team team = new Domain.Team();
            team.teamname = "testteam123";
            teamrepo.AddTeam(team);
            _db.SaveChanges();

            int gameid = 1;
            Domain.Team newteam = teamrepo.GetByTeamName("testteam123");
            Domain.Rank rank = new Domain.Rank(newteam, gameid);

            //still working on this part
           // Rank testrank = _db.Rank.Where(x => x.)

            test.DeleteRank(rank);
            bool exists = test.AlreadyExists(rank);
            // should not already exist, since it was never added to the database            
            Assert.AreEqual(exists, false);

            //changed team to newteam so it matches the code
            teamrepo.DeleteTeam(newteam);
            _db.SaveChanges();
        }

        [TestMethod]
        public void TestAddAndDeleteRank()
        {
            _db = new Data.Entities.HLContext();
            test = new Data.RankRepository(_db);
            Data.TeamRepository teamrepo = new Data.TeamRepository(_db);
            Domain.Team team = new Domain.Team();
            team.teamname = "testteam123";
            teamrepo.AddTeam(team);
            _db.SaveChanges();

            int gameid = 1;
            Domain.Team newteam = teamrepo.GetByTeamName("testteam123");
            Domain.Rank rank = new Domain.Rank(newteam, gameid);

            bool add = test.AddRank(rank);
            test.Save();
            Assert.AreEqual(add, true);
            bool repeateadd = test.AddRank(rank);
            test.Save();
            Assert.AreEqual(repeateadd, false);

            Domain.Rank removeRank = test.GetRank("testteam123", 1);
            Assert.IsNotNull(removeRank);

            bool delete = test.DeleteRank(removeRank);
            test.Save();
            Assert.AreEqual(delete, true);
            bool repeatdelete = test.DeleteRank(removeRank);
            test.Save();
            Assert.AreEqual(repeatdelete, false);

            teamrepo.DeleteTeam(newteam);
            _db.SaveChanges();
        }
        
        [TestMethod]
        public void TestInitializeRanks()
        {
            _db = new Data.Entities.HLContext();
            test = new Data.RankRepository(_db);
            Data.TeamRepository teamrepo = new Data.TeamRepository(_db);
            Domain.Team team = new Domain.Team();
            team.teamname = "testteam";
            teamrepo.AddTeam(team);
            _db.SaveChanges();

            Domain.Team newteam = teamrepo.GetByTeamName("testteam");

            bool initialized = test.InitializeRanks(newteam);
            Assert.AreEqual(initialized, true);
            bool repeateinit = test.InitializeRanks(newteam);
            Assert.AreEqual(repeateinit, false);

            var ranklist = test.GetRanksByTeam("testteam");
            foreach (var rank in ranklist)
            {
                test.DeleteRank(rank);
            }

            teamrepo.DeleteTeam(newteam);
            _db.SaveChanges();
        }

        [TestMethod]
        public void TestUpdateRank()
        {
            _db = new Data.Entities.HLContext();
            test = new Data.RankRepository(_db);
            Data.TeamRepository teamrepo = new Data.TeamRepository(_db);
            Domain.Team team = new Domain.Team();
            team.teamname = "testteam";
            teamrepo.AddTeam(team);
            _db.SaveChanges();

            int gameid = 1;
            Domain.Team newteam = teamrepo.GetByTeamName("testteam");
            Domain.Rank rank = new Domain.Rank(newteam, gameid);
            Domain.Rank rank2 = new Domain.Rank(newteam, 2);

            test.AddRank(rank);
            test.Save();

            bool updatenotexist = test.UpdateRank(rank2);
            //should be false, because rank is not in database so cannot be updated
            Assert.AreEqual(false, updatenotexist);

            var newrank = test.GetRank("testteam", 1);
            bool update = test.UpdateRank(newrank);
            Assert.AreEqual(true, update);

            
            newrank.AddWin();
            test.UpdateRank(newrank);
            var uprank = test.GetRank("testteam", 1);
            Assert.AreEqual(uprank.wins, 1);

            test.DeleteRank(newrank);
            teamrepo.DeleteTeam(newteam);
            _db.SaveChanges();
        }
    }
}
