
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
            //deUser.id = dmUser.id;
            deUser.Username = dmUser.username;
            deUser.Password = dmUser.password;

            return deUser;
        }
        public static Domain.User Map(Data.Entities.User deUser) => new Domain.User(deUser.Username, deUser.Password)
        {
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
            //deTeam.id = dmTeam.id;
            deTeam.Teamname = dmTeam.teamname;
            return deTeam;
        }
        public static Domain.Team Map(Data.Entities.Team deTeam) => new Domain.Team
        {
           // id = deTeam.id,
            teamname = deTeam.Teamname,
        };

        public static Data.Entities.Rank Map(Domain.Rank dmRank)
        {
            Data.Entities.Rank deRank = new Entities.Rank();
            deRank.id = dmRank.id;
            deRank.teamid = dmRank.teamid;
            deRank.gamemodeid = dmRank.gamemodeid;
            deRank.rank = dmRank.rank;
            deRank.wins = dmRank.wins;
            deRank.losses = dmRank.losses;

            return deRank;
        }
        public static Domain.Rank Map(Data.Entities.Rank deRank) => new Domain.Rank
        {
            id = deRank.id,
            teamid = deRank.teamid,
            gamemodeid = deRank.gamemodeid,
            rank = deRank.rank,
            wins = deRank.wins,
            losses = deRank.losses
        };
        */
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