using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IGardenProgressManager
    {
        bool UpdateProgress(Garden garden, User user);
    }
}
