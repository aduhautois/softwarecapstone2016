using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class TimePledgePending : TimePledge
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Object to represent TimePledged that is 
        /// pending for commitment
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int TimePledgeID { get; set; }
        public int TimeNeededID { get; set; }
        public DateTime DateNeeded { get; set; }
        public DateTime DateMatched { get; set; }
    }
}
