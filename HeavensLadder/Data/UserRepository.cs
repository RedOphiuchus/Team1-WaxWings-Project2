

using Data.Entities;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    class UserRepository : IUserRepository
    {
        private readonly HLContext _db;
        public UserRepository(HLContext db)
        {
            _db = db;
        }
        //public IEnumerable<Domain.User> GetUsers()
        //{
        //    return _db.User.Select(x => Mapper.Map(x));
        //}
      /*  public bool AddUser(User user)
        {
            bool check = false;
            //_db.User.Add(Mapper.Map(user));
            return check;
        }*/
        public bool validatelogin(string username, string password)
        {
            bool validate = false;
            return validate;
        }
        public bool validateusername(string username)
        {
            bool validateuser = false;
            return validateuser;
        }
        //List<User> TeamUsers(team team);
    }
}