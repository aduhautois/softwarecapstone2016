using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Advice
    {
		public int AdviceID { get; set; }
        public string Advices { get; set; }
		public int CreatedBy { get; set; }
		public int ModifiedBy { get; set; }
		public string Content { get; set; }	
		public string Category { get; set; }
		public DateTime ModifiedDate { get; set; }
		public DateTime CreatedDate { get; set; }

        public Advice(int adviceID, string advice, int createdBy,
			int modifiedBy, string content, string category, DateTime modifiedDate, DateTime createdDate)
        {
            this.AdviceID = adviceID;
            this.Advices = advice;	
			this.CreatedBy = createdBy;
			this.ModifiedBy = modifiedBy;
			this.Content = content;
			this.ModifiedDate = modifiedDate;
			this.CreatedDate = createdDate;
        }
    }
}
