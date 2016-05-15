using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.GreenThumb.MVC.Models
{
    /// <summary>
    /// ViewModel for a user viewing basic information about a group.
    /// 
    /// Created By: Trent Cullinan 02/31/2016
    /// </summary>
    public class UserGroupViewModel
    {
        public int GroupId { get; set; }
        [Display(Name = "Group Name")]
        public string Name { get; set; }
        [Display(Name = "Group Leader")]
        public string LeaderUserName { get; set; }
        [Display(Name = "Leader Email")]
        public string LeaderEmail { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }
    }
}