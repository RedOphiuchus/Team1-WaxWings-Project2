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
            bool UsernamesEqual = true;
            string username = "testusername";
            string password = "testpassword";
            User user1 = new User(username,password);

            Team test1 = new Team();
            test1.Userlist.Add(user1);
            test1.Roles.Add(true);

            Team test2 = new Team(user1);

            if (test1.Userlist[0].username.Equals(test2.Userlist[0].username))
            {
                UsernamesEqual = true;
            }

            Assert.AreEqual(UsernamesEqual,true);
        }
        


            
        //test for second parameterized constructor
        [TestMethod]
        public void TestDomTeam2()
        {
            
            string username = "testusername";
            string password = "testpassword";
            List<User> userlist = new List<User>();
            List<Boolean> roleslist = new List<Boolean>();

            User user1 = new User(username,password);
            

            userlist.Add(user1);
            roleslist.Add(true);

            Team test1 = new Team();
            test1.Userlist = userlist;
            test1.Roles = roleslist;
            Team test2 = new Team(userlist, roleslist);

          
            Assert.AreEqual(test1.Roles[0], test2.Roles[0]);
        }
        

            
        [TestMethod]
        public void AddMemberTest()
        {
            bool UsernamesEqual = false;
            string username = "testusername";
            string password = "testpassword";
            //test for case where user does not exist in the team
            Team test1 = new Team();
            Team test2 = new Team();
            User testuser1 = new User(username,password);

            bool success = test1.AddMember(testuser1);

            test2.Userlist.Add(testuser1);
            test2.Roles.Add(false);
            if(test1.Userlist[0].username.Equals(test2.Userlist[0].username))
            {
                UsernamesEqual = true;
            }

            Assert.AreEqual(UsernamesEqual, true);

            //test case for where user does exist in the team
            success = test1.AddMember(testuser1);
            test2.Userlist.Add(testuser1);
            test2.Roles.Add(false);

            Assert.AreNotEqual(test1.Userlist.Count, test2.Userlist.Count);


        }
        


        [TestMethod]
        public void RemoveMemberTest()
        {
            string username = "testusername";
            string password = "testpassword";
            Team test1 = new Team();
            Team test2 = new Team();
            User testuser1 = new User(username,password);
            User testuser2 = new User("testusername1", "testpassword1");

            //check for when removemember passes
            bool success = test1.AddMember(testuser1);
            success = test1.AddMember(testuser2);
            success = test1.RemoveMember(testuser1);

            success = test2.AddMember(testuser1);

            Assert.AreEqual(test1.Userlist.Count, test2.Userlist.Count);

            //check for when removemember fails
            User testuser3 = new User("testusername2","testpassword2");
            success = test1.RemoveMember(testuser3);
            if (success == false)
            {
                Assert.AreEqual(test1.Userlist.Count, test2.Userlist.Count);
            }
            else
            {
                Assert.AreEqual(true, false);
            }
           
        }

        public void SetLeaderTest()
        {
            User user1 = new User("username1","password1");
            User user2 = new User("username2", "password2");
            User user3 = new User("username3", "password3");
            User user4 = new User("username4", "password4");

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
            User user1 = new User("username1", "password1");
            User user2 = new User("username2", "password2");
            User user3 = new User("username3", "password3");
            User user4 = new User("username4", "password4");

            User teamleader = new User(null,null);

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


    
    }
}
