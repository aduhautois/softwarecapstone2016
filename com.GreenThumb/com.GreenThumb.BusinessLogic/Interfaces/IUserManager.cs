using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IUserManager
    {
        User CurrentUser { get; }

        bool EditUserInformation(User user);
        bool LogOutUser();
        bool ChangePassword(string newPassword);
        IEnumerable<Group> RefreshGroupList();
        bool RemoveUser();
    }
}
