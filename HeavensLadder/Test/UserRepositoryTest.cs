using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        Data.Entities.HLContext _db;
        Data.UserRepository test;

        [TestMethod]
        public void getuserstest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            string username1 = "Edwin";
            string password1 = "password";
            string username2 = "Push";
            string password2 = "Pinder";
            bool expected = true;
            bool actual = false;
            int userid = 0;
            Domain.User user1 = new Domain.User(username1, password1);
            Domain.User user2 = new Domain.User(username2, password2);
            test.AddUser(user1);
            test.AddUser(user2);
            test.Save();
            var Users = test.GetUsers();
            foreach (var User in Users)
            {
                //if (User.username == username1)
                //{
                //    userid = User.id;
                //}
                actual = test.validateusername(User.username);
                if (actual == false)
                {
                    break;
                }
            }

            Assert.AreEqual(expected, actual);
            test.DeleteUser(user1);
            test.DeleteUser(user2);
            test.Save();
        }

        [TestMethod]
        public void getuserbyidtest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            string username = "Edwin";
            string password = "password";
            string expected = username;
            int userid = 0;
            Domain.User user1 = new Domain.User(username, password);
            test.AddUser(user1);
            test.Save();
            var Users = test.GetUsers();
            foreach (var User in Users)
            {
                if (User.username == username)
                {
                    userid = User.id;
                }
            }
            string actual = test.GetUserByUserid(userid).username;

            Assert.AreEqual(expected, actual);
            test.DeleteUser(user1);
            test.Save();
        }

        [TestMethod]
        public void validatelogintest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            bool expected = false;
            string username = "Edwin";
            string password = "password";
            Domain.User user1 = new Domain.User();
            user1.UserFill(username, password);
            test.AddUser(user1);
            test.Save();
            var Users = test.GetUsers();
            foreach (var User in Users)
            {
                if (User.username == username)
                {
                    if (User.password == password)
                    {
                        expected = true;
                    }
                }
            }
            bool actual = test.validatelogin(username, password);
            Assert.AreEqual(expected, actual);
            test.DeleteUser(user1);
            test.Save();
        }

        [TestMethod]
        public void validateusernametest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            bool expected = false;
            string username = "akash";
            Domain.User user1 = new Domain.User();
            user1.UserFill("akash", "1234");
            test.AddUser(user1);
            test.Save();
            var Users = test.GetUsers();
            foreach (var User in Users)
            {
                if (User.username == username)
                {
                    expected = true;
                }
            }
            bool actual = test.validateusername(username);

            Assert.AreEqual(expected, actual);
            test.DeleteUser(user1);
            test.Save();
        }

        [TestMethod]
        public void addusertest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            bool expected = false;
            string username = "Sera";
            string password = "something";
            Domain.User user1 = new Domain.User();
            user1.UserFill(username, password);
            test.AddUser(user1);
            test.Save();
            var Users = test.GetUsers();
            foreach (var User in Users)
            {
                if (User.username == user1.username)
                {
                    expected = true;
                }
            }
            bool actual = test.validateusername(user1.username);

            Assert.AreEqual(expected, actual);
            test.DeleteUser(user1);
            test.Save();
        }

        [TestMethod]
        public void deleteusertest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            bool expected = false;
            string username = "Carlos";
            string password = "something";
            Domain.User user1 = new Domain.User();
            user1.UserFill(username, password);
            test.AddUser(user1);
            test.Save();
            int userid = 0;
            var Users = test.GetUsers();
            foreach (var User in Users)
            {
                if (User.username == username)
                {
                    userid = User.id;
                }
            }
            test.DeleteUser(test.GetUserByUserid(userid));
            test.Save();
            bool actual = test.validateusername(user1.username);

            Assert.AreEqual(expected, actual);
            if (actual == true)
            {
                test.DeleteUser(user1);
                test.Save();
            }
        }
        
        //[TestMethod]
        //public void teamuserstest()
        //{
        //    Data.Entities.HLContext _db = new Data.Entities.HLContext();
        //    Data.UserRepository test = new Data.UserRepository(_db);
            
        //    string username = "Carlos";
        //    string password = "something";
        //    Domain.User user1 = new Domain.User();
        //    user1.UserFill(username, password);
        //    string username2 = "Akash";
        //    string password2 = "other";
        //    Domain.User user2 = new Domain.User();
        //    user1.UserFill(username2, password2);
        //    Domain.Team team1 = new Domain.Team(user1);
        //    team1.teamname = "Team1";
        //    team1.AddMember(user2);
            

        //}

        [TestMethod]
        public void savetest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            bool expected = false;
            string username = "Carlos";
            string password = "something";
            Domain.User user1 = new Domain.User();
            user1.UserFill(username, password);
            test.AddUser(user1);
            test.Save();
            int userid = 0;
            var Users = test.GetUsers();
            foreach (var User in Users)
            {
                if (User.username == username)
                {
                    userid = User.id;
                    expected = true;
                }
            }
            
            bool actual = test.validateusername(test.GetUserByUserid(userid).username);

            Assert.AreEqual(expected, actual);
            test.DeleteUser(test.GetUserByUserid(userid));
            test.Save();
        }

    }
}
