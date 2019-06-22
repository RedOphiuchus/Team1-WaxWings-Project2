using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class ChallengeTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            //arrange
            Domain.Team team1 = new Domain.Team();
            Domain.Team team2 = new Domain.Team();

            //act
            Domain.Challenge ch1 = new Domain.Challenge(team1, team2, 0);
            Domain.Challenge ch2 = new Domain.Challenge(0, team1, team2, 0, true, false);
            Domain.Challenge ch3 = new Domain.Challenge(0, team1, team2, 0, null, null);

            //assert
            Assert.AreEqual(ch1.Team1, team1);
            Assert.AreEqual(ch1.Team2, team2);
            Assert.AreEqual(ch1.GameModeId, 0);

            Assert.AreEqual(ch2.Team1, team1);
            Assert.AreEqual(ch2.Team2, team2);
            Assert.AreEqual(ch2.GameModeId, 0);
            Assert.AreEqual(ch2.Team1Report, true);
            Assert.AreEqual(ch2.Team2Report, false);

            Assert.IsNull(ch3.Team1Report);
            Assert.IsNull(ch3.Team2Report);
        }

        [TestMethod]
        public void VictorTest()
        {
            //Arrange
            Domain.Team team1 = new Domain.Team();
            Domain.Team team2 = new Domain.Team();
            Domain.Challenge ch1 = new Domain.Challenge(0, team1, team2, 0, true, false);
            Domain.Challenge ch2 = new Domain.Challenge(0, team1, team2, 0, false, true);
            Domain.Challenge ch3 = new Domain.Challenge(0, team1, team2, 0, true, true);
            Domain.Challenge ch4 = new Domain.Challenge(0, team1, team2, 0, false, false);
            Domain.Challenge ch5 = new Domain.Challenge(0, team1, team2, 0, null, false);
            Domain.Challenge ch6 = new Domain.Challenge(0, team1, team2, 0, true, null);
            Domain.Challenge chn = new Domain.Challenge(0, team1, team2, 0, null, null);

            bool? team1Win;
            bool? team2Win;
            bool? bothClaimWon;
            bool? bothClaimLoss;
            bool? Team1NotSubmit;
            bool? Team2NotSubmit;
            bool? bothTeamNotSubmit;


            //Act
            team1Win = ch1.Victor();
            team2Win = ch2.Victor();
            bothClaimWon = ch3.Victor();
            bothClaimLoss = ch4.Victor();
            Team1NotSubmit = ch5.Victor();
            Team2NotSubmit = ch6.Victor();
            bothTeamNotSubmit = chn.Victor();

            //Assert
            Assert.AreEqual(team1Win, true);
            Assert.AreEqual(team2Win, false);
            Assert.IsNull(bothClaimWon);
            Assert.IsNull(bothClaimLoss);
            Assert.IsNull(Team1NotSubmit);
            Assert.IsNull(Team2NotSubmit);
            Assert.IsNull(bothTeamNotSubmit);
        }

        [TestMethod]
        public void ReportTest()
        {
            //Arrange
            Domain.Team team1 = new Domain.Team();
            team1.teamname = "team1";
            Domain.Team team2 = new Domain.Team();
            team2.teamname = "team2";
            Domain.Team team3 = new Domain.Team();
            team3.teamname = "team3";
            Domain.Challenge ch1 = new Domain.Challenge(team1, team2, 0);
            Domain.Challenge ch2 = new Domain.Challenge(team1, team2, 0);
            Domain.Challenge ch3 = new Domain.Challenge(team1, team2, 0);
            Domain.Challenge ch4 = new Domain.Challenge(team1, team2, 0);
            Domain.Challenge ch5 = new Domain.Challenge(team1, team2, 0);
            bool teamNotInChallenge;
            bool team1UpdateSucceeded;
            bool team2UpdateSucceeded;

            //Act
            teamNotInChallenge = ch1.MakeReport(team3.teamname, true);
            team1UpdateSucceeded = ch2.MakeReport(team1.teamname, true);
            ch3.MakeReport(team1.teamname, false);
            team2UpdateSucceeded = ch4.MakeReport(team2.teamname, true);
            ch5.MakeReport(team2.teamname, false);


            //Assert
            Assert.IsFalse(teamNotInChallenge);
            Assert.IsTrue((bool)ch2.Team1Report);
            Assert.IsTrue(team1UpdateSucceeded);
            Assert.IsFalse((bool)ch3.Team1Report);
            Assert.IsTrue((bool)ch4.Team2Report);
            Assert.IsTrue(team2UpdateSucceeded);
            Assert.IsFalse((bool)ch5.Team2Report);

        }

    }
}
