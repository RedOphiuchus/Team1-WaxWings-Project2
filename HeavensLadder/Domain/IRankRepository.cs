using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IRankRepository
    {
        List<Rank> GetAllRanks();
        List<Rank> GetRanksByMode(string gamemode);
        List<Rank> GetRanksByTeam(string teamname);
        List<Rank> GetRank(string team, string gamemode);
        bool AddRank(Rank rank);
        bool UpdateRank(Rank rank);
        bool DeleteRank(Rank rank);
        bool AlreadyExists(Rank rank);
        bool InitializeRanks(Team team);
    }
}
