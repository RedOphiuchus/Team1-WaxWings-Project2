using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        bool validatelogin(string username, string password);
        bool validateusername(string username);
        //List<User> TeamUsers(Team team);
        //IEnumerable<User> GetUsers();

    }
}