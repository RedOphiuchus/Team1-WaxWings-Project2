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
        public ChallengeController(IChallengeRepository ChallengeRepository)
        {
            _ChallengeRepository = ChallengeRepository;
        }

        // GET: api/Challenge
        [HttpGet("{teamname}")]
        public IEnumerable<Challenge> Get(string teamname)
        {
            var temp = _ChallengeRepository.GetTeamChallenges(teamname);
            return temp;
        }

        // POST: api/Challenge
        [HttpPost]
        public void Post(Challenge challenge)
        {
            _ChallengeRepository.AddChallenge(challenge);
        }

        // PUT: api/Challenge/5
        [HttpPut]
        public void Put(Challenge challenge)
        {
            _ChallengeRepository.UpdateChallenge(challenge);
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
