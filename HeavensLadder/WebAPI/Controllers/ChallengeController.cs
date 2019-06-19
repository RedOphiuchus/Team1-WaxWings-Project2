using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeRepository _ChallengeRepository;
        private readonly ITeamRepository _TeamRepository;

        public ChallengeController(IChallengeRepository ChallengeRepository, ITeamRepository TeamRepository)
        {
            _ChallengeRepository = ChallengeRepository;
            _TeamRepository = TeamRepository;
        }

        // GET: api/Challenge
        [HttpGet("{teamname}")]
        public IEnumerable<Challenge> Get(string teamname)
        {
            var temp = _ChallengeRepository.GetUnresolvedTeamChallenges(teamname);
            return temp;
        }

        // POST: api/Challenge
        [HttpPost]
        public ActionResult Post(string teamname1, string teamname2, int gamemodeid)
        {
            //query for team1
            Team team1 = _TeamRepository.GetByTeamName(teamname1);
            Team team2 = _TeamRepository.GetByTeamName(teamname2);
            if (team1 != null && team2 != null)
            {
                Challenge challenge = new Challenge(team1,team2,gamemodeid);
                _ChallengeRepository.AddChallenge(challenge);
                return Ok();
            }
            else
            {
                return BadRequest();   
            }
        }

        // PUT: api/Challenge/5
        [HttpPut]
        public void Put(string teamname, int challengeid, bool report)
        {
            Challenge chal = _ChallengeRepository.GetChallengeById(challengeid);
            //compare teamnames
            if(chal.Team1.teamname.Equals(teamname))
            {
                chal.Team1Report = report;
            }
            if(chal.Team2.teamname.Equals(teamname))
            {
                chal.Team2Report = report;
            }
            if (chal != null)
            {
                _ChallengeRepository.UpdateChallenge(chal);
            }
            //TODO if both reports are submitted, update ranks
        }


        /*
        // GET: api/Challenge
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Challenge/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Challenge
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Challenge/5
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
