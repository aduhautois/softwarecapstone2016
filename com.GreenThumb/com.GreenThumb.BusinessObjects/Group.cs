using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Group
    {
        public int GroupID { get; set; }
        //public int Name { get; set; }//Kristine Johnson removed this by commenting it out, it should be string-see below
        public int GroupLeaderID { get; set; }
        public bool Active { get; set; }
        public IEnumerable<GroupMember> UserList { get; set; } // Modified by: Trent Cullinan
        public DateTime CreatedDate { get; set; }
        public GroupMember GroupLeader { get; set; } // Added by: Trent Cullinan
        public List<Garden> GardenList { get; set; }  //added by: Chris Schwebach

        ///Kristine Johnson Added

           
        public int OrganizationID { get; set; }
        public string Name { get; set; }
       
    
    }

}

