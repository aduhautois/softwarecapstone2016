using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Rhett Allen
    /// Created: 4/29/16
    /// Used to display the response with readable properties
    /// </summary>
    public class ResponseView
    {
        public string UserResponse { get; set; }
        public DateTime Date { get; set; }
        public string ArticleName { get; set; }
        public string Name { get; set; }
        public int? BlogID { get; set; }
    }
}
