using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Extending User object to have the fields that GroupMembers have.
    /// Created by: Trent Cullinan 02/20/2016
    /// </summary>
    /// <remarks>
    /// Ryan Taylor 03/31/16
    /// Added Status Property
    /// </remarks>
    public class GroupMember
    {
        public User User { get; set; }
        public bool Leader { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
