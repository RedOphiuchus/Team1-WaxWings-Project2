using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Test
{
    [TestClass]
    public class MapperUserTest
    {
        [TestMethod]
        public void usermapperdetest()
        {
            string username2 = "Push";
            string password2 = "Pinder";
            Domain.User user2 = new Domain.User(username2, password2);
            var deuser1 = Mapper.Map(user2);
            Assert.AreEqual(deuser1.Username, username2);
            Assert.AreEqual(deuser1.Password, password2);
            Assert.AreEqual(deuser1.Id, 0);
        }

        [TestMethod]
        public void usermapperdmtest()
        {
            string username2 = "Push";
            string password2 = "Pinder";
            Data.Entities.User user2 = new Data.Entities.User();
            user2.Username = username2;
            user2.Password = password2;
            var dmuser1 = Mapper.Map(user2);
            Assert.AreEqual(dmuser1.username, username2);
            Assert.AreEqual(dmuser1.password, password2);
            Assert.AreEqual(dmuser1.id, 0);
        }
    }
}
