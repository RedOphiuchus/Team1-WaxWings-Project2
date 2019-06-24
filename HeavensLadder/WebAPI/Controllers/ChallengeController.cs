using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeRepository _ChallengeRepository;
        private readonly ITeamRepository _TeamRepository;
        private readonly IRankRepository _RankRepository;

        public ChallengeController(IChallengeRepository ChallengeRepository, ITeamRepository TeamRepository, IRankRepository RankRepository)
        {
            _ChallengeRepository = ChallengeRepository;
            _TeamRepository = TeamRepository;
            _RankRepository = RankRepository;
        }

        // GET: api/Challenge
        [HttpGet("{teamname}")]
        public IActionResult Get(string teamname)
        {
            if (teamname == null)
                return BadRequest("Needs a teamname");

            var temp = _ChallengeRepository.GetUnresolvedTeamChallenges(teamname);
            if (temp.Count == 0)
                return BadRequest(new { message = "Team has no active challenges." });

            foreach (var cha in temp)
            {
                foreach (var user in cha.Team1.Userlist)
                {
                    user.password = null;
                }

                foreach (var user in cha.Team2.Userlist)
                {
                    user.password = null;
                }
            }
            return Ok(temp);
        }

        // POST: api/Challenge
        [HttpPost("Add")]
        public IActionResult Post([FromBody] Models.ChallengeSubmitModel challenge)
        {
            //query for team1
            Team team1 = _TeamRepository.GetByTeamName(challenge.team1);
            Team team2 = _TeamRepository.GetByTeamName(challenge.team2);
            if (team1 == null)
                return BadRequest(new { message = "Improper input: first team does not exist." });
            if (team2 == null)
                return BadRequest(new { message = "Improper input: second team does not exist." });

            Challenge newChallenge = new Challenge(team1,team2,challenge.gamemode);
            bool success = _ChallengeRepository.AddChallenge(newChallenge);
            if (!success)
                return StatusCode(500);

            List<Challenge> challenges = _ChallengeRepository.GetUnresolvedTeamChallenges(challenge.team1);
            Challenge createdChallenge = null;
            foreach (var cha in challenges)
            {
                if (cha.Team2.teamname == challenge.team2 && cha.GameModeId == challenge.gamemode)
                    createdChallenge = cha;
            }

            if (createdChallenge == null)
                return StatusCode(500);

            foreach(var user in createdChallenge.Team1.Userlist)
            {
                user.password = null;
            }

            foreach (var user in createdChallenge.Team2.Userlist)
            {
                user.password = null;
            }

            return Created("api/challenge/add", createdChallenge);
        }

        // PUT: api/Challenge/5
        [HttpPut("report")]
        public IActionResult Put(Models.ChallengeUpdateModel model)
        {
            Challenge chal = _ChallengeRepository.GetChallengeById(model.challengeid);
            if (chal == null)
                return BadRequest("Challege does not exist.");

            //compare teamnames
            bool success = chal.MakeReport(model.teamname, model.report);

            if (!success)
                return BadRequest("The team is not a part of this challenge.");

            bool update = _ChallengeRepository.UpdateChallenge(chal);
            if (!update)
                return StatusCode(500);
            //TODO if both reports are submitted, update ranks
            if (chal.Team1Report == null || chal.Team2Report == null)
                return Ok(new { message = "Result submitted. Waiting for other team." });

            bool? isDecided = chal.Victor();
            if (isDecided == null)
                return Ok(new { message = "Result submitted. Results don't match. No victor determined." });

            if((bool)isDecided)
            {
                List<Rank> ranks = _RankRepository.GetRanksByTeam(chal.Team1.teamname);
                Rank rank1 = null;
                foreach(var r in ranks)
                {
                    if (r.gamemodeid == chal.GameModeId)
                        rank1 = r;
                }
                rank1.AddWin();

                ranks = _RankRepository.GetRanksByTeam(chal.Team2.teamname);
                Rank rank2 = null;
                foreach (var r in ranks)
                {
                    if (r.gamemodeid == chal.GameModeId)
                        rank2 = r;
                }
                rank2.AddLoss();

                _RankRepository.UpdateRank(rank1);
                _RankRepository.UpdateRank(rank2);

                return Ok();
            }
            else
            {
                List<Rank> ranks = _RankRepository.GetRanksByTeam(chal.Team1.teamname);
                Rank rank1 = null;
                foreach (var r in ranks)
                {
                    if (r.gamemodeid == chal.GameModeId)
                        rank1 = r;
                }
                rank1.AddLoss();

                ranks = _RankRepository.GetRanksByTeam(chal.Team2.teamname);
                Rank rank2 = null;
                foreach (var r in ranks)
                {
                    if (r.gamemodeid == chal.GameModeId)
                        rank2 = r;
                }
                rank2.AddWin();

                _RankRepository.UpdateRank(rank1);
                _RankRepository.UpdateRank(rank2);

                return Ok();
            }
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
