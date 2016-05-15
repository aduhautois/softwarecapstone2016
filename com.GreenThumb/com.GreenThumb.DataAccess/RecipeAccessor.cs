using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace com.GreenThumb.DataAccess
{
   public class RecipeAccessor
    {
        ///<summary>
        ///Author: Chris Schwebach
        ///DB accessor to insert a recipe  
        ///Date: 3/19/16
        ///</summary>
        ///changed method name from InputRecipe 4/21/16 Steve Hoover
       public static int CreateRecipe(Recipe recipe, int UserId)
       {
           int rowCount = 0;

           DateTime dateSubmited = DateTime.Now;
           
           // get a connection
           var conn = DBConnection.GetDBConnection();

           // we need a command object (the command text is in the stored procedure)
           var cmd = new SqlCommand("Expert.spInsertRecipes", conn);

           // set the command type for stored procedure
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("@Title", recipe.Title);
           cmd.Parameters.AddWithValue("@Category", recipe.Category);
           cmd.Parameters.AddWithValue("@Directions", recipe.Directions);
           cmd.Parameters.AddWithValue("@CreatedBy", UserId);
           cmd.Parameters.AddWithValue("@CreatedDate", dateSubmited);
           cmd.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
           cmd.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);

           cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
           cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

           try
           {
               conn.Open();
               rowCount = (int)cmd.ExecuteNonQuery();
           }
           catch (Exception)
           {
               throw new ApplicationException("Invalid Selection!");
           }
           finally
           {
               conn.Close();
           }

           return rowCount;
       }

       /// <summary>
       /// Rhett Allen
       /// Created Date: 3/31/16
       /// Gets a list of recipes from a stored procedure with a similar keyword and specified category
       /// </summary>
       /// <param name="keyword">Word that is like recipe fields</param>
       /// <param name="category">The recipe's category. Null category acts like all categories.</param>
       /// <param name="offset">The index where the stored procedure begins searching for recipes</param>
       /// <param name="returnAmount">The amount of recipes that are returned</param>
       /// <returns>List of recipes from a stored procedure with a similar keyword and specified category</returns>
       /// changed method name from FetchRecipesWithKeywordAndCategory 4/21/16 Steve Hoover
       public static List<Recipe> RetrieveRecipesWithKeywordAndCategory(string keyword, string category, int offset, int returnAmount)
       {
           List<Recipe> recipes = new List<Recipe>();

           var conn = DBConnection.GetDBConnection();
           var query = @"Expert.spSelectRecipesWithKeywordAndCategory";
           var cmd = new SqlCommand(query, conn);

           cmd.CommandType = System.Data.CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Keyword", keyword);

           if (category == null)
           {
               cmd.Parameters.AddWithValue("@Category", DBNull.Value);
           }
           else
           {
               cmd.Parameters.AddWithValue("@Category", category);
           }

           cmd.Parameters.AddWithValue("@Offset", offset);
           cmd.Parameters.AddWithValue("@ReturnAmount", returnAmount);

           try
           {
               conn.Open();

               SqlDataReader reader = cmd.ExecuteReader();

               if (reader.HasRows)
               {
                   while (reader.Read())
                   {
                       Recipe recipe = new Recipe()
                       {
                           RecipeID = reader.GetInt32(0),
                           Title = reader.GetString(1),
                           Category = reader.GetString(2),
                           Directions = reader.GetString(3),
                           CreatedBy = reader.GetInt32(4),
                           CreatedDate = reader.GetDateTime(5)
                       };

                       if (reader.IsDBNull(6))
                       {
                           recipe.ModifiedBy = null;
                       }
                       else
                       {
                           recipe.ModifiedBy = reader.GetInt32(6);
                       }

                       if (reader.IsDBNull(7))
                       {
                           recipe.ModifiedDate = null;
                       }
                       else
                       {
                           recipe.ModifiedDate = reader.GetDateTime(7);
                       }

                       recipes.Add(recipe);
                   }
               }
           }
           catch (Exception)
           {
               throw;
           }
           finally
           {
               conn.Close();
           }

           return recipes;
       }

       /// <summary>
       /// Rhett Allen
       /// Created Date: 3/31/16
       /// Counts how many recipes with a similar keyword and specified category are in the database
       /// </summary>
       /// <param name="keyword">Word that is like recipe fields</param>
       /// <param name="category">The recipe's category. Null category acts like all categories.</param>
       /// <returns>The number of recipes with a similar keyword and specified category are in the database</returns>
       /// changed method name from CountRecipes 4/21/16 Steve Hoover
       public static int RetrieveRecipeCount(string keyword = "", string category = null)
       {
           int count = 0;

           var conn = DBConnection.GetDBConnection();
           var query = @"Expert.spCountRecipes";
           var cmd = new SqlCommand(query, conn);

           cmd.CommandType = System.Data.CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Keyword", keyword);
           if (category == null)
           {
               cmd.Parameters.AddWithValue("@Category", DBNull.Value);
           }
           else
           {
               cmd.Parameters.AddWithValue("@Category", category);
           }

           try
           {
               conn.Open();
               count = (int)cmd.ExecuteScalar();
           }
           catch (Exception)
           {
               throw;
           }
           finally
           {
               conn.Close();
           }

           return count;
       }
    }
}
