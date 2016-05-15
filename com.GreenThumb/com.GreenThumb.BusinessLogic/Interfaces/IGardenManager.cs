using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface for Garden Manager by Poonam Dubey
    /// </summary>
    public interface IGardenManager
    {
        bool AddNewGarden(Garden garden);
    }
}
