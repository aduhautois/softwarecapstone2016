
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    ///Author: Chris Schwebach
    /// Interaction logic for RecipeInput.xaml 
    ///Date: 3/19/16 
    /// </summary>
    public partial class RecipeInput : Page
    {
        private RecipeManager myRecipeManager = new RecipeManager();

        private AccessToken _accessToken;

        private string category = "";


        public RecipeInput(AccessToken _accessToken)
        {
            this._accessToken = _accessToken;
            InitializeComponent();
            txtTitle.Clear();
            txtDirections.Clear();
        }

        private void selectBaked(object sender, RoutedEventArgs e)
        {
            category = "baked";
        }

        private void selectBeverage(object sender, RoutedEventArgs e)
        {
            category = "beverage";
        }

        private void selectCanning(object sender, RoutedEventArgs e)
        {
            category = "canning";
        }

        private void selectDessert(object sender, RoutedEventArgs e)
        {
            category = "dessert";
        }

        private void selectGrilled(object sender, RoutedEventArgs e)
        {
            category = "grilled";
        }

        private void selectMainDish(object sender, RoutedEventArgs e)
        {
            category = "main dish";
        }

        private void selectSalad(object sender, RoutedEventArgs e)
        {
            category = "salad";
        }

        private void selectSideDish(object sender, RoutedEventArgs e)
        {
            category = "side dish";
        }
        private void selectSoup(object sender, RoutedEventArgs e)
        {
            category = "soup";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string directions = txtDirections.Text;
            try
            {
                myRecipeManager.AddNewRecipe(title, category, directions, _accessToken.UserID);
                this.NavigationService.Navigate(new ExpertPages.RecipeDetail(_accessToken, new Recipe(0, title, category, directions, _accessToken.UserID, new DateTime(), 0, DateTime.Now)));
                txtTitle.Clear();
                txtDirections.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Clear();
            txtDirections.Clear();
            NavigationService.Navigate(new ExpertPages.RecipeInput(_accessToken));
        }

    }
}
