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

namespace com.GreenThumb.WPF_Presentation.AdminPages
{
    /// <summary>
    /// Interaction logic for ProfileMenu.xaml
    /// Added by Ibrahim Abuzaid 04-15-2016
    /// </summary>
    public partial class ProfileAdmin : Page
    {
        UserManager usrMgr = new UserManager();
        UserRoleManager usrRoleMgr = new UserRoleManager();
        GroupManager grpMgr = new GroupManager();

        private AccessToken _accessToken;
        
        public ProfileAdmin(AccessToken _accessToken)
        {
            if(null != _accessToken){
                this._accessToken = _accessToken;
            }
            InitializeComponent();
            populateUser();
            //frmEdit.Visibility = Visibility.Hidden;
            //frmPassword.Visibility = Visibility.Hidden;
            //frmRole.Visibility = Visibility.Hidden;
            //grdGarden.Visibility = Visibility.Hidden;
        }
        private void populateUser()
        {
            int userIn = int.Parse(txtUserIn.Text);

            try
            {
         //       User user = new User();
                var users = usrMgr.GetPersonalInfo(userIn);
               
             //   grdUser.ItemsSource = users;
                lblFirstName.Content = users.FirstName;
               
                if (users == null)
                {
                    lblMessage.Foreground = Brushes.Red;
                    lblMessage.Content = "Users NO: " + users.UserID + "  Not Found in DataBase, try again";      
                }
                else
                {
                    lblFirstName.Content = users.FirstName;
                    lblLastName.Content = users.LastName;
                    lblZip.Content = users.Zip;
                    lblMail.Content = users.EmailAddress;
                    lblUserName.Content = users.UserName;
                    lblRegion.Content = users.RegionId;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("USER..... failure here : " + ex.Message + ex.StackTrace);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int userIn = int.Parse(txtUserIn.Text);
            var users = usrMgr.GetPersonalInfo(userIn);

            frmPassword.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmEdit.Visibility = Visibility.Visible;
            txtFirstName.Text = users.FirstName;
            txtLastName.Text = users.LastName;
            txtZip.Text = users.Zip;
            txtEmail.Text = users.EmailAddress;
            txtRegion.Text= users.RegionId.ToString();
            txtRegion.IsEnabled = false;
            txtUserName.Text = users.UserName;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            int userIn = int.Parse(txtUserIn.Text);
            var users = usrMgr.GetPersonalInfo(userIn);
            User user = new User();
            frmEdit.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Visible;

            if (txtOldPassword.Password != null && txtOldPassword.Password != users.Password)
            {
                lblMessage.Content = "Invalid old Password";
            }

            if (txtNewPassword2.Password == null || txtNewPassword1.Password == null)
            {
               lblMessage.Content = "enter new Password twice";
            } 

            if (txtNewPassword2.Password != txtNewPassword1.Password)
            {
                lblMessage.Content = "new Password doesn't match!";
            } 

        }
        private void btnPasswordSave_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            
            try
            {
                var res = usrMgr.EditPasssword(txtUserName.Text, txtOldPassword.Password, txtNewPassword1.Password);
                lblMessage.Content = txtUserName.Text + "  " + txtOldPassword.Password + "   " + txtNewPassword1.Password;
                if (res == true)
                {
                    lblMessage.Content = "Operation Succeeded. ";
                    
                }
                else
                {
                    lblMessage.Content = "Operation failed. ";
                }
            }
            catch (Exception)
            {
                lblMessage.Content = "Operation Failed, check out!";
            }
            finally
            {
                txtOldPassword.Password = "";
                txtNewPassword1.Password = "";
                txtNewPassword2.Password = "";
                frmPassword.Visibility = Visibility.Hidden;
                populateUser();
            }
        }
         
        private void btnUserRoles_Click(object sender, RoutedEventArgs e)
        {
            frmEdit.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Visible;
            int userIn = int.Parse(txtUserIn.Text);
            try
            {
                var userRoles = usrRoleMgr.GetUserRoleListByUser(userIn);
                grdUserRoleList.ItemsSource = userRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("USER Role..... failure here : " + ex.Message + ex.StackTrace);
            }
        }

        
        private void btnGarden_Click(object sender, RoutedEventArgs e)
        {
            frmEdit.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Hidden;         
            frmRole.Visibility = Visibility.Hidden;
            grdMap.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Visible;
            int userIn = int.Parse(txtUserIn.Text);
            try
            {
                var groupManager = grpMgr.GetGroupsForUser(userIn);
                grdGarden.ItemsSource = groupManager;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Garden..... failure here : " + ex.Message + ex.StackTrace);
            }
            grdMap.Visibility = Visibility.Visible;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int userIn = int.Parse(txtUserIn.Text);
            User user = new User();

            user.UserID = userIn;
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Zip = txtZip.Text;
            user.EmailAddress = txtEmail.Text;
            user.UserName = txtUserName.Text;

            if (txtRegion.Text.Trim() == "" || txtRegion.Text == null)
            {
                user.RegionId = null;
            }
            else
            {
                user.RegionId = int.Parse(txtRegion.Text);
            }
            
            try
            {                  
                        var res = usrMgr.EditUserPersonalInfo(user.UserID, user.FirstName, user.LastName,
                                 user.Zip, user.EmailAddress, user.RegionId);
                        if (res == true)
                        {

                            lblMessage.Content = "Operation Succeeded. ";
                            txtFirstName.Text = user.FirstName;
                            txtLastName.Text = user.LastName;
                            txtUserName.Text = user.UserName;
                            txtEmail.Text = user.EmailAddress;
                            txtZip.Text = user.Zip;
                            txtRegion.Text = user.RegionId.ToString();

                            populateUser();
                        }
                        else
                        {
                            lblMessage.Content = "Operation failed. ";
                        }
                       

            }
            catch (Exception)
            {
                lblMessage.Content = "Operation Failed, check out!";
            }
            finally
            {
                
               
            }
        }
        public void btnBack_Click(object sender, RoutedEventArgs e)
        {
            frmEdit.Visibility = Visibility.Hidden;       
        }
        public void btnPasswordBack_Click(object sender, RoutedEventArgs e)
        {
            frmPassword.Visibility = Visibility.Hidden;
        }
        public void DataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
             frmPassword.Visibility = Visibility.Hidden;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (regions.SelectedIndex + 1 == 11)
            {
                txtRegion.Text = "";
            }
            else
            {
                txtRegion.Text = (regions.SelectedIndex + 1).ToString();
            }
        }

        private void txtUserIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtUserIn.Text = txtUserIn.Text.Trim();
        }
        
    }
}
