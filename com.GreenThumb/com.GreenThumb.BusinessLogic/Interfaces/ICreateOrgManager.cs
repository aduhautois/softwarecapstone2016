using System;
namespace com.GreenThumb.BusinessLogic
{
   public interface ICreateOrgManager
    {
        bool AddNewOrganization(int orgLeaderID, string organizationName, string localPhone);
    }
}
