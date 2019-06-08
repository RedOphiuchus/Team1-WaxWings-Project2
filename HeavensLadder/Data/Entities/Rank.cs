using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Rank
    {
        public int Id { get; set; }
        public int Teamid { get; set; }
        public int Gamemodeid { get; set; }
        public int Rank1 { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public virtual GameModes Gamemode { get; set; }
        public virtual Team Team { get; set; }
    }
}
