using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic.Interfaces
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 01/29/2016
    /// </summary>
    public interface IAdminOrganizerManager
    {
        IEnumerable<Organization> Organizations { get; }

        bool CreateOrganization(Organization organization);
        bool RemoveOrganization(Organization organization);
        bool PromoteOrganizater(User user);
        bool DemoteOrganizer(User user);
    }
}
