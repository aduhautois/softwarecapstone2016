using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    class AddRequest
    {
        public int RequestID { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
