using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using Domain;
using Data.Entities;

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
            team1.teamname = "testteam";
            int gamemode = 1;
            Domain.Rank domrank = new Domain.Rank(team1, gamemode);

            Data.Entities.Rank datrank = Data.Mapper.Map(domrank);

            Assert.AreEqual(datrank.Team.Teamname, "testteam");           
            Assert.AreEqual(datrank.Gamemode.Id, 1);
        }

        [TestMethod]
        public void TestMapDataToDomain()
        {
            Data.Entities.Team team1 = new Data.Entities.Team();
            team1.Teamname = "testteam";
            int gamemode = 1;
            Data.Entities.Rank datrank = new Data.Entities.Rank();
            Data.Entities.GameModes deGame = new Data.Entities.GameModes();
            deGame.Id = gamemode;
            datrank.Gamemode = deGame;
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
            Domain.Team team = new Domain.Team();
            team.teamname = "testteam";
            int gameid = 1;
            Domain.Rank rank = new Domain.Rank(team, gameid);

            bool exists = test.AlreadyExists(rank);
                        
            Assert.AreEqual(exists, false);
        }
        /*
        [TestMethod]
        public void TestInitializeRanks()
        {
            _db = new Data.Entities.HLContext();
            test = new Data.RankRepository(_db);
            Domain.Team team = new Domain.Team();
            team.teamname = "testteam";

            bool initialized = test.InitializeRanks(team);
            Assert.AreEqual(initialized, true);
            bool repeateinit = test.InitializeRanks(team);
            Assert.AreEqual(repeateinit, false);
        }*/
    }
}
