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
    /// Author: Poonam Dubey
    /// Created on: Mar.31st.2016
    /// Interaction logic for ViewGroups.xaml
    /// </summary>
    public partial class ViewGroups : Page
    {
        private AccessToken _accessToken;
        private GroupManager _groupMgr = new GroupManager();
        public ViewGroups(AccessToken at)
        {
            InitializeComponent();
            this._accessToken = at;
            PopulateGroupList();
        }


        private void PopulateGroupList()
        {
            try
            {
                var groupList = _groupMgr.GetGroupsToView(_accessToken.UserID);
                dataGroupList.ItemsSource = groupList.OrderBy(s => s.Name);
            }
            catch (Exception)
            {
                dataGroupList.ItemsSource = null;
            }
        }

        /// <summary>
        /// Function called on row selection Created By : Poonam Dubey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGroupList.SelectedItem != null)
            {
                try
                {
                    var selectedGrp = (Group)dataGroupList.SelectedItem;
                    GroupRequest reqObj = new GroupRequest();
                    MessageBoxResult result = MessageBox.Show("Do you want to request to join " + selectedGrp.Name + " group", "Join Group", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        reqObj.GroupID = selectedGrp.GroupID;
                        reqObj.RequestDate = DateTime.Now;
                        reqObj.UserID = _accessToken.UserID;
                        reqObj.RequestStatus = 'P'; // this is for Pending status , since user is requesting
                        int count = _groupMgr.AddGroupMember(reqObj);
                        if (count == 1)
                        {
                            MessageBox.Show("Your request has been submitted successfully", "Request Submitted", MessageBoxButton.OK);
                        }
                        else
                        {
                            string msg = "You had already requested to join this group, your status is ";
                            if (count == 2)
                                msg = msg + "Pending";
                            else if (count == 3)
                                msg = msg + "Approved";
                            else if (count == 4)
                                msg = msg + "Denied";
                            MessageBox.Show(msg, "Information!", MessageBoxButton.OK);
                        }
                    }
                    else
                    {

                    }
                }
                catch
                {
                    MessageBox.Show("You must select a Group to join it");
                }
            }
        }
    }
}
