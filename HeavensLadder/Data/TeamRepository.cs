using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
   public class TeamRepository : ITeamRepository
    {
        private readonly Entities.HLContext _db;
        public TeamRepository(Entities.HLContext db)
        {
            _db = db;
        }

        public bool AddTeam(Team team)
        {
            //intialize boolean that indicates if method succeeded to false
            bool success = false;
            //search database to see if the team already exists in the database
            Data.Entities.Team x = _db.Team.Where(a => a.Teamname.Equals(team.teamname)).FirstOrDefault();
            //if the team does not exist, add the team to the database
            if (x == null)
            {
                _db.Team.Add(Mapper.Map(team));
                //set boolean to true to indicate that we successfully added a team
                _db.SaveChanges();
                success = true;
            }
            //now, we need to add its userteams
            //now, I want to add the elements from this team into the userteam table

            List<User> DomainUser = team.Userlist;
            List<bool> DomainRole = team.Roles;

            //reobtain the current team from the database to obtain its ID
            Data.Entities.Team DBteam = _db.Team.Where(a => a.Teamname.Equals(team.teamname)).FirstOrDefault();

            for (int i = 0; i < DomainUser.Count; i++)
            {
                //determine the ID for each individual user in the team
                Data.Entities.User DBUser = _db.User.Where(z => z.Username.Equals(DomainUser[i])).FirstOrDefault();
                if (DBUser == null)
                {
                    //if the user didnt exist, we do not add any userteams
                }
                else
                {
                    //here we add each user into the userteam table
                    Data.Entities.UserTeam userteam = new Data.Entities.UserTeam();
                    userteam.Teamid = DBteam.Id;
                    userteam.Leader = DomainRole[i];
                    userteam.Userid = DBUser.Id;
                    _db.UserTeam.Add(userteam);
                    _db.SaveChanges();
                }
            }

            return success;
        }

        public bool DeleteTeam(Team team)
        {
            //intialize boolean that indicates if method succeeded to false
            bool success = false;
            //search database to ensure that the team already exists in the database
            Data.Entities.Team x = _db.Team.Where(a => a.Teamname.Equals(team.teamname)).FirstOrDefault();

            //if the team does exist, remove the team from the database, but first, delete the userteams associated with it
            if (x != null)
            {
                IEnumerable<Data.Entities.UserTeam> y = x.UserTeam;
                if (y != null)
                {
                    foreach (var item in y)
                    {
                        _db.UserTeam.Remove(item);

                    }
                    _db.SaveChanges();
                }
            }
            if (x != null)
            {
                _db.Team.Remove(x);
                _db.SaveChanges();
                //set boolean to true to indicate that we successfully added a team
                success = true;
            }
            return success;
        }
        public bool UpdateTeam(Team team)
        {
            bool success = true;
            int i = 0; //index for loops
            //first determine the ID of the team that we are updating
            Data.Entities.Team DBTeam = _db.Team.Where(z => z.Teamname.Equals(team.teamname)).FirstOrDefault();

            //ensure that our DBteam is not null, we have an equal number of roles and users, and that we have more than 0 roles
            if (DBTeam != null && team.Roles.Count==team.Userlist.Count && team.Roles.Count>0)
            {
                int DBTeamID = DBTeam.Id;
                //next, determine the elements in the USERTEAM table that have that teams ID
                List<Data.Entities.UserTeam> DBUserTeam = _db.UserTeam.Where(p => p.Teamid == DBTeamID).ToList();
                //now, remove those elements from the userteam table
                if (DBUserTeam.Count > 0)
                {
                    foreach (var item in DBUserTeam)
                    {
                        _db.UserTeam.Remove(item);
                    }
                }
                //now, I want to add the elements from this team into the userteam table

                List<User> DomainUser = team.Userlist;
                List<bool> DomainRole = team.Roles;

                for(i=0; i<DomainUser.Count; i++)
                {
                    //determine the ID for each individual user in the team
                    Data.Entities.User DBUser = _db.User.Where(z => z.Username.Equals(DomainUser[i].username)).FirstOrDefault();
                    if (DBUser == null)
                    {
                        //if the user didnt exist, we want to stop the program and return false
                        success = false;
                        return success;
                    }
                    else
                    {
                        //here we add each user into the userteam table
                        Data.Entities.UserTeam userteam = new Data.Entities.UserTeam();
                        userteam.Teamid = DBTeamID;
                        userteam.Leader = DomainRole[i];
                        userteam.Userid = DBUser.Id;
                        _db.UserTeam.Add(userteam);
                        _db.SaveChanges();
                    }
                }

            }
            else
            {
                success = false;
                return success;
            }

            return success;
        }
        public List<Team> GetUserTeams(User user)
        {
            List<Team> x = new List<Team>(); //list to store teams and ultimately return
            int i = 0; //index variable for loops
            List<int> teamIDs = new List<int>(); //list to store team IDs to try and create team objects

            //first get the users user ID (primary key)
            Data.Entities.User DBUser = _db.User.Where(z => z.Username.Equals(user.username)).FirstOrDefault();
            if (DBUser != null)
            {
                int userID = DBUser.Id;

                //now, get all of the UserTeam entities that contain that specific userID
                List<Data.Entities.UserTeam> DBUserTeam = _db.UserTeam.Where(y => y.Userid == userID).ToList();
                //now, loop through the UserTeam table and obtain all of the teamIDs
                if(DBUserTeam != null)
                {
                    for (i = 0; i < DBUserTeam.Count; i++)
                        teamIDs.Add(DBUserTeam[i].Teamid);

                }
            }

            //now we have a list of the Team IDs, so we can go ahead and try to construct the teams using it
            //first lets make a loop
            for (i = 0; i<teamIDs.Count; i++)
            {
                //step1, create a team object
                Team team = new Team(); //i dont know if naming them all the same thing will cause an issue

                //step 2 get the team name
                Data.Entities.Team deteam = _db.Team.Where(q => q.Id == teamIDs[i]).FirstOrDefault();

                team.teamname = deteam.Teamname;

                //step 3 get the other stuff, like roles, users
                //unfortunately, its not that easy, gotta first query and get a list of UserTeams
                List<Data.Entities.UserTeam> deUserTeam = _db.UserTeam.Where(b => b.Teamid == teamIDs[i]).ToList();

                //step 4, now i gotta loop through the list of userTeams to get some datas for my team
                for(i = 0; i>deUserTeam.Count; i++)
                {
                    //now I want to add the user and the role, role is easy
                    team.Roles.Add(deUserTeam[i].Leader);
                    //user is a bit more complicated, I need to make the user object
                    //so first I will make a query to get the user entity
                    Data.Entities.User deuser = _db.User.Where(w => w.Id == deUserTeam[i].Userid).FirstOrDefault();

                    //now I can make a new user object and add it to my userlist.
                    User use = new User(deuser.Username, deuser.Password);
                    team.Userlist.Add(use);



                }
                x.Add(team);

            }

            //and finally return x, a list of teams after all that jumping around!
            return x;
        }

        public Team GetByTeamName(string name)
        {
            Team something = new Team(); //initialize team object!
                                         
            int i = 0;//index variable for loops                             
            //get team id from the name
            Data.Entities.Team deteam = _db.Team.Where(h => h.Teamname.Equals(name)).FirstOrDefault();
            int teamID = deteam.Id;

            List<Data.Entities.UserTeam> deUserTeam = _db.UserTeam.Where(b => b.Teamid == teamID).ToList();

            //step 4, now i gotta loop through the list of userTeams to get some datas for my team
            for (i = 0; i < deUserTeam.Count; i++)
            {
                //now I want to add the user and the role, role is easy
                something.Roles.Add(deUserTeam[i].Leader);
                //user is a bit more complicated, I need to make the user object
                //so first I will make a query to get the user entity
                Data.Entities.User deuser = _db.User.Where(w => w.Id == deUserTeam[i].Userid).FirstOrDefault();

                //now I can make a new user object and add it to my userlist.
                User use = new User(deuser.Username, deuser.Password);
                something.Userlist.Add(use);



            }


            something.teamname = deteam.Teamname;

            //add team id to the team
            something.id = teamID;
           
            
            return something;
        }



    }
}
