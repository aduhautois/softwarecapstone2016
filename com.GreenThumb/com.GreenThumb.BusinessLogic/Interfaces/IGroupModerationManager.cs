using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IGroupModerationManager
    {
        Group CurrentGroup { get; }

        bool RemoveMember(User user);
        //bool RemovePost(Post post);
        //bool HidePost(Post post);
        //bool ChangeGroupOptions(GroupOptions groupOptions);
        bool EditGroupInformation(Group group);
        bool CloseGroup();
    }
}
