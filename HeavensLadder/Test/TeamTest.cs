using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class Teamtest
    {
        
        //test for first parametrized constructor
        [TestMethod]
        public void TestDomTeam()
        {
            string username = "testusername";
            string password = "testpassword";
            User user1 = new User(username,password);

            Team test1 = new Team();
            test1.Userlist.Add(user1);
            test1.Roles.Add(true);

            Team test2 = new Team();
            test2.CreateTeam(user1);

            Assert.AreEqual(test1, test2);
        }
        


            /*
        //test for second parameterized constructor
        [TestMethod]
        public void TestDomTeam2()
        {

            List<DomUser> userlist = new List<DomUser>();
            List<Boolean> roleslist = new List<Boolean>();

            DomUser user1 = new DomUser();
            user1.username = "testusername";
            user1.password = "testpassword";

            userlist.Add(user1);
            roleslist.Add(true);

            Team test1 = new Team();
            test1.UserList = userlist;
            test1.Roles = roleslist;

            Team test2 = DomTeam(userlist,roleslist);
            Assert.AreEqual(test1, test2);
        }
        */

            /*
        [TestMethod]
        public void AddMemberTest()
        {
            //test for case where user does not exist in the team
            Team test1 = new Team();
            Team test2 = new Team();
            DomUser testuser1 = new DomUser();
            testuser1.username = "username1";
            testuser1.password = "password1";

            bool success = test1.AddMember(testuser1);

            test2.UserList.Add(testuser1);
            test2.Roles.Add(false);

            Assert.AreEqual(test1, test2);

            //test case for where user does exist in the team
            success = test1.AddMember(testuser1);
            test2.UserList.Add(testuser1);
            test2.Roles.Add(false);

            Assert.AreNotEqual(test1, test2);


        }
        

        [TestMethod]
        public void RemoveMemberTest()
        {
            Team test1 = new Team();
            Team test2 = new Team();
            DomUser testuser1 = new DomUser();
            testuser1.username = "username1";
            testuser1.password = "password1";

            bool success = test1.AddMember(testuser1);
            success = test1.RemoveMember(testuser1);

            Assert.AreEqual(test1, test2);

            //check for when removemember fails
            DomUser testuser2 = new DomUser();
            success = test1.RemoveMember(testuser2);
            Assert.AreEqual(test1, test2);
        }

        public void SetLeaderTest()
        {
            DomUser user1 = new DomUser();
            DomUser user2 = new DomUser();
            user2.username = "testing1";
            DomUser user3 = new DomUser();
            DomUser user4 = new DomUser();

            Team team1 = new Team();
            team1.AddMember(user1);
            team1.AddMember(user2);
            team1.AddMember(user3);
            team1.AddMember(user4);

            team1.Roles.Add(true);
            team1.Roles.Add(false);
            team1.Roles.Add(false);
            team1.Roles.Add(false);

            team1.setLeader(user2);
            Assert.AreEqual(true, team1.Roles[1]);


        }


        public void getLeaderTest()
        {
            DomUser user1 = new DomUser();
            DomUser user2 = new DomUser();
            user2.username = "testing1";
            DomUser user3 = new DomUser();
            DomUser user4 = new DomUser();

            DomUser teamleader = new DomUser();

            Team team1 = new Team();
            team1.AddMember(user1);
            team1.AddMember(user2);
            team1.AddMember(user3);
            team1.AddMember(user4);

            team1.Roles.Add(true);
            team1.Roles.Add(false);
            team1.Roles.Add(false);
            team1.Roles.Add(false);

            teamleader = team1.getLeader();
            Assert.AreEqual(teamleader,user2);

        }


    */
    }
}
