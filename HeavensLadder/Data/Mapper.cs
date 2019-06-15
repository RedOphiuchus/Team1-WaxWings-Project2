
using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Data
{
    public static class Mapper
    {
        public static Data.Entities.User Map(Domain.User dmUser)
        {
            Data.Entities.User deUser = new Entities.User();
            deUser.Id = dmUser.id;
            deUser.Username = dmUser.username;
            deUser.Password = dmUser.password;

            return deUser;
        }
        public static Domain.User Map(Data.Entities.User deUser) => new Domain.User
        {
            id = deUser.Id,
            username = deUser.Username,
            password = deUser.Password,
        };
        ////Todo Complete
        //public static Data.Entities.Challenge Map(Domain.Challenge dmChallenge)
        //{
        //    Data.Entities.Challenge deChallenge = new Entities.Challenge();
        //    deChallenge.Id = dmChallenge.id;

        //    return deChallenge;
        //}
        ////Todo Complete
        //public static Domain.Challenge Map(Data.Entities.Challenge deChallenge) => new Domain.Challenge
        //{
        //    id = deChallenge.Id,
        //};

        public static Data.Entities.Team Map(Domain.Team dmTeam)
        {
            Data.Entities.Team deTeam = new Entities.Team();
            List<Data.Entities.UserTeam> deUserTeam = new List<Entities.UserTeam>();
            if (dmTeam.Userlist.Count > 0 && dmTeam.Userlist.Count==dmTeam.Roles.Count)
            {
                for (int i = 0; i < dmTeam.Userlist.Count; i++)
                {
                    Data.Entities.UserTeam soloUserTeam = new Data.Entities.UserTeam();
                    soloUserTeam.Leader = dmTeam.Roles[i];
                    soloUserTeam.Userid = dmTeam.Userlist[i].id;
                    deUserTeam.Add(soloUserTeam);
                }
            }

            if (dmTeam.id != null)
            {
                deTeam.Id = (int)dmTeam.id;
            }
            deTeam.UserTeam = deUserTeam;
            deTeam.Teamname = dmTeam.teamname;
            return deTeam;
        }
        public static Domain.Team Map(Data.Entities.Team deTeam)
        {
            Domain.Team dmTeam = new Domain.Team();
            dmTeam.id = deTeam.Id;
            dmTeam.teamname = deTeam.Teamname;
            int i = 0;
            foreach(var item in deTeam.UserTeam)
            {
                dmTeam.Roles.Add(item.Leader);
                dmTeam.Userlist.Add(Map(item.User));
                i += 1;
            }

            foreach(var item in deTeam.Rank)
            {
                dmTeam.rank.Add(item.Rank1);
            }
            

            return dmTeam;
            
        }

        public static Data.Entities.Rank Map(Domain.Rank dmRank)
        {
            Data.Entities.Rank deRank = new Entities.Rank();
            deRank.Teamid = (int)dmRank.team.id;
            if (dmRank.id != null)
                deRank.Id = (int)dmRank.id;
            deRank.Gamemodeid = dmRank.gamemodeid;
            deRank.Rank1 = dmRank.ranking;
            deRank.Wins = dmRank.wins;
            deRank.Losses = dmRank.losses;
            return deRank;
        }

        public static Domain.Rank Map(Data.Entities.Rank deRank) => new Domain.Rank(Map(deRank.Team), deRank.Gamemode.Id)
        {
            id = deRank.Id,
            team = Map(deRank.Team),
            gamemodeid = deRank.Gamemode.Id,
            ranking = deRank.Rank1,
            wins = deRank.Wins,
            losses = deRank.Losses
        };        

        //public static Data.Entities.DirectMessage Map(Domain.DirectMessage dmDirectMessage)
        //{
        //    Data.Entities.DirectMessage deDirectMessage = new Entities.DirectMessage();
        //    deDirectMessage.id = dmDirectMessage.id;
        //    deDirectMessage.messagetime = dmDirectMessage.messagetime;
        //    deDirectMessage.sendid = dmDirectMessage.sendid;
        //    deDirectMessage.receiveid = dmDirectMessage.receiveid;

        //    return deDirectMessage;
        //}
        //public static Domain.DirectMessage Map(Data.Entities.DirectMessage deDirectMessage) => new Domain.DirectMessage
        //{
        //    id = deDirectMessage.id,
        //    messagetime = deDirectMessage.messagetime,
        //    sendid = deDirectMessage.sendid,
        //    receiveid = deDirectMessage.receiveid
        //};
    }
}