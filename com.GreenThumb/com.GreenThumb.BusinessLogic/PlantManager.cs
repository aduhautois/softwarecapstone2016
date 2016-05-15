using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    //Created by Stenner Kvindlog 
    public class PlantManager
    {


        ///<summary>
        ///Author: Stenner Kvindlog         
        ///fetchPlantList gets a list of all the plants 
        //calling to the plant accessor
        ///Date: 3/4/16
        ///</summary>
        public List<Plant> GetPlantList(Active active)
        {
            try
            {
                List<Plant> plants = PlantAccessor.RetrievePlantList(active);
                foreach (Plant plant in plants)
                {
                    plant.RegionIDs = PlantAccessor.RetrievePlantRegions(plant);
                }
                return plants;
                //return AddTestPlants(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        ///<summary>
        ///Author: Stenner Kvindlog         
        ///fetchPlant gets a plant by plantId
        //calling to the plant accessor
        ///Date: 3/4/16
        ///</summary>
        public Plant GetPlant(int plantId)
        {
            return PlantAccessor.RetrievePlant(plantId);
        }


        ///<summary>
        ///Author: Stenner Kvindlog         
        ///AddPlant creates a plant 
        //calling to the plant accessor
        ///Date: 3/4/16
        ///</summary>
        public int AddPlant(Plant newPlant)
        {
            try
            {
                //bool myBool = PlantAccessor.AddPlant(newPlant);
                return PlantAccessor.CreatePlant(newPlant);
            }
            catch (Exception)
            {

                throw;
            }
        }

        ///<summary>
        ///Author: Stenner Kvindlog         
        ///EditPLant sends new and old plant to database to be edited  
        //calling to the plant accessor
        ///Date: 3/4/16
        ///</summary>
        public bool EditPlant(Plant newPlant, Plant oldPlant)
        {
            try
            {
                bool myBool = PlantAccessor.EditPlant(newPlant, oldPlant);
                return myBool;
            }
            catch (Exception)
            {

                throw;
            }

        }

        ///<summary>
        ///Author: Sara Nanke         
        ///Gets plant regions
        ///Date: 4/7/16
        ///</summary>
        public bool?[] GetPlantRegions(Plant plant)
        {
            try
            {
                return PlantAccessor.RetrievePlantRegions(plant);
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///<summary>
        ///Author: Sara Nanke         
        ///Sets plant regions 
        ///Date: 4/7/16
        ///</summary>
        public bool AddPlantRegions(Plant plant, bool?[] regions)
        {
            List<int> regionIds = new List<int>();
            for (int i = 0; i < regions.Length; i++)
            {
                if (regions[i] == true)
                {
                    regionIds.Add(i + 1);
                }
            }
            try
            {
                return PlantAccessor.CreatePlantRegions(plant, regionIds);
            }
            catch (Exception)
            {
                throw;
            }
        }


        ///<summary>
        ///Author: Sara Nanke         
        ///Creates some test data 
        ///Date: 3/31/16
        ///delete before the end of projects Dat Tran 4/22/2016
        ///</summary>
        public List<Plant> AddTestPlants(bool IsDB = true)
        {
            List<Plant> plants = new List<Plant>();

            //creating dummy plant list
            var date = new DateTime(1992, 1, 1);
            plants.Add(new
                Plant(null, "Blood Carrot", "Carrot", "Vegetable", "orange for bunnies", "Summer", 1000, date, 1001, date, true));
            plants.Add(new
                Plant(null, "Braburn", "Apple", "Fruit", "sweet crisp and ready to eat", "Summer", 1000, date, 1001, date, true));
            plants.Add(new
                Plant(null, "Michigan Apple", "Apple", "Fruit", "tastes like Michigan", "Summer", 1000, date, 1001, date, true));
            plants.Add(new
                Plant(null, "Pink Lady", "Apple", "Fruit", "best apple ever", "Summer", 1000, date, 1001, date, true));
            plants.Add(new
                Plant(null, "Parsley", "Herb", "Herb", "good on pizza", "Summer", 1000, date, 1001, date, true));
            plants.Add(new
                Plant(null, "Basil", "Herb", "Herb", "good in tomato soup", "Summer", 1000, date, 1001, date, true));

            if (IsDB)
            {
                //creating test plants
                foreach (Plant plant in plants)
                {
                    AddPlant(plant);
                }
            }
            return plants;
        }

        //(int PlantID, string Name, string Type, string Category, string Description, string Season,
        //              int CreatedBy, DateTime CreatedDate, int ModifiedBy, DateTime ModifiedDate, bool Active)
    }
}
