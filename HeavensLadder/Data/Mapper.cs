
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
        public static Data.Entities.Challenge Map(Domain.Challenge dmChallenge)
        {
            Data.Entities.Challenge deChallenge = new Entities.Challenge();
            if(dmChallenge.id != null)
                deChallenge.Id = (int)dmChallenge.id;

            Data.Entities.Sides SideA = new Data.Entities.Sides();
            if (dmChallenge.sideAId != null)
            {
                SideA.Id = (int)dmChallenge.sideAId;
            }
            //SideA.Teamid = dmChallenge.Team1.id;
            SideA.Winreport = dmChallenge.Team1Report;
            deChallenge.Sides.Add(SideA);

            Data.Entities.Sides SideB = new Data.Entities.Sides();
            if(dmChallenge.sideBId != null)
            {
                SideB.Id = (int) dmChallenge.sideBId;
            }
            //SideB.Teamid = dmChallenge.Team2.id;
            SideB.Winreport = dmChallenge.Team2Report;
            deChallenge.Sides.Add(SideB);

            if(dmChallenge.GameModeId != null)
                deChallenge.GameModeId = (int) dmChallenge.GameModeId;
            return deChallenge;
        }
        ////Todo Complete
        public static Domain.Challenge Map(Data.Entities.Challenge deChallenge)
        {
            List<Domain.Team> teams = new List<Domain.Team>();
            List<bool?> results = new List<bool?>();
            foreach(var side in deChallenge.Sides)
            {
                teams.Add(Map(side.Team));
                results.Add(side.Winreport);
            }
            Domain.Challenge dmChallenge = new Domain.Challenge(deChallenge.Id, teams[0], teams[1], (int)deChallenge.GameModeId, results[0], results[1]);
            return dmChallenge;
        }

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
            deRank.Team = Map(dmRank.team);
            Data.Entities.GameModes deGame = new Entities.GameModes();
            deGame.Modename = dmRank.gamemode;
            deRank.Gamemode = deGame;
            deRank.Rank1 = dmRank.ranking;
            deRank.Wins = dmRank.wins;
            deRank.Losses = dmRank.losses;

            return deRank;
        }

        public static Domain.Rank Map(Data.Entities.Rank deRank) => new Domain.Rank(Map(deRank.Team), deRank.Gamemode.Modename)
        {
            team = Map(deRank.Team),
            gamemode = deRank.Gamemode.Modename,
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