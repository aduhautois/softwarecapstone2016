using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Expertise
    {

        public string GardenTypeID { get; set; }
        public int RegionID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int ExpertID { get; set; }
        public bool Active { get; set; }





        public Expertise(string TypeID, int RegionID, string Content, DateTime CreatedDate, DateTime ModifiedDate,
                         int CreatedBy, int ModifiedBy, int ExpertID, bool Active )
        {
            this.GardenTypeID = TypeID;
            this.RegionID = RegionID;
            this.Content = Content;
            this.CreatedDate = CreatedDate;
            this.ModifiedDate = ModifiedDate;
            this.CreatedBy = CreatedBy;
            this.ModifiedBy = ModifiedBy;
            this.ExpertID = ExpertID;
            this.Active = Active;
        }
    }
}
