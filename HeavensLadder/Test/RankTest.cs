using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class RankTest
    {
        [TestMethod]
        public void TestRankConstructor()
        {
            Team team1 = new Team();
            team1.teamname = "testteam";
            int gamemode = 1;
            Rank rank1 = new Rank(team1, gamemode);

            Assert.AreEqual("testteam", rank1.team.teamname);
            Assert.AreEqual(1, rank1.gamemodeid);

            Team team2 = new Team();
            team2.teamname = "dreamteam";
            Rank rank2 = new Rank(team2, gamemode);

            Assert.AreNotEqual(rank1.team.teamname, rank2.team.teamname);
            Assert.AreEqual(rank1.gamemodeid, rank2.gamemodeid);
        }

        [TestMethod]
        public void TestAddWin()
        {
            Team team1 = new Team();
            team1.teamname = "testteam";
            int gamemode = 1;
            Rank rank = new Rank(team1, gamemode);

            Assert.AreEqual(rank.wins, 0);
            rank.AddWin();
            Assert.AreEqual(rank.wins, 1);    
        }

        [TestMethod]
        public void TestAddLoss()
        {
            Team team1 = new Team();
            team1.teamname = "testteam";
            int gamemode = 1;
            Rank rank = new Rank(team1, gamemode);

            Assert.AreEqual(rank.losses, 0);
            rank.AddLoss();
            Assert.AreEqual(rank.losses, 1);
        }        
    }
}
