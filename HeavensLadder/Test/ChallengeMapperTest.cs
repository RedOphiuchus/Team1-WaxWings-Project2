using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data;

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
            Data.Entities.Challenge r1 = Mapper.Map(ch1);
            Data.Entities.Challenge r2 = Mapper.Map(ch2);
            Data.Entities.Challenge r3 = Mapper.Map(ch3);
            Data.Entities.Challenge r4 = Mapper.Map(ch4);
            Data.Entities.Challenge r5 = Mapper.Map(ch5);

            //Assert
            //ch1
            Assert.AreEqual(0, r1.Id);
            int i = 0;
            foreach (var side in r1.Sides)
            {
                if (i == 0)
                    Assert.AreEqual(t1.id, side.Teamid);
                if (i == 1)
                    Assert.AreEqual(t2.id, side.Teamid);
                i++;
            }
            Assert.AreEqual(1, r1.GameModeId);


            //ch2
            Assert.AreEqual(1,r2.Id);
            i = 0;
            foreach (var side in r2.Sides)
            {
                Assert.IsNull(side.Winreport);
                if (i == 0)
                    Assert.AreEqual(t1.id, side.Teamid);
                if (i == 1)
                    Assert.AreEqual(t2.id, side.Teamid);
                i++;
            }
            Assert.AreEqual(1, r2.GameModeId);

            //ch3
            Assert.AreEqual(1, r3.Id);
            i = 0;
            foreach (var side in r3.Sides)
            {
                if (i == 0)
                {
                    Assert.AreEqual(t1.id, side.Teamid);
                    Assert.IsTrue((bool)side.Winreport);
                }
                if (i == 1)
                {
                    Assert.AreEqual(t2.id, side.Teamid);
                    Assert.IsNull(side.Winreport);
                }
                i++;
            }
            Assert.AreEqual(1, r3.GameModeId);

            //ch4
            Assert.AreEqual(1, r4.Id);
            i = 0;
            foreach (var side in r4.Sides)
            {
                if (i == 0)
                {
                    Assert.AreEqual(t1.id, side.Teamid);
                    Assert.IsNull(side.Winreport);
                }
                if (i == 1)
                {
                    Assert.AreEqual(t2.id, side.Teamid);
                    Assert.IsTrue((bool)side.Winreport);
                }
                i++;
            }
            Assert.AreEqual(1, r4.GameModeId);

            //ch5
            Assert.AreEqual(1, r5.Id);
            i = 0;
            foreach (var side in r5.Sides)
            {
                if (i == 0)
                {
                    Assert.AreEqual(t1.id, side.Teamid);
                    Assert.IsTrue((bool)side.Winreport);
                }
                if (i == 1)
                {
                    Assert.AreEqual(t2.id, side.Teamid);
                    Assert.IsFalse((bool)side.Winreport);
                }
                i++;
            }
            Assert.AreEqual(1, r5.GameModeId);
        }

        [TestMethod]
        public void DataToDomainTest()
        {
            //Arrange
            Data.Entities.Sides sideA = new Data.Entities.Sides();
            Data.Entities.Team teamA = new Data.Entities.Team();
            teamA.Teamname = "Good";
            teamA.Id = 4;
            sideA.Team = teamA;
            sideA.Winreport = true;
            

            Data.Entities.Sides sideB = new Data.Entities.Sides();
            Data.Entities.Team teamB = new Data.Entities.Team();
            teamB.Teamname = "Bad";
            teamB.Id = 7;
            sideB.Team = teamB;

            Data.Entities.Sides sideC = new Data.Entities.Sides();

            Data.Entities.Challenge ch1 = new Data.Entities.Challenge();
            ch1.Sides.Add(sideA);
            ch1.Sides.Add(sideB);
            ch1.GameModeId = 3;

            Data.Entities.Challenge ch2 = new Data.Entities.Challenge();
            ch2.Sides.Add(sideA);
            ch2.Sides.Add(sideB);
            ch2.Id = 5;
            ch2.GameModeId = 4;

            Data.Entities.Challenge ch3 = new Data.Entities.Challenge();
            ch3.Sides.Add(sideA);
            ch3.Sides.Add(sideB);
            ch3.Sides.Add(sideC);

            Data.Entities.Challenge ch4 = new Data.Entities.Challenge();


            //Act
            Domain.Challenge r1 = Mapper.Map(ch1);
            Domain.Challenge r2 = Mapper.Map(ch2);
            Domain.Challenge r3 = Mapper.Map(ch3);
            Domain.Challenge r4 = Mapper.Map(ch4);

            //Arrange
            Assert.AreEqual(0, r1.id);
            Assert.AreEqual("Good", r1.Team1.teamname);
            Assert.AreEqual("Bad", r1.Team2.teamname);
            Assert.AreEqual(4, r1.Team1.id);
            Assert.AreEqual(7, r1.Team2.id);
            Assert.AreEqual(3, r1.GameModeId);

            Assert.AreEqual(5, r2.id);

            Assert.IsNull(r3);
            Assert.IsNull(r4);


        }
    }
}
