using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 02/24/2016
    /// </summary>
    public class GroupLeaderRequest
    {
        public int RequestID { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
