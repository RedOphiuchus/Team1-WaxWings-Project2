using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class UserTeam
    {
        public int Id { get; set; }
        public int Teamid { get; set; }
        public int Userid { get; set; }
        public bool Leader { get; set; }

        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
