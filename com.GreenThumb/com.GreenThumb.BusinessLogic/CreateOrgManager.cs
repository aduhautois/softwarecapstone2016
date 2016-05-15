using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System.Data.SqlClient;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Created By: Kristine Johnson
    /// Uses an interface to implement business logic.  
    /// </summary>
    public class CreateOrgManager:ICreateOrgManager
    {
        /// <summary>
        /// Comments added by TRex 4/19/16
        /// This method adds a new organization.
        /// </summary>
        /// <param name="orgLeaderID"></param>
        /// <param name="organizationName"></param>
        /// <param name="localPhone"></param>
        /// <returns></returns>
        public bool AddNewOrganization(int orgLeaderID, string organizationName, string localPhone)
        {
           ///makes a new instance of an organization based on the input
            var newOrg = new Organization()
            {
                
                Name = organizationName,
                ContactPhone = localPhone,
            };

           
            ///parameters set in the database
            if (organizationName.Length > 100)
            {
                throw new ApplicationException("Organization name cannot be over 100 characters!");
            }

           
            ///parameters set in the database
            if (localPhone.Length != 10)
            {
                throw new ApplicationException("Local Phone must be 10 digits!");
            }

           

            try
            {
                return (CreateOrgAccessor.InsertOrganization(newOrg, orgLeaderID)==1);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }
    }
}
