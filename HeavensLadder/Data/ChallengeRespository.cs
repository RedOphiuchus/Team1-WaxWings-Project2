using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

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
            if (cha == null)
                return false;

            if (_db.Challenge.Find(cha.Id) != null)
                return false;

            _db.Challenge.Add(cha);
            _db.SaveChanges();
            return true;
        }

        public List<Challenge> GetTeamChallenges(string teamname)
        {
            List<Data.Entities.Challenge> chas = _db.Challenge.Include("Sides.Team").ToList();
            List<Challenge> output = new List<Challenge>();
            foreach(var cha in chas)
            {
                if(cha.Sides.ToList()[0].Team.Teamname == teamname || cha.Sides.ToList()[1].Team.Teamname == teamname)
                    output.Add(Mapper.Map(cha));
            }

            return output;
        }

        public Challenge GetChallengeById(int id)
        {
            Data.Entities.Challenge cha = _db.Challenge.Where(c => c.Id == id).Include("Sides.Team").FirstOrDefault();
            if (cha == null)
                return null;
            Domain.Challenge outCha = Mapper.Map(cha);
            return outCha;
        }

        public List<Challenge> GetUnresolvedTeamChallenges(string teamname)
        {
            List<Data.Entities.Challenge> chas = _db.Challenge.Where(c => (c.Sides.ToList()[0].Team.Teamname == teamname) || (c.Sides.ToList()[1].Team.Teamname == teamname)).Include("Sides.Team").ToList();
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
            if (cha == null)
                return false;

            Data.Entities.Challenge dbcha;
            if ((dbcha =_db.Challenge.Find(cha.Id)) != null)
            {
                _db.Entry(dbcha).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            foreach(var side in cha.Sides)
            {
                Data.Entities.Sides rmSide;
                if((rmSide = _db.Sides.Find(side.Id)) != null)
                    _db.Entry(rmSide).State = Microsoft.EntityFrameworkCore.EntityState.Detached;          
            }
            _db.Challenge.Update(cha);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteChallenge(Challenge challenge)
        {
            Data.Entities.Challenge cha = Mapper.Map(challenge);
            if (cha == null)
                return false;

            List<Data.Entities.Sides> sides = _db.Sides.ToList();
            foreach(var side in sides)
            {
                if (side.Challengeid == cha.Id)
                    _db.Sides.Remove(side);
            }
            _db.SaveChanges();
            Data.Entities.Challenge dbcha;
            if ((dbcha = _db.Challenge.Find(cha.Id)) != null)
            {
                _db.Entry(dbcha).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            cha.Sides.Clear();
            _db.Challenge.Remove(cha);
            _db.SaveChanges();
            return true;
        }
    }
}
