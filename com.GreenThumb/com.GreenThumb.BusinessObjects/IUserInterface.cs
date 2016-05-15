using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public interface IUserInterface
    {
        string User(IEnumerable<User> users);
    }
}
