using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RankController : ControllerBase
    {
        //intialize repository objects so that we can use the repository methods
        private readonly IRankRepository _RankRepository;
        public RankController(IRankRepository RankRepository)
        {
            _RankRepository = RankRepository;
        }

        // GET: api/Rank returns all ranks for a given team
        [HttpGet("{teamname}")]
        public IEnumerable<Rank> Get(string teamname)
        {
            return _RankRepository.GetRanksByTeam(teamname);
            
        }

        // GET: api/world/Rank returns all ranks in the world by gamemode
        [HttpGet("world/{gamemode}")]
        public IEnumerable<Rank> Get(int gamemode)
        {
            return _RankRepository.GetRanksByMode(gamemode);
        }

        

        // POST: api/Rank
        [HttpPost]
        public void Post(Rank rank)
        {
            _RankRepository.AddRank(rank);
        }



        /*

        // GET: api/Rank/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Rank
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Rank/5
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
