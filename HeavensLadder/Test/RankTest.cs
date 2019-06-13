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
            User user = new User("testuser", "pw");
            Team team = new Team(user);
        }
    }
}
