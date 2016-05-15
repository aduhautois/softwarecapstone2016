using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.GreenThumb.BusinessObjects
{
    public class VolunteerNeeds
    {
        
        public string NeedName { get; set; }
       
        
        public int GardenID{get; set;}
        public DateTime DateNeedBy{get; set;}
        public bool Active { get; set; }

        public VolunteerNeeds(string NeedName,  int GardenID, int NeedID,  DateTime DateNeedBy, bool Active)
        {
            this.NeedName = NeedName;
            
            this.GardenID = GardenID;
            this.DateNeedBy = DateNeedBy;
            this.Active = Active;
        }
    }
}