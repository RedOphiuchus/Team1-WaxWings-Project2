using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _TeamRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IRankRepository _RankRepository;
        public TeamController(ITeamRepository TeamRepository, IUserRepository UserRepository, IRankRepository RankRepository)
        {
            _TeamRepository = TeamRepository;
            _UserRepository = UserRepository;
            _RankRepository = RankRepository;
        }

        // GET boolean for creating a team
        [HttpPost("Create")]
        public IActionResult CreateTeam([FromBody] Models.TeamInputModel team)
        {
            string outMessage = "";

            if(ValidateTeamInput(team, out outMessage) == false)
            {
                return BadRequest(new { message = outMessage });
            }

            var user1 = _UserRepository.GetUserByUsername(team.username);
            if (user1 == null)
                return BadRequest(new { message = "User does not exist." });

            Domain.Team team1 = new Domain.Team(user1);
            team1.teamname = team.teamname;
            bool validated = _TeamRepository.AddTeam(team1);
            if (!validated)
                return StatusCode(500);

            Domain.Team madeTeam = _TeamRepository.GetByTeamName(team1.teamname); 
            bool ranksMade = false;
            ranksMade = _RankRepository.InitializeRanks(madeTeam);
            if(!ranksMade)
            {
                _TeamRepository.DeleteTeam(madeTeam);
                return StatusCode(500);
            }

            foreach(var user in madeTeam.Userlist)
            {
                user.password = null;
            }

            return Created("app/team/create", madeTeam);
        }

        private bool ValidateTeamInput(TeamInputModel team, out string message)
        {
            if (team.username == null && team.teamname == null)
            {
                message = "Invalid input: missing teamname and username.";
                return false;
            }
            if (team.teamname == null)
            {
                message = "Invalid input: missing teamname.";
                return false;
            }
            if (team.username == null)
            {
                message = "Invalid input: missing username.";
                return false;
            }
            message = "";
            return true;
        }


        // GET boolean for updating a team
        [HttpPut("Add")]
        public IActionResult UpdateTeam([FromBody] Models.TeamInputModel team)
        {
            string outMessage = "";

            if (ValidateTeamInput(team, out outMessage) == false)
            {
                return BadRequest(new { message = outMessage });
            }

            var user1 = _UserRepository.GetUserByUsername(team.username);
            if (user1 == null)
                return BadRequest(new { message = "User does not exist." });

            var team1fromdb = _TeamRepository.GetByTeamName(team.teamname);
            if (team1fromdb == null)
                return BadRequest(new { message = "Team does not exist." });

            bool validated = team1fromdb.AddMember(user1);
            if (!validated)
                return StatusCode(500);
            var validated2 = _TeamRepository.UpdateTeam(team1fromdb);
            if (!validated2)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        // GET list of strings of users in a team
        [HttpGet("Users")]
        public IActionResult UsersInTeam(string teamname)
        {
            List<string> UserNameList = new List<string>();
            List<Domain.User> UsersList = _UserRepository.TeamUsers(teamname);

            if (UsersList.Count == 0)
                return BadRequest(new { message = "Team does not exist." });
            foreach (var UserList in UsersList)
            {
                UserNameList.Add(UserList.username);
            }
            
            return Ok(UserNameList);
        }

        [HttpPut("SetLead")]
        public IActionResult ChangeLeader([FromBody] Models.TeamInputModel team)
        {
            string outMessage = "";

            if (ValidateTeamInput(team, out outMessage) == false)
            {
                return BadRequest(new { message = outMessage });
            }
            Domain.Team pullTeam = _TeamRepository.GetByTeamName(team.teamname);
            bool hasLeader = false;
            for (int i = 0; i < pullTeam.Roles.Count; i++)
            {
                pullTeam.Roles[i] = false;
                if (pullTeam.Userlist[i].username == team.username)
                {
                    pullTeam.Roles[i] = true;
                    hasLeader = true;
                }
            }

            if (!hasLeader)
                return BadRequest (new { message = "User is not on the team." });

            bool success;
            success = _TeamRepository.UpdateTeam(pullTeam);
            if (!success)
                return StatusCode(500);

            return Ok();
        }

        [HttpGet("team")]
        public IActionResult getbyteamname(string teamname)
        {
            Domain.Team thisTeam = new Domain.Team();
            thisTeam = _TeamRepository.GetByTeamName(teamname);

            if (thisTeam == null)
                return BadRequest(new { message = "Team does not exist." });
            

            return Ok(thisTeam);
        }

    }

    
}
