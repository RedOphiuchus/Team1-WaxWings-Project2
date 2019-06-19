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

        // GET boolean for login
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

        // GET boolean for login
        [HttpPut("Add")]
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

        // GET boolean for login
        [HttpGet("Add/{teamname}")]
        public ActionResult<List<Domain.User>> UsersInTeam(string teamname)
        {
            List<Domain.User> UserList = _UserRepository.TeamUsers(teamname);

            return UserList;
        }


        /*  // GET: api/Team
          [HttpGet]
          public IEnumerable<string> Get()
          {
              return new string[] { "value1", "value2" };
          }

          // GET: api/Team/5
          [HttpGet("{id}", Name = "Get")]
          public string Get(int id)
          {
              return "value";
          }

          // POST: api/Team
          [HttpPost]
          public void Post([FromBody] string value)
          {
          }

          // PUT: api/Team/5
          [HttpPut("{id}")]
          public void Put(int id, [FromBody] string value)
          {
          }

          // DELETE: api/ApiWithActions/5
          [HttpDelete("{id}")]
          public void Delete(int id)
          {
          }

      */
    }

    
}
