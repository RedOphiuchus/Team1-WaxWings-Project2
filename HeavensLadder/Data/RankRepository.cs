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
            if (AlreadyExists(rank))
                return false;
            _db.Rank.Add(Mapper.Map(rank));
            Save();
            return true;
        }

        public bool DeleteRank(Rank rank)
        {
            if (!AlreadyExists(rank))
                return false;
            _db.Rank.Remove(Mapper.Map(rank));
            Save();
            return true;
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
            List<Rank> ranklist = new List<Rank>();
            var elems = _db.Rank;
            foreach (var elem in elems)
            {
                ranklist.Add(Mapper.Map(elem));
            }
            return ranklist;
        }

        public List<Rank> GetRank(string teamname, string gamemode)
        {/*
            var ranklist = GetRanksByTeam(teamname);
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
            List<Rank> ranklist = new List<Rank>();
            var allranks = GetAllRanks();
            foreach (var elem in allranks)
            {
                if (elem.gamemode == gamemode)
                    ranklist.Add(elem);
            }
            return ranklist;
        }

        public List<Rank> GetRanksByTeam(string team)
        {
            List<Rank> ranklist = new List<Rank>();
            var allranks = GetAllRanks();
            foreach (var elem in allranks)
            {
                if (elem.team.teamname == team)
                    ranklist.Add(elem);
            }
            return ranklist;
        }

        public bool UpdateRank(Rank rank)
        {
            if (!AlreadyExists(rank))
                return false;
            _db.Update(Mapper.Map(rank));
            Save();
            return true;
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

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
