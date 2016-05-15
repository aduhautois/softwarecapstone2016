using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IGardenDesigner
    {
        bool SubmitGardenDesign(Garden garden, User user);
		bool ChangeGardenDesign(GardenPlan gardenPlan);
        bool RemoveGardenDesign(GardenPlan gardenPlan);
    }
}
