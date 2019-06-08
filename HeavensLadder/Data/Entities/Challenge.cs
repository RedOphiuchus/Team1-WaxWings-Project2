using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Challenge
    {
        public Challenge()
        {
            Sides = new HashSet<Sides>();
        }

        public int Id { get; set; }

        public virtual ICollection<Sides> Sides { get; set; }
    }
}
