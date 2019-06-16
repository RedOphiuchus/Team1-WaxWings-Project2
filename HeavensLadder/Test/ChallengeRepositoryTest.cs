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
    }
}
