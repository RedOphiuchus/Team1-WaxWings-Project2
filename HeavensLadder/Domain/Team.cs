using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Team
    {

        public string teamname;
        public List<Boolean> Roles;
        public List<User> Userlist;
        public List<int> rank;
        public int? id;



        //zeroth parameterized constructor
        public Team()
        {
            Userlist = new List<User>();
            Roles = new List<bool>();
            teamname = "";
            rank = new List<int>();
        }

        //first parameterized constructor

    public Team(User user1)
    {
        Userlist = new List<User>();
        Roles = new List<bool>();
        Userlist.Add(user1);
        Roles.Add(true); //set first user as team leader
        teamname = "";
        rank = new List<int>();
    }



       public Team(List<User> userlist, List<Boolean> roleslist)
        {
            this.Userlist = userlist;
            this.Roles = roleslist;
            this.teamname = "";
            rank = new List<int>();
        }
   

        
       public bool AddMember(User user)
       {
           
           Boolean success = true;
           int i = 0; //index variable

           //step 1: determine if the user is already on the team
            for (i = 0; i < this.Roles.Count(); i++)
            {
                //determine if person we are trying to add exists in team
                if (Userlist[i].username.Equals(user.username))
                {
                    success = false;
                }
            }

        if(success == true)
            {
           this.Userlist.Add(user);
           this.Roles.Add(false);//add member to role list
           
            }
            return success;

       }
     


    
    public bool RemoveMember(User user)
    {
        Boolean success = false;
        int i = 0; //index variable for loops
        int leaderindex = -1; //index variable to see where leader is
        int removeindex = -2; //index variable to see where removed user is

        //search for leader and check to see if user is in the team
        //roles and userlist  should have the same number of elements
        //so this means I can use this loop to access both lists
        for (i = 0; i < this.Roles.Count(); i++)
        {
            //find the roles index of the leader
            if (Roles[i] == true)
            {
                leaderindex = i;
            }
            //determine if person we are trying to remove exists in team
            if (Userlist[i].username.Equals(user.username))
            {
                success = true;
                removeindex = i;
            }
        }

        //now, we can actually try to move the member
        if(success == true)
        {
            //we remove the person from the team
            Roles.RemoveAt(removeindex);
            Userlist.RemoveAt(removeindex);

            //now we have extra logic for if the leader was removed
            //we basically set the first element on the team to leader
            //unsure if i should do some kind of prompt to notify a leader change
            if (removeindex == leaderindex)
            {
                Roles[0] = true;

            }

        }
            return success;

    } 


     
public bool setLeader(User user)
    {
        bool success = true;
        int i = 0;
        int leaderindex = -1;
        //first determine if user is in the team
        for (i = 0; i < this.Roles.Count(); i++)
        {
            //determine if person we are trying to make leader exists in team
            if (Userlist[i].username.Equals(user.username))
            {
                success = true;
                leaderindex = i;
            }
        }
        //first make all users in the team a member
        if(success == true)
        {
            for (i = 0; i < this.Roles.Count(); i++)
            {
                Roles[i] = false;
            }

        }
        //set the index of the leader to true
        Roles[leaderindex] = true;

        return success;
    }



        
      public User getLeader()
      {
          int leaderindex = -1;
          int i = 0;

          //first find the index of the leader
          for (i = 0; i < this.Roles.Count(); i++)
          {
              if(Roles[i] == true)
              {
                  leaderindex = i;
              }
          }

          return this.Userlist[leaderindex];


      }
      



    }
}
