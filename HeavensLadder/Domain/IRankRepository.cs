using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IRankRepository
    {
        List<Rank> GetAllRanks();
        List<Rank> GetRanksByMode(string gamemode);
        List<Rank> GetRanksByTeam(Team team);
        List<Rank> GetRank(Team team, string gamemode);
    }
}
