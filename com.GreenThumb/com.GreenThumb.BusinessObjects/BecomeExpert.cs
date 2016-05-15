using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class BecomeExpert
    {
        public int RequestNo { get; set; }
        public int Username { get; set; }
        public string WhyShouldIBeAnExpert { get; set; }
        public int? Approved { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }



        public BecomeExpert(int RequestNo, int UserName, string WhyShouldIBeAnExpert, int Approved, int CreatedBy,
                             DateTime CreatedDate, int ModifiedBy, DateTime ModifiedDate, bool Active)
        {
            this.RequestNo = RequestNo;
            this.Username = Username;
            this.WhyShouldIBeAnExpert = WhyShouldIBeAnExpert;
            this.Approved = Approved;
            this.CreatedBy = CreatedBy;
            this.CreatedDate = CreatedDate;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedDate = ModifiedDate;
            this.Active = Active;

        }
    }
}
