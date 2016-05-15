using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SoilNeeded : Soil
    {
        /// <summary>
        /// Author: Poonam
        /// Data Transfer Object to represent Soil Available for donation
        /// from the Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int NeededID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public SoilNeeded(int id, string name, string soilType, int userID, bool active,
            int neededID, int quantity, DateTime date)
            : base(id, name, soilType, userID, active)
        {
            this.NeededID = neededID;
            this.Quantity = quantity;
            this.Date = date;
        }
    }
}
