using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ChallengeSubmitModel
    {
        public string team1 { get; set; }
        public string team2 { get; set; }
        public int gamemode { get; set; }

        public ChallengeSubmitModel(string team1, string team2, int gamemode)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.gamemode = gamemode;
        }
    }
}
