using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class UserRoleManager
    {
        public UserRoleManager() { }
        public List<UserRole> GetUserRoleList()
        {
            /// <summary>
            /// Author: Ibrahim Abuzaid
            /// Data Transfer Object to represent a User from the
            /// Database
            /// 
            /// Added 3/25 By Ibarahim
            /// </summary>
            try
            {
                var userRoleList = UserRoleAccessor.RetrieveUserRoleList();

                if (userRoleList.Count > 0)
                {
                    return userRoleList;
                }
                else
                {
                    throw new ApplicationException("There were no records found.");
                }
            }

            catch (Exception)
            {
                // *** we should sort the possible exceptions and return friendly messages for each
                Console.Out.WriteLine("Exception Handler on Role manager Class...");
                throw;
            }
        }
        // <summary>
        /// 
        /// Getting a sspecific user data py passing his/her name
        /// as a parameter
        /// 
        /// Added 4/15/2016 By Ibarahim Abuzaid
        /// </summary>
        public List<UserRole> GetUserRoleListByUser(int userID)
        {
            /// <summary>
            /// Author: Ibrahim Abuzaid
            /// Data Transfer Object to represent a User from the
            /// Database
            /// 
            /// Added 3/25 By Ibarahim
            /// </summary>
            try
            {
                var userRoleList = UserRoleAccessor.RetrieveUserRoleListByUser(userID);

                if (userRoleList.Count > 0)
                {
                    return userRoleList;
                }
                else
                {
                    throw new ApplicationException("There were no records found.");
                }
            }

            catch (Exception)
            {
                // *** we should sort the possible exceptions and return friendly messages for each
                Console.Out.WriteLine("Exception Handler on Role manager Class...");
                throw;
            }
        }

        public int GetUserRoleCount()
        {
            try
            {
                return UserRoleAccessor.RetrieveUserRoleCount();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddNewUserRole(int userId,
                               string roleId)
        {
            try
            {
                var userRole = new UserRole()
                {
                    UserID = userId,
                    RoleID = roleId
                    
                };
                if (UserRoleAccessor.InsertUserRole(userRole) == 1)
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
        // should not be used after approval
        public bool EditUserRole(UserRole userRole)
        {
            if (userRole.UserID < 1000)
            {
                throw new ApplicationException("Invalid userID");
            }
            
            try
            {
                if (UserRoleAccessor.UpdateUserRole(userRole) == 1)
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
        // should be replaced by EditUserRoleStatus
        public bool RemoveUserRole(int usr, string role)
        {
            if (usr < 1000)
            {
                throw new ApplicationException("Invalid userID");
            }

            try
            {
                if (UserRoleAccessor.UpdateUserRoleRemove(usr, role) == 1)
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

        public bool EditUserRoleStatus(int usr, string role, bool active)
        {
            if (usr < 1000)
            {
                throw new ApplicationException("Invalid userID");
            }

            try
            {
                if (UserRoleAccessor.UpdateUserRoleStatus(usr, role, active) == 1)
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
    }
}
