using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class TimePledgeNeeded : TimePledge
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Object to represent TimePledgedNeeded from
        /// DB Table
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        
        public int TimeNeededID { get; set; }
        public int UserID { get; set; }
        public DateTime DateNeeded { get; set; }
        public string Location { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
