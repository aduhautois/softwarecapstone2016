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

namespace com.GreenThumb.WPF_Presentation.pages
{
    /// <summary>
    /// Interaction logic for CreateGarden.xaml
    /// Created By Poonam Dubey on Mar/5th/2016
    /// </summary>
    public partial class CreateGarden : Page
    {
        /// <summary>
        /// Class Object defined.
        /// </summary>

        private GroupManager myGroupManager = new GroupManager();
        private AccessToken accessToken;
        private Organization organization;
        private GardenManager gardenManager = new GardenManager();


        /// <summary>
        /// Empty Constructor for Create Garden class.
        /// </summary>
        public CreateGarden()
        {
            organization = new Organization();/// Need to provide OrganizationID  
            accessToken = new AccessToken();/// Need to provide UserID

        }

        /// <summary>
        /// Constructor with the parameters.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="organization"></param>
        public CreateGarden(AccessToken accessToken, Organization organization)
        {
            InitializeComponent();


            FillGroupData(organization.OrganizationID);

        }

        /// <summary>
        /// Method to load page controls and bind group name combo box
        /// Load Group Name combo box from - "Groups" table 
        /// Group name , Group Id
        /// </summary>
        private void FillGroupData(int OrganizationID)
        {
            try
            {
                var groups = myGroupManager.GetGroupList(OrganizationID);

                cmbGroupName.ItemsSource = groups;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Method to Save Garden
        /// Get instance of Garden Manager and call create  garden method
        /// Pass field data check validation for null data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                 int groupId = int.Parse(cmbGroupName.SelectedValue.ToString());
                 string gDesc = txtDescription.Text.ToString();
                 string gRegion = txtRegion.Text.ToString();
                 Garden garden = new Garden();

                 if ((groupId.ToString() != string.Empty &&
                        gDesc.ToString() != string.Empty && gRegion.ToString() != string.Empty))
                 {

                     garden.GroupID = groupId;
                     garden.UserID = accessToken.UserID;
                     garden.GardenDescription = gDesc;
                     garden.GardenRegion = gRegion;


                     if (gardenManager.AddNewGarden(garden))
                    
                     {
                         MessageBox.Show("New Garden has been created.", "New Record", MessageBoxButton.OK, MessageBoxImage.Information);

                         //Reset Fields
                         FillGroupData(organization.OrganizationID);
                         txtDescription.Text = "";
                         txtRegion.Text = "";

                    
                     }

                 }
                 else
                 {
                     MessageBox.Show("Please enter all required fields", "New Record", MessageBoxButton.OK, MessageBoxImage.Information);
                 }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Cancel button refresh the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           
            this.Cursor = Cursors.Arrow;
            //Reset Fields
            FillGroupData(organization.OrganizationID);
            txtDescription.Text = "";
            txtRegion.Text = "";
        }





    }
}
