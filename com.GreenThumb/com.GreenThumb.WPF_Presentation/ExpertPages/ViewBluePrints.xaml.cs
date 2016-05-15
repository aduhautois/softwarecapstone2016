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
using System.IO;
using System.Diagnostics;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// stenner kvindlog
    /// Interaction logic for ViewBluePrints.xaml
    /// view blueprint file names and open in folder location 
    /// </summary>
    public partial class ViewBluePrints : Page
    {
          
        List<Blueprint> blueprintList;
        AccessToken _accessToken;
        BlueprintManager myBlueprintManager = new BlueprintManager();

        public ViewBluePrints(AccessToken _AccessToken)
        {
            InitializeComponent();
            _accessToken = _AccessToken;
            populateBlueprintList();
       }

        public void getBlueprints()
        {   // retrive all blueprints from database                                        
           blueprintList = myBlueprintManager.retriveAllBlueprints();
        }

        public void populateBlueprintList()
        {
            try
            {
                blueprintList = myBlueprintManager.retriveAllBlueprints();
                lvBlueprints.Items.Clear();
                this.lvBlueprints.ItemsSource = blueprintList; 
            }
            catch (Exception)
            {
                MessageBox.Show("No Files Have Been Uploaded");   
            }
                  
        }

        // open file with default program 
        private void bluePrints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Blueprint myBlueprint = new Blueprint();
            myBlueprint = (Blueprint)lvBlueprints.SelectedItem;  
          
            // open file with default program 
            System.Diagnostics.Process.Start(myBlueprint.FilePath);       
        }

    }

}
