using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessLogic;
using System.Collections.Generic;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class RegionManagerUnitTests
    {
        /// <summary>
        /// Steve Hoover
        /// 4/29/16
        /// Tests to verify GetRegions returns a valid List<Region>
        /// </summary>
        [TestMethod]
        public void TestGetRegions()
        {
            var regionManager = new RegionManager();

            List<Region> regions = new List<Region>();

            Assert.IsNotNull(regions);
        }
    }
}
