﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            string team1name = "akashteam";
            Domain.Team team1 = new Domain.Team();
            team1.teamname = team1name;
            team1.id = 1;
            var deteam1 = Mapper.Map(team1);
            Assert.AreEqual(deteam1.Teamname, team1name);
            Assert.AreEqual(deteam1.Id, 1);
        }

        [TestMethod]
        public void teammapperdmtest()
        {
            string team1name = "akashteam";
            Data.Entities.Team team1 = new Data.Entities.Team();
            team1.Teamname = team1name;
            team1.Id = 35;
            var dmteam1 = Mapper.Map(team1);
            Assert.AreEqual(dmteam1.teamname, team1name);
            Assert.AreEqual(dmteam1.id, team1.Id);
        }
    }
}
