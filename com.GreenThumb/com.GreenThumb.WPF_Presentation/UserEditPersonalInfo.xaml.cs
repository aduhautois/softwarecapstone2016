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
using System.Windows.Shapes;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Author: Chris Schwebach
    /// Interaction logic for UserEditPersonalInfo.xaml
    /// Date: 3/3/16
    /// ///Updated Date: 3/8/16
    /// Added accessToken info retrieval
    /// Window retrieval/closing features updated
    /// </summary>
    public partial class UserEditPersonalInfo : Window
    {
        private UserManager myUserManager = new UserManager();

        private AccessToken _accessToken;

        /// <summary>
        /// Author: Chris Schwebach	
        /// Updated by Chris Sheehan
        /// Update date: 3/18/2016
        /// Update reason: update access token logic for when not logged in, disable submit button if not logged in
        /// </summary>
        public UserEditPersonalInfo(AccessToken _accessToken)
        {
            this._accessToken = _accessToken;

            InitializeComponent();

            DisplayPersonalInfo();

            txtFirstName.Clear();
            txtLastName.Clear();
            txtZip.Clear();
            txtEmailAddress.Clear();
            txtRegionID.Clear();


            
            if (_accessToken != null)
            { 
                txtFirstName.Text = _accessToken.FirstName;
                txtLastName.Text = _accessToken.LastName;
                txtZip.Text = _accessToken.Zip;
                txtEmailAddress.Text = _accessToken.EmailAddress;
                if (_accessToken.RegionId == 0 || _accessToken.RegionId == null)
                {
                    txtRegionID.Text = "";
                }
                else
                {
                    txtRegionID.Text = _accessToken.RegionId.ToString();
                }
              
            }
            else { btnEditPersonalInfo.IsEnabled = false; }
            
        }

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

        private void btnReturnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtZip.Clear();
            txtEmailAddress.Clear();
            txtRegionID.Clear();
           
            this.Close();
        }

        private void btnEditPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string zip = txtZip.Text;
            string emailAddress = txtEmailAddress.Text;
            string regionIDText = txtRegionID.Text;
            int? regionID;
            int numRegionID;

            if (regionIDText == "")
            {
                try
                {
                    myUserManager.EditUserPersonalInfo(_accessToken.UserID, firstName, lastName, zip, emailAddress, null);
                    DisplayPersonalInfo();
                    txtFirstName.Clear();
                    txtLastName.Clear();
                    txtZip.Clear();
                    txtEmailAddress.Clear();
                    txtRegionID.Clear();
                    lblRegionIDError.Content = "";
                    MessageBox.Show("Profile changed!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            if (int.TryParse(regionIDText, out numRegionID))
            {
                try
                {
                    regionID = numRegionID;
                    myUserManager.EditUserPersonalInfo(_accessToken.UserID, firstName, lastName, zip, emailAddress, regionID);
                    DisplayPersonalInfo();
                    txtFirstName.Clear();
                    txtLastName.Clear();
                    txtZip.Clear();
                    txtEmailAddress.Clear();
                    txtRegionID.Clear();
                    lblRegionIDError.Content = "";
                    MessageBox.Show("Profile changed!");
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (regionIDText != "")
            {
                lblRegionIDError.Content = "RegionID must be a numeric value!";
            }

        }


    }
}
