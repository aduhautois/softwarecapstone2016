using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Nutrient
    {
        /// <summary>
        /// Author: Luke Frahm
        /// Data Transfer Object to represent Nutrient information
        /// from the Database
        /// </summary>
        
        public int NutrientID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Nutrient() { }

        public Nutrient(int nutrientID, string name, string description, int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate)
        {
            NutrientID = nutrientID;
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
