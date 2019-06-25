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
        public IActionResult Get(string teamname)
        {
            var ranks = _RankRepository.GetRanksByTeam(teamname);
            if(ranks.Count == 0)
            {
                return BadRequest(new { message = "Team does not exist." });
            }

            return Ok(ranks);
            
        }

        // GET: api/world/Rank returns all ranks in the world by gamemode
        [HttpGet("world/{gamemode}")]
        public IActionResult Get(int gamemode)
        {
            var ranks = _RankRepository.GetRanksByMode(gamemode);
            if (ranks.Count == 0)
            {
                return BadRequest(new { message = "No ranks exist yet." });
            }

            ranks = ranks.OrderByDescending(r => r.ranking).ToList();

            return Ok(ranks);
        }
    }
    
}
