using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class GameModes
    {
        public GameModes()
        {
            Rank = new HashSet<Rank>();
        }

        public int Id { get; set; }
        public string Modename { get; set; }

        public virtual ICollection<Rank> Rank { get; set; }
    }
}
