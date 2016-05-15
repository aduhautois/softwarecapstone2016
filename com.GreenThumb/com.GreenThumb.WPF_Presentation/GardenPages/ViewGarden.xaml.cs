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

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for ViewGarden.xaml
    /// </summary>
    public partial class ViewGarden : Page
    {

        /// <summary>
        /// 
        /// </summary>
        private AccessToken accessToken;
        private Organization organization;
        private GardenManager gardenManager = new GardenManager();

        public ViewGarden()
        {
            InitializeComponent();
        }




        private void grdGardenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DisplayCustomerData()
        {
            try
            {
                var gardens = gardenManager.GetGardens();
                grdGardenList.ItemsSource = gardens;
            }
            catch (Exception ex)
            {
                lblMessage.Content = ex.Message;
            }

        }


    }
}
