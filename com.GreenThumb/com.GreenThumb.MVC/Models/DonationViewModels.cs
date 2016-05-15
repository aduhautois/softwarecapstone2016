using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Models
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/21/16
    /// </summary>
    public class CreateNeedViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int GardenId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/21/16
    /// </summary>
    public class SendContributionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int NeedID { get; set; }
        [Display(Name = "Need")]
        public string NeedTitle { get; set; }
        [Display(Name = "Optional Message")]
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/21/16
    /// </summary>
    public class GardenDetailViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int GardenID { get; set; }   
        [Display(Name = "Current Needs")]
        public IEnumerable<GardenNeed> ActiveNeeds { get; set; }
        [Display(Name = "Pending Contributions")]
        public IEnumerable<NeedContribution> PendingContributions { get; set; }
        [Display(Name = "Completed Needs")]
        public IEnumerable<GardenNeed> CompletedNeeds { get; set; }
    }

    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/28/16
    /// </summary>
    public class GroupNeedsDetailViewModel
    {
        public IEnumerable<GardenNeedSummary> GardenSummaries { get; set; }
        public IEnumerable<GardenNeed> ActiveNeeds { get; set; }
    }

    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/28/16
    /// </summary>
    public class GardenNeedsDetailViewModel
    {
        public IEnumerable<GardenNeed> ActiveNeeds { get; set; }
    }

    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/28/16
    /// </summary>
    public class ContributionsDetailViewModel
    {
        public IEnumerable<NeedContribution> PendingContributions { get; set; }
        public IEnumerable<NeedContribution> ApprovedContributions { get; set; }
        public IEnumerable<NeedContribution> DeclinedContributions { get; set; }
    }
}