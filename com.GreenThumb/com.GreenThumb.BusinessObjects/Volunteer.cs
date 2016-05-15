using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.GreenThumb.BusinessObjects
{
    public class Volunteer
    {
     
       
        public int NeedID{get; set;}
        public int UserID {get; set;}
        public DateTime DateWillVolunteer  { get; set; }
        public string Description { get; set;  }
        public bool Active { get; set; }

        public Volunteer()
        {

        }

        public Volunteer(int NeedID, int UserID, DateTime DateWillVolunteer,  bool Active)
        {
            this.NeedID = NeedID;
            this.UserID = UserID;
            this.DateWillVolunteer = DateWillVolunteer;
            this.Active = Active;

        }
    }
 }
