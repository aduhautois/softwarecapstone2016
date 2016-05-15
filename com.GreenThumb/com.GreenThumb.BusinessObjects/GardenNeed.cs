using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/14/16
    /// </summary>
    public class GardenNeed
    {
        public int GardenNeedId { get; set; }
        public Garden Garden { get; set; }          // Wrap the Garden unqiue identifier
        public string Title { get; set; }
        public string Description { get; set; }
        public string NeedType { get; set; }
        [Display(Name="Date Created")]
        public DateTime DateCreated { get; set; }
        public User CreatedBy { get; set; }         // Wrap the User unqiue identifier
        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
        public User ModifiedBy { get; set; }        // Wrap the user unique indenifier
        public bool Completed { get; set; }
    }
}
