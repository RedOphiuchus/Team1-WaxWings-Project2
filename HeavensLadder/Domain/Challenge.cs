using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Challenge
    {
        public int? id { get; set; }

        public int? sideAId { get; set; }

        public int? sideBId { get; set; }

        public Team Team1 { get; set; }

        public Team Team2 { get; set; }
        public bool? Team1Report { get; set; }

        public bool? Team2Report { get; set; }

        public int? GameModeId { get; set; }


        public Challenge(Team team1, Team team2, int gameMode)
        {
            Team1 = team1;
            Team2 = team2;
            GameModeId = gameMode;
        }

        public Challenge(int id, Team team1, Team team2, int gameMode, bool? team1Report, bool? team2Report)
        {
            this.id = id;
            Team1 = team1;
            Team2 = team2;
            GameModeId = gameMode;
            Team1Report = team1Report;
            Team2Report = team2Report;
        }

        public bool? Victor()
        {
            if (Team1Report == null || Team2Report == null)
                return null;

            if (Team1Report == true && Team2Report == false)
                return true;
            if (Team1Report == false && Team2Report == true)
                return false;

            return null;
        }

        public bool MakeReport(Team team, bool result)
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
