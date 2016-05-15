using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/14/16
    /// </summary>
    public class NeedContribution
    {
        public int NeedContributionID { get; set; }
        public GardenNeed Need { get; set; }              // Wrap the Need unqiue identifier
        public User SentBy { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }
        public bool? Contributed { get; set; }
    }
}
