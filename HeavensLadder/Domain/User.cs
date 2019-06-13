using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public User()
        {
            this.username = "";
            this.password = "";
        }
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public void UserFill(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
    
}