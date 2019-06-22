using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ChallengeUpdateModel
    {
        public string teamname { get; set; }
        public int challengeid { get; set; }
        public bool report { get; set; }

        public ChallengeUpdateModel(string teamname, int challengeid, bool report)
        {
            this.teamname = teamname;
            this.challengeid = challengeid;
            this.report = report;
        }
    }
}
