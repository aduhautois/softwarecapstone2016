using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Organization
    {
        public int OrganizationID { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public bool Active { get; set; }
        public User OrganizationLeader { get; set; }
        public IEnumerable<Group> OrganizationGroups { get; set; }
    }
}
