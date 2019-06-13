using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IUserRepository
    {
        IEnumerable<Domain.User> GetUsers();
        bool AddUser(Domain.User user);
        bool validatelogin(string username, string password);
        bool validateusername(string username);
        List<Domain.User> TeamUsers(string teamname);
    }
}