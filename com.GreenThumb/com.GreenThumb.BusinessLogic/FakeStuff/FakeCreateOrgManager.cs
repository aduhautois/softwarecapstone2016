using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic.FakeStuff
{
    public class FakeCreateOrgManager : ICreateOrgManager
    {
        public bool AddNewOrganization(int orgLeaderID,
                                string organizationName,
                                    string localPhone)
        ///makes a new instance of an organization based on the input
        {
            try
            {
                if (organizationName == "NEWORG")
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}


