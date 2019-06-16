using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class DirectMessage
    {
        public int Id { get; set; }
        public DateTime? Messagetime { get; set; }
        public int Sendid { get; set; }
        public int Recieveid { get; set; }

        public virtual User Recieve { get; set; }
        public virtual User Send { get; set; }
    }
}
