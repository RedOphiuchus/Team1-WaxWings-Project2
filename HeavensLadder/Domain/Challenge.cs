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

        Challenge(int id, Team team1, Team team2, string gameMode, bool? team1Report, bool? team2Report)
        {
            this.id = id;
            Team1 = team1;
            Team2 = team2;
            GameMode = gameMode;
            Team1Report = team1Report;
            Team2Report = team2Report;
        }

        bool? Victor()
        {
            if (Team1Report == null || Team2Report == null)
                return null;

            if (Team1Report == true && Team2Report == false)
                return true;
            if (Team1Report == false && Team2Report == true)
                return false;

            return null;
        }

        bool MakeReport(Team team, bool result)
        {
            if(team == Team1)
            {
                Team1Report = result;
                return true;
            }
            if(team == Team2)
            {
                Team2Report = result;
                return true;
            }
            return false;
        }
    }
}
