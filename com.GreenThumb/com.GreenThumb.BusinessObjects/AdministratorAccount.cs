using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObject
{
    /// <summary>
    /// This is the class that adds an administrator to the database.
    /// </summary>
    public class AdministratorAccount
    {
        
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zip { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public AdministratorAccount() { }

        /// <summary>
        /// This method contains the specifications for adding an administrator.
        /// </summary>
        public AdministratorAccount(
                        int userID,
                        string firstName,
                        string lastName,
                        string zip,
                        string emailAddress,
                        string userName,
                        string password,
                        bool active) 
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Zip = zip;
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
            Active = active;
        }
    }
 }
