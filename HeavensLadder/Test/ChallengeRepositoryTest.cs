using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data;
using Domain;

namespace Test
{
    [TestClass]
    public class ChallengeRepositoryTest
    {
        [TestMethod]
        public void AddGetByTeamNameTest()
        {
            //Arrange
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            ChallengeRepository ChRepo = new ChallengeRepository(_db);
            TeamRepository TRepo = new TeamRepository(_db);
            bool addSuccess;
            Challenge outCha;

            Team team1 = new Team();
            team1.teamname = "Test1";
            TRepo.AddTeam(team1);
            Team Test1 = TRepo.GetByTeamName(team1.teamname);

            Team team2 = new Team();
            team2.teamname = "Test2";
            TRepo.AddTeam(team2);
            Team Test2 = TRepo.GetByTeamName(team2.teamname);


            Challenge cha = new Challenge(Test1, Test2, 1);

            //Act

            addSuccess = ChRepo.AddChallenge(cha);
            outCha = ChRepo.GetTeamChallenges(team1.teamname).FirstOrDefault();
            ChRepo.DeleteChallenge(outCha);
            TRepo.DeleteTeam(team1);
            TRepo.DeleteTeam(team2);

            //Assert
            Assert.IsTrue(addSuccess);
            Assert.AreEqual(team1.teamname, outCha.Team1.teamname);
            Assert.AreEqual(team2.teamname, outCha.Team2.teamname);
            Assert.AreEqual(1, outCha.GameModeId);

        }

        [TestMethod]
        public void AddGetUnresolvedTeamChallenges()
        {
            //Arrange
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            ChallengeRepository ChRepo = new ChallengeRepository(_db);
            TeamRepository TRepo = new TeamRepository(_db);
            bool add1Success;
            bool add2Success;
            List<Challenge> outChas;

            Team team1 = new Team();
            team1.teamname = "Test1";
            TRepo.AddTeam(team1);
            Team Test1 = TRepo.GetByTeamName(team1.teamname);

            Team team2 = new Team();
            team2.teamname = "Test2";
            TRepo.AddTeam(team2);
            Team Test2 = TRepo.GetByTeamName(team2.teamname);


            Challenge cha1 = new Challenge(Test1, Test2, 1);
            Challenge cha2 = new Challenge(0, Test1, Test2, 1, true, false);

            //Act

            add1Success = ChRepo.AddChallenge(cha1);
            add2Success = ChRepo.AddChallenge(cha2);

            outChas = ChRepo.GetUnresolvedTeamChallenges(team1.teamname);
            foreach (var cha in outChas)
            {
                ChRepo.DeleteChallenge(cha);
            }
            List<Challenge> tempChas = ChRepo.GetTeamChallenges(team1.teamname);
            foreach (var cha in tempChas)
            {
                ChRepo.DeleteChallenge(cha);
            }
            TRepo.DeleteTeam(team1);
            TRepo.DeleteTeam(team2);

            //Assert
            Assert.IsTrue(add1Success);
            Assert.IsTrue(add2Success);
            Assert.AreEqual(1, outChas.Count);
            Assert.AreEqual("Test1", outChas[0].Team1.teamname);
        }

        [TestMethod]
        public void GetChallengeByIdTest()
        {
            //Arrange
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            ChallengeRepository ChRepo = new ChallengeRepository(_db);
            TeamRepository TRepo = new TeamRepository(_db);
            Challenge outCha;
            Challenge badCha;
            Team team1 = new Team();
            team1.teamname = "Test1";
            TRepo.AddTeam(team1);
            Team Test1 = TRepo.GetByTeamName(team1.teamname);

            Team team2 = new Team();
            team2.teamname = "Test2";
            TRepo.AddTeam(team2);
            Team Test2 = TRepo.GetByTeamName(team2.teamname);


            Challenge cha1 = new Challenge(Test1, Test2, 1);

            ChRepo.AddChallenge(cha1);
            int id = (int)ChRepo.GetTeamChallenges(Test1.teamname).FirstOrDefault().id;
            //Act

            outCha = ChRepo.GetChallengeById(id);
            badCha = ChRepo.GetChallengeById(-1);

            ChRepo.DeleteChallenge(outCha);

            //Assert
            Assert.IsNotNull(outCha);
            Assert.IsNull(badCha);
        }

        [TestMethod]
        public void UpdateChallengeTest()
        {
            //Arrange
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            ChallengeRepository ChRepo = new ChallengeRepository(_db);
            TeamRepository TRepo = new TeamRepository(_db);
            bool updateSuccess;
            Challenge outCha;

            Team team1 = new Team();
            team1.teamname = "Test1";
            TRepo.AddTeam(team1);
            Team Test1 = TRepo.GetByTeamName(team1.teamname);

            Team team2 = new Team();
            team2.teamname = "Test2";
            TRepo.AddTeam(team2);
            Team Test2 = TRepo.GetByTeamName(team2.teamname);


            Challenge cha1 = new Challenge(Test1, Test2, 1);
            ChRepo.AddChallenge(cha1);
            Challenge pullCha = ChRepo.GetTeamChallenges(team1.teamname).FirstOrDefault();
            pullCha.MakeReport(pullCha.Team1.teamname, true);
            pullCha.MakeReport(pullCha.Team2.teamname, false);

            //Act
            updateSuccess = ChRepo.UpdateChallenge(pullCha);
            outCha = ChRepo.GetTeamChallenges(team1.teamname).FirstOrDefault();

            List<Challenge> tempChas = ChRepo.GetTeamChallenges(team1.teamname);
            foreach (var cha in tempChas)
            {
                ChRepo.DeleteChallenge(cha);
            }
            TRepo.DeleteTeam(team1);
            TRepo.DeleteTeam(team2);

            //Assert
            Assert.IsTrue(updateSuccess);
            Assert.IsTrue((bool)outCha.Team1Report);
            Assert.IsFalse((bool)outCha.Team2Report);

        }
    }
}
