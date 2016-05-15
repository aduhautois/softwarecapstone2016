using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObject
{
    /// <summary>
    /// This is the interface for creating an administrator account.
    /// </summary>
    public interface IAdminAccount
    {
        string Administrator(IEnumerable<AdministratorAccount> admin);
    }
}
