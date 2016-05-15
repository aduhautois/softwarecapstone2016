using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.MVC.Models
{
    public class ArticlesViewModel
    {
        public Blog blog { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

    }
}