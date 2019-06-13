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
        [TestMethod]
        public void TestMapDomainToData()
        {
            Domain.Team team1 = new Domain.Team();
            team1.teamname = "testteam";
            string gamemode = "Deathmatch";
            Domain.Rank domrank = new Domain.Rank(team1, gamemode);

            Data.Entities.Rank datrank = Data.Mapper.Map(domrank);

            Assert.AreEqual(datrank.Team.Teamname, "testteam");           
            Assert.AreEqual(datrank.Gamemode.Modename, "Deathmatch");
        }

        [TestMethod]
        public void TestMapDataToDomain()
        {
            Data.Entities.Team team1 = new Data.Entities.Team();
            team1.Teamname = "testteam";
            string gamemode = "Deathmatch";
            Data.Entities.Rank datrank = new Data.Entities.Rank();
            Data.Entities.GameModes deGame = new Data.Entities.GameModes();
            deGame.Modename = gamemode;
            datrank.Gamemode = deGame;
            datrank.Team = team1;

            Domain.Rank domrank = Data.Mapper.Map(datrank);

            Assert.AreEqual(domrank.team.teamname, "testteam");
            Assert.AreEqual(domrank.gamemode, "Deathmatch");
        }
    }
}
