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
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Rhett Allen
    /// Created Date: 3/31/16
    /// Page for viewing the details of a single recipe
    /// </summary>
    public partial class RecipeDetail : Page
    {
        private AccessToken _accessToken;
        private Recipe _recipe;
        private UserManager userManager = new UserManager();
        public RecipeDetail(AccessToken accessToken, Recipe recipe)
        {
            InitializeComponent();

            _accessToken = accessToken;
            _recipe = recipe;

            InitializeControls();
        }

        private void InitializeControls()
        {
            lblTitle.Text =  _recipe.Title;
            lblDescription.Text = _recipe.Directions;
            lblCreatedBy.Text = "made by " + userManager.GetUser(_recipe.CreatedBy).UserName;
            lblDate.Text = _recipe.CreatedDate.ToString("MMMM dd, yyyy hh:mm tt");
            lblCategory.Text = "Category: " + _recipe.Category;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExpertPages.ViewRecipe(_accessToken));
        }
    }
}
