using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Added by Poonam Dubey on 02/27/2016
    /// </summary>
    public class Region
    {
        public int RegionID { get; set; }
        //public string GardenID { get; set; }
        public string SoilType { get; set; }
        public decimal? AverageTemperatureSummer { get; set; }
        public decimal? AverageTemperatureFall { get; set; }
        public decimal? AverageTemperatureWinter { get; set; }
        public decimal? AverageTemperatureSpring { get; set; }
        public decimal? AverageRainfall { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Region() { }

        public Region(int regionID,
                       string soilType,
                       decimal averageTemperatureSummer,
                       decimal averageTemperatureFall,
                       decimal averageTemperatureWinter,
                       decimal averageTempertureSpring,
                       decimal averageRainfall,
                       int createdBy,
                       DateTime createdDate,
                       int modifiedBy,
                       DateTime modifiedDate)
        {
            RegionID = regionID;
            SoilType = soilType;
            AverageTemperatureSummer = averageTemperatureSummer;
            AverageTemperatureFall = averageTemperatureFall;
            AverageTemperatureWinter = averageTemperatureWinter;
            AverageTemperatureSpring = averageTempertureSpring;
            AverageRainfall = averageTemperatureFall;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;

        }

        // Rhett Allen 3/24/16 - added ToString() override
        public override string ToString()
        {
            return "Region " + RegionID;
        }

    }
}
