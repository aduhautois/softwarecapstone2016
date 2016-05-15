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
    /// Interaction logic for OrgManageUserGroups.xaml
    /// Created By: Trent Cullinan 02/24/2016
    /// </summary>
    public partial class OrgManageUserGroups : Page
    {
        private AccessToken accessToken;
        private OrgUserManager orgUserManager;
        private GroupMember orgMember;

        /// <summary>
        /// Initialize constructor and confirm access for an organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="orgUserManager">Initialized manager to be referenced.</param>
        /// <param name="orgMember">Member of organization to be referenced.</param>
        public OrgManageUserGroups(AccessToken accessToken, OrgUserManager orgUserManager, GroupMember orgMember)
        {
            InitializeComponent();

            this.accessToken = accessToken;
            this.orgUserManager = orgUserManager;
            this.orgMember = orgMember;

            BindOrganizationGroups();
        }

        // Created By: Trent Cullinan 02/24/2016
        private void BindOrganizationGroups()
        {
            try
            {
                dgrdGroups.ItemsSource = orgUserManager.GetOrgGroups(accessToken);
                dgrdGroups.Items.Refresh();
            }
            catch (Exception ex)
            {
                lblResponseMessage.Content = "Error: " + ex.Message;
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void BindUserLeads()
        {
            try
            {
                dgrdUserLeads.ItemsSource = orgUserManager.GetUserOrgGroups(accessToken, this.orgMember);
                dgrdUserLeads.Items.Refresh();
            }
            catch (Exception ex)
            {
                lblResponseMessage.Content = "Error: " + ex.Message;
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private bool CheckAccessToken(AccessToken accessToken, Organization organization)
        {
            return organization.OrganizationLeader.UserID == accessToken.UserID;
        }

        // Created By: Trent Cullinan 02/24/2016
        private void btnActionOne_Click(object sender, RoutedEventArgs e)
        {
            if (tiOrgGroups.IsSelected)
            {
                try
                {
                    var group = (Group)dgrdGroups.SelectedItem;

                    if (null != group)
                    {
                        lblResponseMessage.Content = orgUserManager.AddGroupLeader(
                            accessToken, group, this.orgMember) ?
                            "Added user as leader." : "Unable to add to group leaders.";

                        BindOrganizationGroups();
                    }
                    else
                    {
                        lblResponseMessage.Content = "Error: Please select a group.";
                    }
                }
                catch (Exception ex)
                {
                    lblResponseMessage.Content = "Error: " + ex.Message;
                }
            }
            else if (tiUserLeads.IsSelected)
            {
                try
                {
                    var group = (Group)dgrdUserLeads.SelectedItem;

                    if (null != group)
                    {
                        lblResponseMessage.Content = orgUserManager.RemoveGroupLeader(
                            accessToken, group, this.orgMember) ?
                            "Removed as group leader." : "Unable to remove from group leaders.";

                        BindUserLeads();
                    }
                    else
                    {
                        lblResponseMessage.Content = "Error: Please select a group.";
                    }
                }
                catch (Exception ex)
                {
                    lblResponseMessage.Content = "Error: " + ex.Message;
                }
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void btnActionTwo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var group = (Group)dgrdGroups.SelectedItem;

                if (null != group)
                {
                    lblResponseMessage.Content = orgUserManager.EditPrimaryLeader(
                        accessToken, group, this.orgMember) ?
                        "Set user as primary group leader." : "Unable to set as primary group leader.";

                    BindOrganizationGroups();
                }
                else
                {
                    lblResponseMessage.Content = "Error: Please select a group.";
                }
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

            if (tiOrgGroups.IsSelected)
            {
                BindOrganizationGroups();

                btnActionOne.Content = "Add Leader";
                btnActionTwo.Content = "Add Primary";
                btnActionTwo.Visibility = Visibility.Visible;
            }
            else if (tiUserLeads.IsSelected)
            {
                BindUserLeads();

                btnActionOne.Content = "Remove Leader";
                btnActionTwo.Visibility = Visibility.Hidden;
            }
        }

        // Created By: Trent Cullinan 02/24/2016
        private void dgrdUserLeads_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        // Created By: Trent Cullinan 02/24/2016
        private void dgrdGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
