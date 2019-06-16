using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    interface IChallengeRepository
    {
        List<Challenge> GetTeamChallenges(string teamname);
        List<Challenge> GetUnresolvedTeamChallenges(string teamname);
        bool UpdateChallenge(Challenge challenge);
        bool AddChallenge(Challenge challenge);
    }
}
