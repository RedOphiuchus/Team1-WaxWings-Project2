using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    class Challenge
    {
        int id { get; set; }

        Team Team1 { get; set; }

        Team Team2 { get; set; }
        bool? Team1Report { get; set; }

        bool? Team2Report { get; set; }

        string GameMode { get; set; }

        Challenge(Team team1, Team team2, string gameMode)
        {
            Team1 = team1;
            Team2 = team2;
            GameMode = gameMode;
        }
    }
}
