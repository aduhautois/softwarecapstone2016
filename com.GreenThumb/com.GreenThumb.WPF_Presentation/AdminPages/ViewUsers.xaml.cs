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

namespace com.GreenThumb.WPF_Presentation.AdminPages
{
    /// <summary>
    /// Stenner kvindlog 4/22
    /// populate list view with users
    /// Interaction logic for ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : Page
    {
        UserManager _myUserManager = new UserManager();
        AccessToken _accessToken;

        public ViewUsers(AccessToken _AccessToken)
        {
            InitializeComponent();
            populateUserList();
            _accessToken = _AccessToken;
        }

        private void populateUserList()
        {

            //load user info and put in list view 
            try
            {
                List<User> _myUserList = _myUserManager.GetUserList(Active.active);             
                lvUsers.Items.Clear();
                this.lvUsers.ItemsSource = _myUserList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }  
    }
}
