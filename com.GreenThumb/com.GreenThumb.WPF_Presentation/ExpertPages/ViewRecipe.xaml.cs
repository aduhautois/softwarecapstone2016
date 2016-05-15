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
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;
using System.Reflection;
using System.ComponentModel;

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Rhett Allen
    /// Created Date: 3/31/16
    /// Page for viewing and searching for recipes
    /// </summary>
    public partial class ViewRecipe : Page
    {
        private AccessToken _accessToken = new AccessToken();
        private RecipeManager recipeManager = new RecipeManager();
        private int perPage = 10;
        private int currentPage = 1;
        private int maxPages = 1;
        private List<Recipe> recipeList = new List<Recipe>();
        private string keyword = "";
        private string category = "";

        public ViewRecipe(AccessToken accessToken)
        {
            InitializeComponent();

            _accessToken = accessToken;

            InitializeComboboxCategories();
        }

        private void InitializeComboboxCategories()
        {
            cmbCategories.Items.Add("All");
            foreach (Enum e in Enum.GetValues(typeof(RecipeCategories)).Cast<RecipeCategories>())
            {
                cmbCategories.Items.Add(ConvertEnumToDescription(e));
            }
            cmbCategories.SelectedIndex = 0;
            category = (string)cmbCategories.SelectedItem;
        }

        private string ConvertEnumToDescription(Enum e)
        {
            Type type = e.GetType();
            string name = Enum.GetName(type, e);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return e.ToString();
        }

        private void txtKeywords_KeyUp(object sender, KeyEventArgs e)
        {
            currentPage = 1;
            keyword = txtKeywords.Text;
            SearchRecipes();
        }

        private void SearchRecipes()
        {
            if (category == "All")
            {
                category = null;
            }

            int count = recipeManager.GetRecipesCount(keyword, category);

            CountPages(count);
        }

        private void CountPages(int count)
        {
            maxPages = count / perPage;

            if (count % perPage != 0)
            {
                maxPages++;
            }

            if (maxPages == 0)
            {
                maxPages = 1;
                currentPage = 1;
            }

            lblPage.Content = "Page " + currentPage;

            PopulateRecipes();
        }

        private void PopulateRecipes()
        {
            try
            {
                gridRecipes.ItemsSource = recipeManager.GetRecipesWithKeywordAndCategory(keyword, category, currentPage, perPage);
            }
            catch (Exception)
            {
                recipeList = new List<Recipe>();
                gridRecipes.ItemsSource = new List<Recipe>();
                currentPage = 1;
            }
        }

        private void gridRecipes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenRecipe();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage > 1)
            {
                currentPage--;
            }

            lblPage.Content = "Page " + currentPage;
            SearchRecipes();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage < maxPages)
            {
                currentPage++;
            }

            lblPage.Content = "Page " + currentPage;
            SearchRecipes();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenRecipe();
        }

        private void OpenRecipe()
        {
            try
            {
                Recipe recipe = (Recipe)gridRecipes.SelectedItem;
                if(recipe != null)
                {
                    this.NavigationService.Navigate(new ExpertPages.RecipeDetail(_accessToken, recipe));
                }
            }
            catch (Exception)
            {

            }
        }

        private void gridRecipes_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (gridRecipes.SelectedIndex != -1)
            {
                btnOpen.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                btnOpen.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            lblPage.Content = "Page " + currentPage;
            SearchRecipes();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            currentPage = maxPages;
            lblPage.Content = "Page " + currentPage;
            SearchRecipes();
        }

        private void cmbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            category = (string)cmbCategories.SelectedItem;
            SearchRecipes();
        }
    }
}
