using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Team
    {
        public Team()
        {
            Rank = new HashSet<Rank>();
            Sides = new HashSet<Sides>();
            UserTeam = new HashSet<UserTeam>();
        }

        public int Id { get; set; }
        public string Teamname { get; set; }

        public virtual ICollection<Rank> Rank { get; set; }
        public virtual ICollection<Sides> Sides { get; set; }
        public virtual ICollection<UserTeam> UserTeam { get; set; }
    }
}
