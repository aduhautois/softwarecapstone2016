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

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for OrgGroupLeaderManager.xaml
    /// Created By: Trent Cullinan 02/24/2016
    /// </summary>
    public partial class OrgGroupLeaderManager : Page
    {
        private AccessToken accessToken;
        private OrgUserManager orgUserManager;
        private OrgRequestsManager orgRequestManager;

        /// <summary>
        /// Initialize constructor and confirm access for an organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="organization">Organization to be referenced.</param>
        public OrgGroupLeaderManager(AccessToken accessToken, Organization organization)
        {
            InitializeComponent();

            try
            {
                orgUserManager = new OrgUserManager(accessToken, organization);
                orgRequestManager = new OrgRequestsManager(accessToken, organization);

                this.accessToken = accessToken;
            }
            catch (Exception ex)
            {
                lblResponseMessage.Content = "Error: " + ex.Message;
            }

            BindOrganizationUsers();
        }

        // Created By: Trent Cullinan 02/24/2016
        private void BindOrganizationUsers()
        {
            try
            {
                dgrdUsers.ItemsSource = orgUserManager.GetOrgUsers(this.accessToken);
                dgrdUsers.Items.Refresh();
            }
            catch (Exception ex)
            {
                lblResponseMessage.Content = "Error: " + ex.Message;
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void dgrdUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblResponseMessage.Content = string.Empty;
            btnActionOne.Content = ((GroupMember)dgrdUsers.SelectedItem).Leader ?
                "Demote" : "Promote";
        }

        // Created By: Trent Cullinan 02/24/2016
        private void btnActionOne_Click(object sender, RoutedEventArgs e)
        {
            if (tiOrgUsers.IsSelected)
            {
                GroupMember user = (GroupMember)dgrdUsers.SelectedItem;

                try
                {
                    lblResponseMessage.Content = orgUserManager.EditUserLeader(this.accessToken, user) ?
                        "User changed!" : "Sorry, selected user could not be changed.";

                    BindOrganizationUsers();

                    btnActionOne.Content = user.Leader ?
                        "Demote" : "Promote";
                }
                catch (Exception ex)
                {
                    lblResponseMessage.Content = "Error: " + ex.Message;
                }
            }
            else if (tiRequests.IsSelected)
            {
                GroupLeaderRequest request = (GroupLeaderRequest)dgrdRequests.SelectedItem;

                if (null != request)
                {
                    try
                    {
                        lblResponseMessage.Content = orgRequestManager.EditApproveRequest(
                            this.accessToken, request, orgUserManager.GetGroupMember(accessToken, request.User)) ?
                            "Request Approved" : "Unable to approve";

                        BindLeaderRequests();
                    }
                    catch (Exception ex)
                    {
                        lblResponseMessage.Content = "Error: " + ex.Message;
                    }
                }
                else
                {
                    lblResponseMessage.Content = "Error: Please select a request.";
                }
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void btnActionTwo_Click(object sender, RoutedEventArgs e)
        {
            if (tiOrgUsers.IsSelected)
            {
                GroupMember user = (GroupMember)dgrdUsers.SelectedItem;

                try
                {
                    OrgManageUserGroups orgManageUserGroups = new OrgManageUserGroups(
                        this.accessToken, this.orgUserManager, user);

                    NavigationService.Navigate(orgManageUserGroups);
                }
                catch (Exception ex)
                {
                    lblResponseMessage.Content = "Error: " + ex.Message;
                }
            }
            else if (tiRequests.IsSelected)
            {
                GroupLeaderRequest request = (GroupLeaderRequest)dgrdRequests.SelectedItem;

                if (null != request)
                {
                    try
                    {
                        lblResponseMessage.Content = orgRequestManager.EditDeclineRequest(accessToken, request) ?
                            "Request Declined" : "Unable to decline";

                        BindLeaderRequests();
                    }
                    catch (Exception ex)
                    {
                        lblResponseMessage.Content = "Error: " + ex.Message;
                    }
                }
                else
                {
                    lblResponseMessage.Content = "Error: Please select a request.";
                }
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void BindLeaderRequests()
        {
            try
            {
                dgrdRequests.ItemsSource = orgRequestManager.GetOrgRequests(accessToken);
                dgrdRequests.Items.Refresh();
            }
            catch (Exception ex)
            {
                lblResponseMessage.Content = "Error: " + ex.Message;
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void tcScreens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblResponseMessage.Content = string.Empty;

            if (tiOrgUsers.IsSelected)
            {
                BindOrganizationUsers();

                btnActionOne.Content = "Promote";
                btnActionTwo.Content = "Manage";
            }
            else if (tiRequests.IsSelected)
            {
                BindLeaderRequests();

                btnActionOne.Content = "Approve";
                btnActionTwo.Content = "Decline";
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void dgrdRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true; // Required or else events will boil up
        }
    }
}
