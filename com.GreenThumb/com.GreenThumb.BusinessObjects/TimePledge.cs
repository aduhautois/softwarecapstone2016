using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class TimePledge
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Transfer Object to represent Time Pledged from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public string GardenAffiliation { get; set; }
        public bool Active { get; set; }
    }
}
