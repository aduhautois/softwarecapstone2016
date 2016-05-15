using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace com.GreenThumb.MVC.Models
{
    public class GroupCreationViewModels
    {

        [Display(Name = "UserID")]
        int UserID { get; set; }

        [Display(Name = "UserName")]
        string UserName { get; set; }

        [Required]
        [Display(Name = "GroupName")]
        string GroupName { get; set; }

        [Display(Name = "GroupLeaderID")]
        int GroupLeaderID { get; set; }
    }

    /// <summary>
    /// Created by: Trent Cullinan 04/05/2016
    /// Modified by Nicholas King
    /// </summary>
    public class GroupDetailViewModel
    {
        public int GroupID { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Display(Name = "Group Leader")]
        public GroupMemberViewModel GroupLeader { get; set; }

        [Display(Name = "Group Members")]
        public IEnumerable<GroupMemberViewModel> GroupMembers { get; set; }

        public IEnumerable<GroupGardenViewModel> Gardens { get; set; }

        //Added by Nicholas King
        public List<GroupMemberRequestModel> Requests { get; set; }
    }

    /// <summary>
    /// Created by: Trent Cullinan 04/05/2016
    /// </summary>
    public class GroupMemberViewModel
    {
        public int UserID { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
    }

    /// <summary>
    /// Created by: Trent Cullinan 04/05/2016
    /// </summary>
    public class GroupGardenViewModel
    {
        public int GardenID { get; set; }

        [Display(Name = "Garden Name")]
        public string GardenName { get; set; }

        public string Description { get; set; }

        // [Display(Name = "Date Created")]
        // public DateTime DateCreated { get; set; }  // Is it too much to ask for some consistency in the database? Can we have some useful data?
    }
}