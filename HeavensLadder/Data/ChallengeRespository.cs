using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain;

namespace Data
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly Data.Entities.HLContext _db;

        public ChallengeRepository(Data.Entities.HLContext db)
        {
            _db = db;
        }

        public bool AddChallenge(Challenge challenge)
        {
            Data.Entities.Challenge cha = Mapper.Map(challenge);
            if (cha != null)
                return false;

            if (_db.Find<Data.Entities.Challenge>(cha) != null)
                return false;

            _db.Challenge.Add(cha);
            _db.SaveChanges();
            return true;
        }

        public List<Challenge> GetTeamChallenges(string teamname)
        {
            List<Data.Entities.Challenge> chas = _db.Challenge.Where(c => (c.Sides.ToList()[0].Team.Teamname == teamname) || (c.Sides.ToList()[1].Team.Teamname == teamname)).ToList();
            List<Challenge> output = new List<Challenge>();
            foreach(var cha in chas)
            {
                output.Add(Mapper.Map(cha));
            }

            return output;
        }

        public List<Challenge> GetUnresolvedTeamChallenges(string teamname)
        {
            List<Data.Entities.Challenge> chas = _db.Challenge.Where(c => (c.Sides.ToList()[0].Team.Teamname == teamname) || (c.Sides.ToList()[1].Team.Teamname == teamname)).ToList();
            chas = chas.Where(c => (c.Sides.ToList()[0].Winreport == null) || (c.Sides.ToList()[1].Winreport == null)).ToList();
            List<Challenge> output = new List<Challenge>();
            foreach (var cha in chas)
            {
                output.Add(Mapper.Map(cha));
            }

            return output;
        }

        public bool UpdateChallenge(Challenge challenge)
        {
            Data.Entities.Challenge cha = Mapper.Map(challenge);
            if (cha != null)
                return false;

            Data.Entities.Challenge dbcha;
            if ((dbcha =_db.Find<Data.Entities.Challenge>(cha)) != null)
            {
                _db.Entry(dbcha).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _db.Challenge.Update(cha);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteChallenge(Challenge challenge)
        {
            Data.Entities.Challenge cha = Mapper.Map(challenge);
            if (cha != null)
                return false;

            Data.Entities.Challenge dbcha;
            if ((dbcha = _db.Find<Data.Entities.Challenge>(cha)) != null)
            {
                _db.Entry(dbcha).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _db.Challenge.Remove(cha);
            _db.SaveChanges();
            return true;
        }
    }
}
