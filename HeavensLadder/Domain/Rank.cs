using System;

namespace Domain
{
    public class Rank
    {
        public int? id { get; set; }
        public int ranking { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public Team team { get; set; }
        public int gamemodeid { get; set; }

        /// <summary>
        /// This will create a Rank class for a particular team and gamemode.
        /// It will have wins, losses, and ranking at 0 until after one game has been completed.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public Rank(Team t, int game)
        {
            team = t;
            gamemodeid = game;
            wins = 0;
            losses = 0;
            ranking = 0; 
        }

        public void AddWin()
        {
            this.wins += 1;
        }

        public void AddLoss()
        {
            this.losses += 1;
        }

        public decimal CalculateRank()
        {
            int totalgames = this.wins + this.losses;
            if (totalgames == 0)
                return 0;
            //add logic for calculating rank here, possibly using a Bernoulli parameter
            return 0;
        }
    }
}
