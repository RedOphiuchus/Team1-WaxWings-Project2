using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    class RankTest
    {
        [TestMethod]
        public void TestRankConstructor()
        {
            Team team1 = new Team();
            team1.teamname = "testteam";
            string gamemode = "Deathmatch";
            Rank rank = new Rank(team1, gamemode);

            Assert.AreEqual(team1.teamname, rank.team.teamname);
            Assert.AreEqual(gamemode, rank.gamemode);

            Team team2 = new Team();
            team2.teamname = "dreamteam";
            gamemode = "Deathmatch";
            Rank rank2 = new Rank(team2, gamemode);

            Assert.AreNotEqual(rank.team.teamname, rank2.team.teamname);
            Assert.AreEqual(rank.gamemode, rank2.gamemode);
        }
    }
}
