using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Models
{
    public class GardenCreationViewModel
    {
        [Required]
        [AllowHtml]
        [Display(Name = "Garden Description")]
        public string GardenDescription { get; set; }

        [Required]
        [Display(Name = "Garden Name")]
        public string GardenName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int GroupID { get; set; }

        public int RegionID { get; set; }
        
    }
}