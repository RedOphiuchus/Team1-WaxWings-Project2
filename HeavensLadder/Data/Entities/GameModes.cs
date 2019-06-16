using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class GameModes
    {
        public GameModes()
        {
            Challenge = new HashSet<Challenge>();
            Rank = new HashSet<Rank>();
        }

        public int Id { get; set; }
        public string Modename { get; set; }

        public virtual ICollection<Challenge> Challenge { get; set; }
        public virtual ICollection<Rank> Rank { get; set; }
    }
}
