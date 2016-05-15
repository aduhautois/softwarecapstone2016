using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Recipe
    {
        /// <summary>
        /// Author: Luke Frahm
        /// Data Transfer Object to represent a submitted Recipe
        /// from the Database
        /// </summary>

        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Directions { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Recipe() { }

        public Recipe(int recipeID, string title, string category, string directions, int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate)
        {
            RecipeID = recipeID;
            Title = title;
            Category = category;
            Directions = directions;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
        }
    }
}
