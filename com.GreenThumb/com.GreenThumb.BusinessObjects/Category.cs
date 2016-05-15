using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Category
    {

        public string CategoryName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }

        public Category(string CategoryName, int CreatedBy, DateTime Date, bool Active)
        {
            this.CategoryName = CategoryName;
            this.CreatedBy = CreatedBy;
            this.Date = Date;
            this.Active = Active;
        }

    }
}
