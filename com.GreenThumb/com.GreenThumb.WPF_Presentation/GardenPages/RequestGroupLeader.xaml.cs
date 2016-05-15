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

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for RequestGroupLeader.xaml
    /// </summary>
    public partial class RequestGroupLeader : Page
    {
        private GroupLeaderRequestManager _manager;
        private AccessToken _acctoken;
        private List<Group> _userGroups = null;
        private string _selectedGroup = "";

        public RequestGroupLeader(AccessToken acctoken)
        {
            _acctoken = acctoken;
            _manager = new GroupLeaderRequestManager(_acctoken);
            _userGroups = _manager.GetUserGroups();

            InitializeComponent();
            for (int i = 0; i < _userGroups.Count; i++)
            {
                cmbGroupList.Items.Add(_userGroups[i].Name);
            }
        }

        private void btnRequestLeader_Click(object sender, RoutedEventArgs e)
        {
            string msg = _manager.AddGroupLeaderRequest(_selectedGroup);
            lblMessage.Content = msg;
        }

        private void cmbGroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedGroup = cmbGroupList.SelectedValue.ToString();
        }
    }
}
