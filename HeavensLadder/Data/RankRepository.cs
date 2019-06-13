using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Data
{
    class RankRepository : IRankRepository
    {
        private readonly Entities.HLContext _db;
        public RankRepository(Entities.HLContext db)
        {
            _db = db;
        }
        public List<Rank> GetAllRanks()
        {
            //return _db.Rank.Select(x => Mapper.Map(x));
            throw new NotImplementedException();
        }

        public List<Rank> GetRank(string teamname, string gamemode)
        {/*
            var list = GetRanksByTeam(team);
            var rank;
            foreach (var r in list)
            {
                if (r.gamemode == gamemode)
                    rank = r;
            }
            //var element = _db.Rank.Where(a => a.team.name.Contains(team.name)).FirstOrDefault();

            if (rank != null)
                return Mapper.Map(rank);
            else
                return null;*/
            throw new NotImplementedException();
        }

        public List<Rank> GetRanksByMode(string gamemode)
        {
            throw new NotImplementedException();
        }

        public List<Rank> GetRanksByTeam(string team)
        {
            throw new NotImplementedException();
        }
    }
}
