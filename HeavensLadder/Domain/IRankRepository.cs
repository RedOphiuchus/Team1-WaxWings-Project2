using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IRankRepository
    {
        List<Rank> GetAllRanks();
        List<Rank> GetRanksByMode(int gamemode);
        List<Rank> GetRanksByTeam(string teamname);
        Rank GetRank(string team, int gamemode);
        bool AddRank(Rank rank);
        bool UpdateRank(Rank rank);
        bool DeleteRank(Rank rank);
        bool AlreadyExists(Rank rank);
        bool InitializeRanks(Team team);
        void Save();
    }
}
