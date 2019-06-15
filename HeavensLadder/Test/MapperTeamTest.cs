using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Test
{
    [TestClass]
    public class MapperTeamTest
    {
        [TestMethod]
        public void teammapperdetest()
        {
            //string username2 = "Push";
            //string password2 = "Pinder";
            //Domain.User user2 = new Domain.User(username2, password2);
            //var deuser1 = Mapper.Map(user2);
            string team1name = "akashteam";
            Domain.Team team1 = new Domain.Team();
            team1.teamname = team1name;
            var deteam1 = Mapper.Map(team1);
            Assert.AreEqual(deteam1.Teamname, team1name);
            //Assert.AreEqual(deuser1.Password, password2);
            //Assert.AreEqual(deuser1.Id, 0);
        }

        [TestMethod]
        public void teammapperdmtest()
        {
            //string username2 = "Push";
            //string password2 = "Pinder";
            //Data.Entities.User user2 = new Data.Entities.User();
            //user2.Username = username2;
            //user2.Password = password2;
            //var dmuser1 = Mapper.Map(user2);

            string team1name = "akashteam";
            Data.Entities.Team team1 = new Data.Entities.Team();
            team1.Teamname = team1name;
            var dmteam1 = Mapper.Map(team1);
            Assert.AreEqual(dmteam1.teamname, team1name);
            //Assert.AreEqual(dmuser1.password, password2);
            //Assert.AreEqual(dmuser1.id, 0);
        }
    }
}
