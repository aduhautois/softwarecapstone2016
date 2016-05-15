using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class GardenPlan
    {
        /// <summary>
        /// Author: Luke Frahm
        /// Data Transfer Object to represent a Garden Plan
        /// from the Database
        /// </summary>

        public int GardenPlanID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }

        public GardenPlan() { }

        public GardenPlan(int gardenPlanID, int userID, string description, DateTime dateCreated, bool active)
        {
            GardenPlanID = gardenPlanID;
            UserID = userID;
            Description = description;
            DateCreated = dateCreated;
            Active = active;
        }
    }
}
