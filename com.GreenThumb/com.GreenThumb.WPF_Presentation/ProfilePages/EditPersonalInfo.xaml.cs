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

namespace com.GreenThumb.WPF_Presentation.ProfilePages
{

    ///<summary>
    ///Author: Chris Schwebach
    ///Interaction logic for EditPersonalInfo.xaml
    ///Date: 3/3/16
    ///Updated Date: 3/19/16
    ///Changed from a window to a page
    ///</summary>
    public partial class EditPersonalInfo : Page
    {

        private UserManager myUserManager = new UserManager();

        private AccessToken _accessToken;

        private int? regionId;

        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for constructor
        /// Date: 3/19/16
        /// </summary>
        public EditPersonalInfo(AccessToken _accessToken)
        {
            this._accessToken = _accessToken;

            InitializeComponent();

            DisplayPersonalInfo();

            txtFirstName.Clear();
            txtLastName.Clear();
            txtZip.Clear();
            txtEmailAddress.Clear();

            txtFirstName.Text = _accessToken.FirstName;
            txtLastName.Text = _accessToken.LastName;
            txtZip.Text = _accessToken.Zip;
            txtEmailAddress.Text = _accessToken.EmailAddress;

        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void Na_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 10;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void None_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = null;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void one_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 1;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void two_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 2;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void three_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 3;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void four_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 4;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void five_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 5;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void six_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 6;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void seven_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 7;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void eight_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 8;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for item selected ddl
        /// Date: 3/19/16
        /// </summary>
        private void nine_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 9;
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for getting current user info
        /// Date: 3/19/16
        /// </summary>
        private void DisplayPersonalInfo()
        {
            try
            {
                var user = myUserManager.GetPersonalInfo(_accessToken.UserID);
                grdPersonalInfo.ItemsSource = new List<User>{user};
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for cancel button
        /// Date: 3/19/16
        /// </summary>
        /// <remarks>
        /// Modified By Chris Sheehan
        /// rerouted cancel button to main profile tab page
        /// Date: 4/14/16
        /// </remarks>
        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePages.ProfileMain(_accessToken));
        }
        /// <summary>
        /// Author: Chris Schwebach
        /// Logic for save button
        /// Date: 3/19/16
        /// </summary>
        private void btnEditPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string zip = txtZip.Text;
            string emailAddress = txtEmailAddress.Text;

            try
            {

                myUserManager.EditUserPersonalInfo(_accessToken.UserID, firstName, lastName, zip, emailAddress, regionId);
                DisplayPersonalInfo();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtZip.Clear();
                txtEmailAddress.Clear();
                MessageBox.Show("Profile changed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
