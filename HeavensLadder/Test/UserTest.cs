using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class UserTest
    {
        
        //test for first parametrized constructor
        [TestMethod]
        public void TestUser()
        {
            //Arrange
            string usernamecheck = "Akash";
            string passwordcheck = "1234";

            //Act
            User user1 = new User("Akash", "1234");

            //Assert
            Assert.AreEqual(usernamecheck, user1.username);
            Assert.AreEqual(passwordcheck, user1.password);

        }
        
    }
}