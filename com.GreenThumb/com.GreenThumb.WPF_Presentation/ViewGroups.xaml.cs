using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessLogic.Interfaces;
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
using System.Windows.Shapes;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Created by Kristine Johnson 2/28/16
    /// A view with button to display a group list
    /// </summary>
    public partial class ViewGroups : Window
    {
        private IGroupManager myGroupManager = new GroupManager();
        private AccessToken accessToken;
        private Organization organization;
        public ViewGroups()
        {
            organization = new Organization();///{ OrganizationID = 1000 };uncomment these to test code once log in is complete///
            accessToken = new AccessToken();/// {UserID=1000 }; uncomment these to test code without finished accesstoken///

        }
        public ViewGroups(AccessToken accessToken, Organization organization)
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisplayGroupData();
        }
        private void DisplayGroupData()
        {
            try
            {
                var groups = myGroupManager.GetGroupList(organization.OrganizationID);
                grdGroups.ItemsSource = groups;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("You selected" + ((Group)grdGroups.SelectedItem).Name);
        }
    }
}
