using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Soil : Item
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Basic Data Transfer Object to represent Soil from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public string SoilType { get; set; }
        public int UserID { get; set; }
        public bool Active { get; set; }

        public Soil(int id, string name,
            string soilType, int userID, bool active)
            : base (id, name)
        {
            this.SoilType = soilType;
            this.UserID = userID;
            this.Active = active;
        }
    }
}
