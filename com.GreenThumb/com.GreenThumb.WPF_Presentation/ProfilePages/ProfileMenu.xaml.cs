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

namespace com.GreenThumb.WPF_Presentation.ProfilePages
{
    /// <summary>
    /// Interaction logic for ProfileMenu.xaml
    /// Added by Ibrahim Abuzaid 04-15-2016
    /// </summary>
    public partial class ProfileMenu : Page
    {
        UserManager usrMgr = new UserManager();
        UserRoleManager usrRoleMgr = new UserRoleManager();
        GroupManager grpMgr = new GroupManager();

        private AccessToken _accessToken;
        
        public ProfileMenu(AccessToken _accessToken)
        {
            this._accessToken = _accessToken;
      //      ProfileMenu profMenu = new ProfileMenu(_accessToken);
            InitializeComponent();
            populateUser();
            frmEdit.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
        }
        private void populateUser()
        {
            try
            {
                var users = usrMgr.GetPersonalInfo(_accessToken.UserID);
                User user = new User();
             //   grdUser.ItemsSource = users;
                lblFirstName.Content = user.FirstName;
               
                if (user == null)
                {
                    lblMessage.Foreground = Brushes.Red;
                    lblMessage.Content = "Users NO: " + _accessToken.UserID + "  Not Found in DataBase, try again";      
                }
                else
                {
                    lblFirstName.Content = _accessToken.FirstName;
                    lblLastName.Content = _accessToken.LastName;
                    lblZip.Content = _accessToken.Zip;
                    lblMail.Content = _accessToken.EmailAddress;
                    lblUserName.Content = _accessToken.UserName;
                    lblRegion.Content =_accessToken.RegionId;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("USER..... failure here : " + ex.Message + ex.StackTrace);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            frmPassword.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmEdit.Visibility = Visibility.Visible;
            txtFirstName.Text = _accessToken.FirstName;
            txtLastName.Text = _accessToken.LastName;
            txtZip.Text = _accessToken.Zip;
            txtEmail.Text = _accessToken.EmailAddress;
            txtRegion.Text = _accessToken.RegionId.ToString();
            txtUserName.Text = _accessToken.UserName;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var users = usrMgr.GetPersonalInfo(_accessToken.UserID);
            User user = new User();
            frmEdit.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Visible;

            if (txtOldPassword.Password != null && txtOldPassword.Password != _accessToken.Password)
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
            try
            {
                var userRoles = usrRoleMgr.GetUserRoleListByUser(_accessToken.UserID);
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
            try
            {
                var groupManager = grpMgr.GetGroupsForUser(_accessToken.UserID);
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
            User user = new User();
            user.UserID = _accessToken.UserID;
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Zip = txtZip.Text;
            user.EmailAddress = txtEmail.Text;
            user.UserName = txtUserName.Text;
            user.RegionId = int.Parse(txtRegion.Text);          
            
            try
            {                  
                        var res = usrMgr.EditUserPersonalInfo(user.UserID, user.FirstName, user.LastName,
                                 user.Zip, user.EmailAddress, user.RegionId);
                        if (res == true)
                        {

                            lblMessage.Content = "Operation Succeeded. ";
                            _accessToken.FirstName = user.FirstName;
                            _accessToken.LastName = user.LastName;
                            _accessToken.UserName = user.UserName;
                            _accessToken.EmailAddress = user.EmailAddress;
                            _accessToken.Zip = user.Zip;
                            _accessToken.RegionId = user.RegionId;
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

        
    }
}
