using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using BusinessLogic.Interfaces;

namespace com.GreenThumb.BusinessLogic
{
    public class UserManager
    {
        private int userID;
        ///<summary>
        ///Author: Chris Schwebach
        ///EditUserPersonalInfo validates input from user calling to the UserAccessor
        ///Date: 3/3/16
        ///Updated Date: 3/19/16
        ///Updated regionID user input parameters
        ///</summary>
        public bool EditUserPersonalInfo(int userID, string firstName, string lastName, string zip, string emailAddress, int? regionId)
        {
           bool result = false;

            if (firstName.Length < 1 || firstName.Length > 50 || firstName.Equals(""))
            {
                throw new ApplicationException("Invalid First Name! First name must be between 1 and 50 characters in length");
            }
            else if (lastName.Length < 1 || lastName.Length > 100)
            {
                throw new ApplicationException("Invalid Last Name! Last name must be between 1 and 100 characters in length");
            }
            else if (zip.Length != 0 && (zip.Length != 5 && zip.Length != 9))
            {
                throw new ApplicationException("Invalid zip! Zip must be 5 or 9 characters in length.");
            }
            else if (emailAddress.Length > 100)
            {
                throw new ApplicationException("Invalid Email Address! Email must be less than 100 characters in length.");
            }
            else if (regionId == 10)
            {
                throw new ApplicationException("Please choose a regionID or choose none to opt out!");
            }

            try
            {

                var count = UserAccessor.UpdateUserPersonalInfo(userID, firstName, lastName, zip, emailAddress, regionId);
                result = 0 != count ? true : false;
                if (count != 0)
                {
                    result = true;
                }
                else if (count == 0)
                {
                    result = false;
                }
                else
                {
                    throw new ApplicationException("Multiple Record Found!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///GetUserPersonalInfo get the Personal information from user based on accessToken.UserID
        ///calling to the user accessor
        ///Date: 3/3/16
        ///
        /// Updated to return a single user, call RetrieveUserByUserID 4/21/16 Steve Hoover 
        ///</summary>
        public User GetPersonalInfo(int userID)
        {
            try
            {
                return UserAccessor.RetrieveUserByID(userID);
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("No records found");
            }
        }

        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
       public List<User> GetUserList(Active group = Active.active)
        {
            try
            {
                var userList = UserAccessor.RetrieveUserList(group);

                if (userList.Count > 0)
                {
                    return userList;
                }
                else
                {
                    throw new ApplicationException("There were no records found.");
                }
            }
            
            catch (Exception)
            {
                // *** we should sort the possible exceptions and return friendly messages for each
                throw;
            }
        }
        


        public int GetUserCount(Active group = Active.active)
        {
            try
            {
                return UserAccessor.RetrieveUserCount();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddNewUser(string firstName,
                                   string lastName,
                                   string zip,
                                   string emailAddress,
                                   string userName,
                                   string passWord,
                                   bool   active,
                                   int?   regionID)
        {
            try
            {
                var usr = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Zip = zip,
                    EmailAddress = emailAddress,
                    UserName = userName,
                    Password = passWord,
                    Active = active,
                    RegionId= regionID
                };
                return UserAccessor.CreateUser(usr);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool GetUserData(User usr, User newUsr)
        {
           //                 var usr = new User()

            if (usr.UserID < 1000)
            {
                throw new ApplicationException("Invalid UserID");
            }
            
            try
            {
                if(UserAccessor.UpdateUserInformation(usr, newUsr)==true)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        ///<summary>
        ///Author: Stenner Kvindlog         
        ///GetUserByUserName gets a user by username 
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>

        public User GetUserByUserName(string username)
        {
            try
            {
                return UserAccessor.RetrieveUserByUsername(username);
            }
            catch (Exception)
            {
                throw;
            }

        }

		///<summary>
        ///Author: Stenner Kvindlog         
        ///fetchUser gets a user by userID 
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>
        public User GetUser(int userId)
        {
            return UserAccessor.RetrieveUserByID(userId);
        }

        public User GetUser()
        {
            try
            {
                return UserAccessor.RetrieveUser();
            }

            catch (Exception)
            {
                throw;
            }
        }
		///<summary>
        ///Author: Stenner Kvindlog         
        ///AddUser sends user to database to be created  
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>
        public int AddUser(User newUser)
        {
            try
            {
                int num = UserAccessor.CreateUser(newUser);
                return num;
            }
            catch (Exception)
            {

                throw;
            }
        }

		///<summary>
        ///Author: Stenner Kvindlog         
        ///editUser sends old and new user to database to edit user 
		//calling to the user accessor
        ///Date: 3/4/16
		///</summary>
        public bool EditUser(User newUser, User oldUser)
        {
            try
            {
                bool flag = UserAccessor.UpdateUserInformation(newUser, oldUser);
                return flag;
            }
            catch (Exception)
            {

                throw;
            }

        }

        
        /// <summary>
        /// Checks whether the user exists with given information. If a password
        /// is not provided and results false: the username exists in database.
        /// If a password is provided and results false: there was an information
        /// mismatch.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Value to check database.</param>
        /// <param name="password">Value to check database.</param>
        /// <returns>Whether user has given correct information to be available.</returns>
        public bool ConfirmUserExists(string userName, string password = null)
        {
            bool flag = true;
            bool passwordFlag = String.IsNullOrEmpty(password);

            try
            {
                flag = passwordFlag ? 
                    UserAccessor.CheckUserName(userName) :
                    UserAccessor.CheckUserNameWithPassword(userName, password.HashSha256());
            }
            catch (Exception) 
            {
                // If no password is sent in and an exception is caught, set to true
                // to signal that it is unavailable.

                // If a passsword is sent in and an exception is caught, set to false
                // to signal that nothing matched.
                flag = passwordFlag;
            }

            return flag;
        }

        /// <summary>
        /// Confirms information entered by user is correct for re-registering web.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Username that relates.</param>
        /// <param name="email">Email to verified against database.</param>
        /// <param name="password">Password to be verified against database.</param>
        /// <returns>Whether the user exists with confirming information.</returns>
        public bool ConfirmUserInfo(string userName, string email, string password)
        {
            bool flag = false;

            try
            {
                flag = UserAccessor.ConfirmUserInfo(userName, email, password.HashSha256());
            }
            catch (Exception) { } // flag set to false to prevent user from proceeding.

            return flag;
        }

        /// <summary>
        /// Change password of a user.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Username that relates.</param>
        /// <param name="oldPassword">What the password was previously.</param>
        /// <param name="newPassword">What to change password to.</param>
        /// <returns>Whether the password change was successful.</returns>
        public bool EditPasssword(string userName, string oldPassword, string newPassword)
        {
            bool flag = false;

            try
            {
                flag = UserAccessor.UpdateUserPassword(userName, oldPassword.HashSha256(), newPassword.HashSha256());
            }
            catch (Exception) { } // flag set to false

            return flag;
        }

        /// <summary>
        /// Refactored create new user action to separate password from the model.
        /// Required properties: UserName, FirstName, LastName, EmailAddress, Zip
        /// TODO: Get rid of the password in the model. 
        ///
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="user">User with required base information.</param>
        /// <param name="password">Value to be set as password.</param>
        /// <returns>Whether the password change was successful.</returns>
        public bool AddNewUserPasswordChange(User user, string password)
        {
            bool flag = false;

            user.Password = password.HashSha256();

            try
            {
                flag = 1 == UserAccessor.CreateUser(user);
            }
            catch (Exception) { } // flag set to false

            return flag;
        }

        /// <summary>
        /// Refactored so I get the data I want
        /// and exceptions are handled properly.
        /// 
        /// Created by: Trent Cullinan 03/31/16
        /// </summary>
        /// <param name="userName">The username to get id for.</param>
        /// <returns>User Id</returns>
        public int GetUserId(string userName)
        {
            int userId = 0;

            try
            {
                // I have reached a level of laziness 
                // and greed in order to get what I want.
                userId = UserAccessor.RetrieveUserByUsername(userName).UserID;
            }
            catch (Exception) { } // userId will be set to zero.

            return userId;
        }

        /// <summary>
        /// Poonam Dubey
        /// 18th April 2016
        /// Function to check if user is garden leader
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        //public bool CheckIsGardenLeader(int userID)
        //{
        //    try
        //    {
        //        return UserAccessor.CheckIsGardenLeader(userID);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
