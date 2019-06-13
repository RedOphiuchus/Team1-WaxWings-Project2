using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Data
{
    class TeamRepository : ITeamRepository
    {
        private readonly Entities.HLContext _db;
        public TeamRepository(Entities.HLContext db)
        {
            _db = db;
        }

        public bool AddTeam(Team team)
        {
            bool success = false;
           // _db.Team.Add(Mapper.Map(team));
            return success;
        }

        public bool DeleteTeam(Team team)
        {
            bool success = false;
            return success;
        }
        public bool UpdateTeam(Team team)
        {
            bool success = false;
            return success;
        }
        public List<Team> GetUserTeams(User user)
        {
            List<Team> x = new List<Team>();
            return x;
        }

        public Team GetByTeamName(string name)
        {
            Team something = new Team();
            return something;
        }



    }
}
