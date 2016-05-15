using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic
{
    public class NutrientManager
    {
        /// <summary>
        /// Rhett Allen
        /// Created Date: 4/7/16
        /// Adds a single nutrient to a plant
        /// </summary>
        /// <param name="nutrientID">Nutrient ID of nutrient to be added</param>
        /// <param name="plantID">Plant ID of plant the nutrient is added to</param>
        /// <returns>True if the nutrient was added successfully</returns>
        public bool AddNutrientToPlant(int nutrientID, int? plantID)
        {
            bool added = false;

            try
            {
                added = NutrientAccessor.InsertPlantNutrients(nutrientID, plantID);
            }
            catch (Exception)
            {
                throw new ApplicationException("Nutrient could not be added to plant");
            }

            return added;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 4/7/16
        /// Gets a list of nutrients
        /// </summary>
        /// <returns>List of nutrients</returns>
        public List<Nutrient> GetNutrients()
        {
            List<Nutrient> nutrients = new List<Nutrient>();

            try
            {
                nutrients = NutrientAccessor.RetrieveNutrient();

                if(nutrients.Count == 0)
                {
                    throw new ApplicationException("Nutrients could not be found");
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Nutrients could not be found");
            }

            return nutrients;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 4/7/16
        /// Gets a list of all the nutrients for a single plant
        /// </summary>
        /// <param name="plantID">The plant ID of the plant to get the nutrients from</param>
        /// <returns>List of all the nutrients for a single plant</returns>
        public List<Nutrient> GetPlantNutrients(int? plantID)
        {
            List<Nutrient> nutrients = new List<Nutrient>();

            try
            {
                nutrients = NutrientAccessor.RetrievePlantNutrients(plantID);

                if (nutrients.Count == 0)
                {
                    throw new ApplicationException("Nutrients have not yet been added to this plant");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Nutrients could not be found");
            }

            return nutrients;
        }
    }
}
