using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        Data.Entities.HLContext _db;
        Data.UserRepository test;

        //[TestMethod]
        //public void validatelogintest(string username, string password)
        //{
        //    Data.Entities.HLContext _db = new Data.Entities.HLContext();
        //    Data.UserRepository test = new Data.UserRepository(_db);
        //    bool expected = false;
        //    var Users = test.GetUsers();
        //    foreach (var User in Users)
        //    {
        //        if (User.username == username)
        //        {
        //            if (User.password == password)
        //            {
        //                expected = true;
        //            }
        //        }
        //    }
        //    bool actual = test.validatelogin(username, password);
        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod]
        public void validateusernametest()
        {
            Data.Entities.HLContext _db = new Data.Entities.HLContext();
            Data.UserRepository test = new Data.UserRepository(_db);
            bool expected = false;
            string username = "akash";
            Domain.User user1 = new Domain.User("akash", "1234");
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
        }

        //[TestMethod]
        //public void addusertest(Domain.User user)
        //{
        //    Data.Entities.HLContext _db = new Data.Entities.HLContext();
        //    Data.UserRepository test = new Data.UserRepository(_db);
        //    bool expected = false;
        //    test.AddUser(user); //Add Save command probably
        //    test.Save();
        //    var Users = test.GetUsers();
        //    foreach (var User in Users)
        //    {
        //        if (User.username == user.username)
        //        {
        //            expected = true;
        //        }
        //    }
        //    bool actual = test.validateusername(user.username);

        //    Assert.AreEqual(expected, actual);
        //}
    }
}
