using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Sides
    {
        public int Id { get; set; }
        public int Challengeid { get; set; }
        public int Teamid { get; set; }
        public bool Winreport { get; set; }

        public virtual Challenge Challenge { get; set; }
        public virtual Team Team { get; set; }
    }
}
