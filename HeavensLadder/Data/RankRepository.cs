using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using System.Linq;

namespace Data
{
    public class RankRepository : IRankRepository
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
            if (_db.Rank.Find(rank.id) != null)
            {
                _db.Entry(_db.Rank.Find(rank.id)).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _db.Rank.Remove(Mapper.Map(rank));
            Save();
            return true;
        }

        public bool AlreadyExists(Rank rank)
        {
            var team = rank.team;
            var game = rank.gamemodeid;
            var ranklist = _db.Rank.Where(x => x.Id >= 0).ToList();
            if (ranklist.Count == 0)
                return false;
            foreach (Data.Entities.Rank elem in ranklist)
            {
                if (elem != null)
                {
                    if (elem.Teamid == team.id && elem.Gamemodeid == game)
                        return true;
                }
            }
            return false;
        }

        public List<Rank> GetAllRanks()
        {
            List<Rank> ranklist = new List<Rank>();
            //changed this part to return all ranks in the db.
            var elems = _db.Rank.Where(x => x.Id >= 0).ToList();
            foreach (var elem in elems)
            {
                ranklist.Add(Mapper.Map(elem));
            }
            return ranklist;
        }

        public Rank GetRank(string teamname, int gamemode)
        {
            Team team = new Team();
            team.teamname = teamname;
            var ranklist = GetRanksByTeam(teamname);
            Rank rank = null;
            foreach (var r in ranklist)
            {
                if (r.gamemodeid == gamemode)
                    rank = r;
            }

            if (rank != null)
                return rank;
            else
                return null;
        }

        public List<Rank> GetRanksByMode(int gamemode)
        {
            List<Rank> ranklist = new List<Rank>();
            var allranks = GetAllRanks();
            foreach (var elem in allranks)
            {
                if (elem.gamemodeid == gamemode)
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
            if (_db.Rank.Find(rank.id) != null)
            {
                _db.Entry(_db.Rank.Find(rank.id)).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
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
                rank = new Rank(team, elem.Id);
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
