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
    public interface IAdminGardenManager
    {
        bool RemoveGarden(Garden garden);
		bool RemoveGardenDesign(GardenPlan gardenPlan);
		bool ChangeGarden(Garden garden);
        bool ChangeGardenDesign(GardenPlan gardenPlan);
    }
}
