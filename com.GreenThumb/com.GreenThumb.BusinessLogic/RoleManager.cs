using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class RoleManager
    {
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
        public List<Role> GetRoleList()
        {
            try
            {
                var roleList = RoleAccessor.RetrieveRoleList();

                if (roleList.Count > 0)
                {
                    return roleList;
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

        public int GetRoleCount()
        {
            try
            {
                return RoleAccessor.RetrieveRoleCount();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/25/16
        /// Checks to see if the user has a certain role
        /// </summary>
        /// <param name="roleName">RoleID to check</param>
        /// <param name="accessToken">The user's access token</param>
        /// <returns>True if the user is the role name</returns>
        /// Changed Method Name - Emily
        public bool ConfirmUserIsAssignedRole(AccessToken accessToken, string roleName)
        {
            bool isRole = false;

            List<Role> roles = new RoleManager().GetRoleList();
            foreach (Role role in accessToken.Roles)
            {
                foreach (Role r in roles)
                {
                    if (role.RoleID == r.RoleID)
                    {
                        if (role.RoleID == roleName)
                        {
                            return true;
                        }
                    }
                }
            }

            return isRole;
        }

        public bool AddNewRole(string roleId,
                               string description,
                               int createdBy,
                               DateTime createdDate)
        {
            try
            {
                var role = new Role()
                {
                    RoleID  = roleId,
                    Description = description,
                    CreatedBy = createdBy,
                    CreatedDate = createdDate
                };
                if (RoleAccessor.CreateRole(role) == 1)
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
