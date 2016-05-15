using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class RecipeManager
    {
        ///<summary>
        ///Author: Chris Schwebach
        ///Recipe logic for user parameter input to insert a recipe  
        ///Date: 3/19/16 
        ///</summary>
        public bool AddNewRecipe(string title, string category, string directions, int userId)
        {
            bool result = true;

            var newRecipe = new Recipe()
            {
                Title = title,
                Category = category,
                Directions = directions
            };

            if (title.Length < 1 || title.Length > 50)
            {
                throw new ApplicationException("Title for the recipe is required! Must Be less than 50 characters in length");
            }
            else if (category.Length < 1)
            {
                throw new ApplicationException("Must choose a category!");
            }
            else if (directions.Length < 1)
            {
                throw new ApplicationException("Ingredients and directions are required!");
            }

            try
            {
                if (RecipeAccessor.CreateRecipe(newRecipe, userId) == 1)
                {
                    result = true;
                }
                else {
                    result = false;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid Input!");
            }

            return result;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/31/16
        /// Gets a list of recipes based on the category and keyword search
        /// </summary>
        /// <param name="keyword">Word that is contained in the recipe fields</param>
        /// <param name="category">The recipe's category</param>
        /// <param name="pageNumber">The page number of the datagrid for recipes. Used to create the offset.</param>
        /// <param name="perPage">The number of recipes displayed per page.</param>
        /// <returns>List of recipes based on the category and keyword search</returns>
        public List<Recipe> GetRecipesWithKeywordAndCategory(string keyword, string category, int pageNumber, int perPage)
        {
            List<Recipe> recipes = new List<Recipe>();

            int offset = (pageNumber - 1) * perPage;

            try
            {
                recipes = RecipeAccessor.RetrieveRecipesWithKeywordAndCategory(keyword, category, offset, perPage);

                if (recipes.Count > 0)
                {
                    return recipes;
                }
                else
                {
                    throw new ApplicationException("No recipes found");
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Recipes could not be retrieved");
            }
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/31/16
        /// Counts how many recipes are in the database with a category and contain a similar keyword
        /// </summary>
        /// <param name="keyword">Word that is contained in the recipe fields</param>
        /// <param name="category">The recipe's category</param>
        /// <returns>Number of recipes in the database with specified category and contain a similar keyword</returns>
        public int GetRecipesCount(string keyword, string category)
        {
            int count = 0;

            try
            {
                count = RecipeAccessor.RetrieveRecipeCount(keyword, category);
                return count;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Recipes could not be retrieved: " + ex.Message);
            }
        }
    }
}
