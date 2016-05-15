using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class GardenGuide
    {
        /// <summary>
        /// Author: Luke Frahm
        /// Data Transfer Object to represent a Garden Guide
        /// from the Database
        /// </summary>

        public int GardenGuideID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }

        public GardenGuide() { }

        public GardenGuide(int gardenGuideID, int userID, string content)
        {
            GardenGuideID = gardenGuideID;
            UserID = userID;
            Content = content;
        }
    }
}
