using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class ChallengeMapperTest
    {
        [TestMethod]
        public void TestDomainToData()
        {
            //Arrange
            Domain.Team t1 = new Domain.Team();
            Domain.Team t2 = new Domain.Team();
            t1.id = 5;
            t2.id = 1;

            //Create a fresh challenge to add to the database
            Domain.Challenge ch1 = new Domain.Challenge(t1,t2,1);

            //Update an existing challenge to the database
            Domain.Challenge ch2 = new Domain.Challenge(1,t1,t2,1,null,null);
            ch2.sideAId = 2;
            ch2.sideBId = 5;
            Domain.Challenge ch3 = new Domain.Challenge(1, t1, t2, 1, true, null);
            Domain.Challenge ch4 = new Domain.Challenge(1, t1, t2, 1, null, true);
            Domain.Challenge ch5 = new Domain.Challenge(1, t1, t2, 1, true, false);


            //Act




        }
    }
}
