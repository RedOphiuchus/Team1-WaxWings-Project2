using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IChallengeRepository
    {
        List<Challenge> GetTeamChallenges(string teamname);
        List<Challenge> GetUnresolvedTeamChallenges(string teamname);
        Challenge GetChallengeById(int id);
        bool UpdateChallenge(Challenge challenge);
        bool AddChallenge(Challenge challenge);
    }
}
