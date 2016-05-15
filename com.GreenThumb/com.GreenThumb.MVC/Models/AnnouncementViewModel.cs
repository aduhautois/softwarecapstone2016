using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Models
{
    public class AnnouncementViewModel : IModelBinder
    {
        [HiddenInput(DisplayValue = false)]
        public int GroupID{ get; set; }

        [Required]
        [Display(Name="Announcement")]
        public string Content{ get; set; }

        public AnnouncementViewModel()
        {

        }


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;

            string groupID = request.Form.Get("GroupID");
            int convertedGroup = 0;
            int.TryParse(groupID, out convertedGroup);
            
            string content = request.Form.Get("Content");

            return new AnnouncementViewModel { GroupID = convertedGroup, Content = content };

        }
    }
}