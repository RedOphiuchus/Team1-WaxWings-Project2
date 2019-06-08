using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class User
    {
        public User()
        {
            DirectMessageRecieve = new HashSet<DirectMessage>();
            DirectMessageSend = new HashSet<DirectMessage>();
            UserTeam = new HashSet<UserTeam>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<DirectMessage> DirectMessageRecieve { get; set; }
        public virtual ICollection<DirectMessage> DirectMessageSend { get; set; }
        public virtual ICollection<UserTeam> UserTeam { get; set; }
    }
}
