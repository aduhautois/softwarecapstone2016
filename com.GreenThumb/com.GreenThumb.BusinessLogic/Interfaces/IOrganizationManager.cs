using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IOrganizationManager
    {
        IEnumerable<Group> Groups { get; }
        //IEnumerable<Post> Announcement { get; }

        //bool CreateAnnouncement(Post post);
        //bool RemoveAnnouncement(Post post);
        bool AddGroup(Group group);
        bool CreateGroup(Group group);
        bool RemoveGroup(Group group);
        bool CloseGroup(Group group);
        bool PromoteGroupLeader(User user);
        bool DemoteGroupLeader(User user);
        bool UpdateOrganizationInformation(Organization organization);
    }
}
