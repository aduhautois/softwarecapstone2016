using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Notification
    {

        public int NotificationID { get; set; }
        public string Type { get; set; }
        public string Description{ get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }


        public Notification(int NotificationID, string Type, string Description, int CreatedBy, DateTime CreatedDate,
                             int ModifiedBy, DateTime ModifiedDate, bool Active)
        {

            this.NotificationID = NotificationID;
            this.Type = Type;
            this.Description = Description;
            this.CreatedBy = CreatedBy;
            this.CreatedDate = CreatedDate;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedDate = ModifiedDate;
            this.Active = Active;

        }
    }
}
