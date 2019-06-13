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

        public bool AddRank(Rank rank)
        {
            /*_db.Rank.Add(Mapper.Map(rank));
            _db.SaveChanges();
            return true;*/
            throw new NotImplementedException();
        }

        public bool DeleteRank(Rank rank)
        {
            throw new NotImplementedException();
        }

        public bool AlreadyExists(Rank rank)
        {
            var name = rank.team.teamname;
            var game = rank.gamemode;
            var ranklist = _db.Rank;
            foreach (var elem in ranklist)
            {
                if (elem.Team.Teamname == name && elem.Gamemode.Modename == game)
                    return true;
            }
            return false;
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

        public bool UpdateRank(Rank rank)
        {
            throw new NotImplementedException();
        }

        public bool InitializeRanks(Team team)
        {
            Rank rank;
            var gamemodes = _db.GameModes;        
            foreach (var elem in gamemodes)
            {
                rank = new Rank(team, elem.Modename);
                if (AlreadyExists(rank))
                    return false;
                AddRank(rank);
            }
            return true;
        }
    }
}
