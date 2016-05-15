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
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Page
    {
        private AddRequestManager myManager;
        private AccessToken myAccessToken;
        private List<Group> myUserGroups = null;
        private string mySelectedGroup = "";

        public AddRequest(AccessToken myaccesstoken)
        {
            myAccessToken = myaccesstoken;
            myManager = new AddRequestManager(myAccessToken);
            myUserGroups = myManager.GetUserGroups();

            InitializeComponent();
            for (int i = 0; i < myUserGroups.Count; i++)
            {
                cmbMyGroupList.Items.Add(myUserGroups[i].Name);
            }
        }

        private void btnRequestLeader_Click(object sender, RoutedEventArgs e)
        {
            string msg = myManager.AddGroupLeaderRequest(mySelectedGroup);
            lblMessage.Content = msg;
        }

        private void cmbMyGroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mySelectedGroup = cmbMyGroupList.SelectedValue.ToString();
        }
    }
}
