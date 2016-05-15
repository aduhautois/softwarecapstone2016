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

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for ViewTasks.xaml
    /// </summary>
    public partial class ViewTasks : Page
    {
        private JobManager jobManager = new JobManager();
        private AccessToken accessToken;

        public ViewTasks(AccessToken _accessToken)
        {
            accessToken = _accessToken;
            InitializeComponent();
        }

        private void ShowGroupTasks_Click(object sender, RoutedEventArgs e)
        {
            //
            int gardenId = (int)cbGardenId.SelectedValue;
            jobManager.RetrieveJobByGardenId(gardenId);
        }

        private void SelectGroupTask_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// When clicked, the combo box displays all of the GardenId's shared with the access token's UserId.
        /// If a user is added to another garden, this will automatically show the new group.
        /// Created by Steve Hoover 3/31/16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            cbGardenId.ItemsSource.Equals(jobManager.RetrieveGardenIdByUserId(accessToken.UserID));
        }
    }
}
