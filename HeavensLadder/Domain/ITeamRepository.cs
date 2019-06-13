using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface ITeamRepository
    {
        bool AddTeam(Team team);
        bool DeleteTeam(Team team);
        bool UpdateTeam(Team team);
        //List<Team> GetChallengeTeams(Challenge chal);
        List<Team> GetUserTeams(User user);
        Team GetByTeamName(string name);

    }
}
