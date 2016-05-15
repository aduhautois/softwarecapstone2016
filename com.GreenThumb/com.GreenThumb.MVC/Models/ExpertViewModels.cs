using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.GreenThumb.MVC.Models
{
    /// <summary>
    /// 
    /// Created by: Trent Cullinan 05/05/2016
    /// </summary>
    public class ExpertGroupDetailViewModel
    {
        public GroupDetailViewModel GroupDetail { get; set; }
        public GardenTaskViewModel ExpertTaskDetail { get; set; }
    }
}