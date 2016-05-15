using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SoilDonated : Soil
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Transfer Object to represent Soil to be Donated from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int DonatedID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public SoilDonated(int id, string name, string soilType, int userID, bool active,
            int donatedID, int quantity, DateTime date)
            : base(id, name, soilType, userID, active)
        {
            this.DonatedID = donatedID;
            this.Quantity = quantity;
            this.Date = date;
        }
    }
}
