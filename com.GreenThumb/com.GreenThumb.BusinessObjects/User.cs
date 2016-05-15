// Updated by Poonam Dubey on 02/27/2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.GreenThumb.BusinessObjects
{
    public class User
    {
        /// <summary>
        /// Author: Poonam
        /// Data Transfer Object to represent a User from the
        /// Database
        /// UpdateBy: Chris Schwebach
        /// Date: 3/31/16
        /// Added data annotations 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int UserID { get; set; }

        [MinLength(1), MaxLength(50)]
        [Required(ErrorMessage = "Invalid First Name! First name must be between 1 and 50 characters in length")]
        public string FirstName { get; set; }

        [MinLength(1), MaxLength(100)]
        [Required(ErrorMessage = "Invalid Last Name! Last name must be between 1 and 100 characters in length")]
        public string LastName { get; set; }

        public string Zip { get; set; }

        [MaxLength(100)]
        public string EmailAddress { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int? RegionId { get; set; }
        public User() { }
        public User(int userID,
                     string firstName,
                     string lastName,
                     string zip,
                     string emailAddress,
                     string userName,
                     string password,
                     bool active,
                     int regionId)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Zip = zip;
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
            Active = active;
            RegionId = regionId;
        }


        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
             

    }
}
