using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace com.GreenThumb.MVC.Models
{
    /// <summary>
    /// Ryan Taylor
    /// Created: 04/26/2016
    /// </summary>
    public class MessageViewModelOutbox
    {
        [HiddenInput(DisplayValue = false)]
        public int MessageID { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "To")]
        public string Receiver { get; set; }

        [Required]
        [Display(Name = "Date Sent")]
        public string Date { get; set; }

        public bool Delete { get; set; }
    }

    public class MessageViewModelInbox
    {
        [HiddenInput(DisplayValue = false)]
        public int MessageID { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "From")]
        public string Sender { get; set; }

        [Required]
        [Display(Name = "Date Received")]
        public string Date { get; set; }

        [Required]
        public bool Unread { get; set; }

        public bool Delete { get; set; }
    }

    public class MessageViewModelCompose
    {
        public List<MessageViewModelUser> UserList { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "To")]
        public string Receiver { get; set; }
    }

    public class MessageViewModelUser
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}