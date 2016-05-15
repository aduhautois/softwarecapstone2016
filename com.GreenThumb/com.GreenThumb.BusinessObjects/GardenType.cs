using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class GardenType
    {

        public int GardenTypeID { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public GardenType(int GardenTypeID, string Description, int CreatedBy, DateTime CreatedDate, int ModifiedBy, DateTime ModifiedDate)
        {
            this.GardenTypeID = GardenTypeID;
            this.Description = Description;
            this.CreatedBy = CreatedBy;
            this.CreatedDate = CreatedDate;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedDate = ModifiedDate;
        }
    }
}
