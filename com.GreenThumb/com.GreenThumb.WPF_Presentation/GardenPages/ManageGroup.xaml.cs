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
    /// Luke Frahm
    /// Created: 03/31/16
    /// Interaction logic for ManageGroup.xaml
    /// </summary>
    public partial class ManageGroup : Page
    {
        GroupManager _grpManager = new GroupManager();
        string originalGroupName;
        Group _group;
        AccessToken _accessToken;
        List<GroupMember> _memberList;

        public ManageGroup(AccessToken incomingToken, Group group)
        {
            InitializeComponent();
            _group = group;
            _accessToken = incomingToken;
            txtGroupName.Text = group.Name;
            originalGroupName = txtGroupName.Text;
            PopulateMemberList();
        }

        /// <summary>
        /// Luke Frahm
        /// Created: 03/31/16
        /// Logic to change name
        /// </summary>
        private void btnGroupNameChange_Click(object sender, RoutedEventArgs e)
        {
            txtGroupName.IsEnabled = true;
            txtGroupName.Focus();
            txtGroupName.SelectAll();
            btnGroupNameChange.Visibility = Visibility.Hidden;
            btnGroupNameApply.Visibility = Visibility.Visible;
            btnGroupNameCancel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Luke Frahm
        /// Created: 03/31/16
        /// During name change event, this button appears to allow the user to cancel changes
        /// </summary>
        private void btnGroupNameCancel_Click(object sender, RoutedEventArgs e)
        {
            txtGroupName.Text = _group.Name;
            btnGroupNameChange.Visibility = Visibility.Visible;
            btnGroupNameApply.Visibility = Visibility.Hidden;
            btnGroupNameCancel.Visibility = Visibility.Hidden;
            txtGroupName.IsEnabled = false;
        }

        /// <summary>
        /// Luke Frahm
        /// Created: 03/31/16
        /// During name change event, this button appears to allow the user to apply changes
        /// </summary>
        private void btnGroupNameApply_Click(object sender, RoutedEventArgs e)
        {
            btnGroupNameChange.Visibility = Visibility.Visible;
            btnGroupNameApply.Visibility = Visibility.Hidden;
            btnGroupNameCancel.Visibility = Visibility.Hidden;
            txtGroupName.IsEnabled = false;
            if (!txtGroupName.Text.Equals("") || !txtGroupName.Text.Equals(_group.Name))
            {
                try
                {
                    _grpManager.EditGroupName(_group.GroupID, txtGroupName.Text, _group.Name);
                    _group.Name = txtGroupName.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        /// <summary>
        /// Luke Frahm
        /// Created: 03/31/16
        /// This button activates logic to deactivate the group.
        /// </summary>
        private void btnCloseGroup_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you wish to deactivate this group?", "Deactivate Group", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                bool success = _grpManager.EditDeactivateGroup(_group);
                if (success)
                {
                    this.NavigationService.Navigate(new GardenPages.GroupMain(_accessToken));
                }
                else
                {
                    throw new ApplicationException("The group could not be deactivated");
                }
            }

        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 3/31/16
        /// Used to populate the member list in the datagrid view.
        /// </summary>
        private void PopulateMemberList()
        {
            try
            {
                _memberList = _grpManager.GetGroupMembers(_group.GroupID);
                List<User> users = new List<User>();
                foreach (GroupMember g in _memberList)
                {
                    users.Add(g.User);
                }
                dataRequestList.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dataRequestList.ItemsSource = null;
            }

        }

        private void btnMemberStatusDeny_Click(object sender, RoutedEventArgs e)
        {
            // change member status
            PopulateMemberList();
        }

        private void btnMemberStatusApprove_Click(object sender, RoutedEventArgs e)
        {
            // change member status
            PopulateMemberList();
        }

        private void btnMessageAllMembers_Click(object sender, RoutedEventArgs e)
        {
            if (null == _memberList)
            {
                PopulateMemberList();
            }
            this.NavigationService.Navigate(new ProfilePages.MessageCompose(_accessToken, true, _memberList, _group));
        }
    }
}
