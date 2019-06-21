using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TeamInputModel
    {
        public string username { get; set; }
        public string teamname { get; set; }

        public TeamInputModel(string username, string teamname)
        {
            this.username = username;
            this.teamname = teamname;
        }
    }
}
