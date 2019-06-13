using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IUserRepository
    {
        IEnumerable<Domain.User> GetUsers();
        User GetUserByUserid(int userid);
        bool AddUser(Domain.User user);
        bool DeleteUser(Domain.User user);
        bool validatelogin(string username, string password);
        bool validateusername(string username);
        List<Domain.User> TeamUsers(string teamname);
        void Save();
    }
}