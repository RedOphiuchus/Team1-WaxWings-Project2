using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
   public class TeamRepository : ITeamRepository
    {
        private readonly Entities.HLContext _db;
        public TeamRepository(Entities.HLContext db)
        {
            _db = db;
        }

        public bool AddTeam(Team team)
        {
            //intialize boolean that indicates if method succeeded to false
            bool success = false;
            //search database to see if the team already exists in the database
            Data.Entities.Team x = _db.Team.Where(a => a.Teamname.Equals(team.teamname)).FirstOrDefault();
            //if the team does not exist, add the team to the database
            if (x == null)
            {
                _db.Team.Add(Mapper.Map(team));
                //set boolean to true to indicate that we successfully added a team
                success = true;
            }
            return success;
        }

        public bool DeleteTeam(Team team)
        {
            //intialize boolean that indicates if method succeeded to false
            bool success = false;
            //search database to ensure that the team already exists in the database
            Data.Entities.Team x = _db.Team.Where(a => a.Teamname.Equals(team.teamname)).FirstOrDefault();
            //if the team does exist, remove the team from the database
            if (x != null)
            {
                int id = x.Id;
                _db.Team.Remove(x);
                //set boolean to true to indicate that we successfully added a team
                success = true;
            }
            return success;
        }
        public bool UpdateTeam(Team team)
        {

            bool success = false;
                List<Domain.User> usersinteam = new List<Domain.User>();
                var teamwanted = _db.Team.Where(a => a.Teamname == team.teamname).FirstOrDefault();
                var teamwantedid = teamwanted.Id;
                var usersteaminteam = _db.UserTeam.Where(a => a.Teamid == teamwantedid).Include("User");

                foreach (var userteaminteam in usersteaminteam)
                {
                    var user = _db.User.Where(b => b.Id == userteaminteam.Userid);
                    //usersinteam.Add(Mapper.Map(user);
                }
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
