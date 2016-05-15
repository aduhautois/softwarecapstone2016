using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IGardenViewer
    {	
		void InitiateGarden(GardenPlan gardenPlan, User user);
		IEnumerable<GardenPlan> FetchGardenDesigns();
		bool FetchGardenDesign(Garden garden);
    }
}
