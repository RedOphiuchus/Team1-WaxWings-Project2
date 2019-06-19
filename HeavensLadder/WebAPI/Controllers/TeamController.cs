using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _TeamRepository;
        private readonly IUserRepository _UserRepository;
        public TeamController(ITeamRepository TeamRepository, IUserRepository UserRepository)
        {
            _TeamRepository = TeamRepository;
            _UserRepository = UserRepository;
        }

        // GET boolean for creating a team
        [HttpPost("Create/{teamname}/{username}")]
        public ActionResult<bool> CreateTeam(string teamname,string username)
        {
            var user1 = _UserRepository.GetUserByUsername(username);
            Domain.Team team1 = new Domain.Team(user1);
            team1.teamname = teamname;
            bool validated = _TeamRepository.AddTeam(team1);
            bool success = false;
            if (validated == true)
            {
                success = true;
            }
            

            return success;
        }

        // GET boolean for updating a team
        [HttpPut("Add/{teamname}/{username}")]
        public ActionResult<bool> UpdateTeam(string teamname, string username)
        {
            var user1fromdb = _UserRepository.GetUserByUsername(username);
            var team1fromdb = _TeamRepository.GetByTeamName(teamname);
            bool validated = team1fromdb.AddMember(user1fromdb);
            bool success = false;
            if (validated == true)
            {
                var validated2 = _TeamRepository.UpdateTeam(team1fromdb);
                if (validated2 == true)
                {
                    success = true;
                }
            }
            return success;
        }

        // GET list of strings of users in a team
        [HttpGet("Add/{teamname}")]
        public ActionResult<List<string>> UsersInTeam(string teamname)
        {
            List<string> UserNameList = new List<string>();
            List<Domain.User> UsersList = _UserRepository.TeamUsers(teamname);

            if (UsersList != null)
            {
                foreach (var UserList in UsersList)
                {
                    UserNameList.Add(UserList.username);
                }
            }
            return UserNameList;
        }

    }

    
}
