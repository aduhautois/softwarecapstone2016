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
    /// Created By: Trent Cullinan 04/28/16
    /// </summary>
    public class GroupNeedSummary
    {
        public int GroupID { get; set; }
        [Display(Name = "Name")]
        public string GroupName { get; set; }
        [Display(Name = "Current Needs")]
        public int ActiveNeeds { get; set; }
        [Display(Name = "Completed Needs")]
        public int CompletedNeeds { get; set; }
        [Display(Name = "Group Leader")]
        public User GroupLeader { get; set; }
    }

    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/28/16
    /// </summary>
    public class GardenNeedSummary
    {
        public int GardenID { get; set; }
        [Display(Name = "Name")]
        public string GardenName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Region ID")]
        public int RegionID { get; set; }
        [Display(Name = "Current Needs")]
        public int ActiveNeeds { get; set; }
        [Display(Name = "Completed Needs")]
        public int CompletedNeeds { get; set; }
    }
}
