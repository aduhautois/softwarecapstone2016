using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.GreenThumb.MVC.Models
{
    public class GardenTaskViewModel
    {
        /// <summary>
        /// Author: Poonam
        /// Data Transfer Object to represent the grouplist and tasklist.
        /// Date: Apr.10th.2016
        /// </summary>
        public IEnumerable<Group> GroupsList { get; set; }

        public IEnumerable<Task> JobList { get; set; }

        public Job JobObj { get; set; }
    }
}