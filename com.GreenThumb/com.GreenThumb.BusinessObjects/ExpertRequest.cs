using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Represents a request from a User to be promoted an Expert role.
    /// 
    /// Created By: Trent Cullinan 03/15/2016
    /// </summary>
    public class ExpertRequest
    {
        public int RequestID { get; set; }
        public char[] RequestStatus { get; set; }
        public User User { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestTitle { get; set; }
        public string RequestContent { get; set; }
    }
}
