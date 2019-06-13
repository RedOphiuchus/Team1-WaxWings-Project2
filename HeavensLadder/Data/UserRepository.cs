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
        public IEnumerable<Domain.User> GetUsers()
        {
            return _db.User.Select(x => Mapper.Map(x));
        }
        public bool AddUser(Domain.User user)
        {
            bool check = false;
            _db.User.Add(Mapper.Map(user));
            return check;
        }
        public bool validatelogin(string username, string password)
        {
            bool validate = false;
            var element = _db.User.Where(a => a.Username == username).FirstOrDefault();
            if (element.Password == password)
            {
                validate = true;
            }
            return validate;
        }
        public bool validateusername(string username)
        {
            bool validateuser = false;
            var element = _db.User.Where(a => a.Username == username).FirstOrDefault();
            if (element != null)
            {
                validateuser = true;
            }
            return validateuser;
        }
        public List<Domain.User> TeamUsers(string teamname)
        {
            List<Domain.User> usersinteam = new List<Domain.User>();
            var teamwanted = _db.Team.Where(a => a.Teamname == teamname).FirstOrDefault();
            var teamwantedid = teamwanted.Id;

            return usersinteam;
        }
    }
}