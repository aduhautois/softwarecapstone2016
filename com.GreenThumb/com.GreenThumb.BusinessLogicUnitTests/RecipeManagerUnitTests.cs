using System;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class RecipeManagerUnitTests
    {
        RecipeManager recipeManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            recipeManager = new RecipeManager();
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for RecipeManager AddNewRecipe class has valid input
        ///Date: 3/3/16
        ///</summary>
        [TestMethod]
        public void TestUserRecipeInformationInputReturnTrueValid()
        {
            //arrange
            int userId = 1000;
            string title = "Green bean soup";
            string category = "soup";
            string directions = "blah blah";

            //act
            bool result = recipeManager.AddNewRecipe(title, category, directions, userId);

            //assert
            Assert.AreEqual(true, result);
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for RecipeManager AddNewRecipe class has invalid title input
        /// throws exception
        ///Date: 3/3/16
        /// Updated by Steve Hoover 4/29/16
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserRecipeInformationTitleInputInvalid()
        {
            //arrange
            int userId = 1000;
            string title = "";
            string category = "soup";
            string directions = "blah blah";

            //act
            bool result = recipeManager.AddNewRecipe(title, category, directions, userId);

            // assert
            Assert.AreEqual(false, result);
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for RecipeManager AddNewRecipe class has invalid category input
        /// throws exception
        ///Date: 3/3/16
        ///
        /// Updated by Steve Hoover 4/29/16
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserRecipeInformationCategoryInputInvalid()
        {
            //arrange
            int userId = 1000;
            string title = "Tomatoe Soup";
            string category = "";
            string directions = "blah blah";

            //act
            bool result = recipeManager.AddNewRecipe(title, category, directions, userId);

            // assert
            Assert.AreEqual(false, result);
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for RecipeManager AddNewRecipe class has invalid directions/ingrediants input
        /// throws exception
        ///Date: 3/3/16
        ///
        /// Updated by Steve Hoover 4/29/16
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserRecipeInformationDirectionsInputInvalid()
        {
            //arrange
            int userId = 1000;
            string title = "Tomatoe Soup";
            string category = "soup";
            string directions = "";

            //act
            bool result = recipeManager.AddNewRecipe(title, category, directions, userId);

            //assert
            Assert.AreEqual(false, result);
        }
    }
}
