using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Garden
    {
        /// <summary>
        /// Author: Luke Frahm
        /// Data Transfer Object to represent a Garden
        /// from the Database
        /// </summary>

        public int GardenID { get; set; }
        public int GroupID { get; set; }
        public string GardenName { get; set; }  //Added by Chris Schwebach
        public int UserID { get; set; }
        public string GardenDescription { get; set; }
        public string GardenRegion { get; set; }

        public bool Active { get; set; }    //Added by Chris Schwebach
        public Garden() { }

        public Garden(int gardenID, int groupID, string gardenName, int userID, string gardenDescription, string gardenRegion, bool active)
        {
            GardenID = gardenID;
            GroupID = groupID;
            GardenName = gardenName;
            UserID = userID;
            GardenDescription = gardenDescription;
            GardenRegion = gardenRegion;
            Active = active;
        }
    }
}
