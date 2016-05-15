using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    public class RegionManager
    {
        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all regions
        /// </summary>
        /// <returns>All known regions</returns>
        public List<Region> GetRegions()
        {
            List<Region> regions = new List<Region>();

            try
            {
                regions = RegionAccessor.RetrieveRegions();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Regions could not be retrieved: " + ex.Message);
            }

            return regions;
        }
    }
}
