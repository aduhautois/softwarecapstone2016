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

namespace com.GreenThumb.WPF_Presentation.AdminPages
{
    
    //public partial class RegionPage : Page
    //{
    //    UserRegionManager myUserRegionManager = new UserRegionManager();
    //    public RegionPage()
    //    {
    //        InitializeComponent();
    //    }

    //    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        txtRegionID.Text = (regions.SelectedIndex + 1).ToString();
    //    }

    //    private void btnSearch_Click(object sender, RoutedEventArgs e)
    //    {
    //        while (txtUserID.Text == null)
    //        {
    //            lblMessage.Foreground = Brushes.Red;
    //            lblMessage.Content = "Users NO. Can't be empty, you have to enter a valid User Number! ";
    //        }

    //        populateUser();

    //    }
    //    private void populateUser()
    //    {
    //        try
    //        {
    //            lblMessage.Foreground = Brushes.Black;
    //            lblMessage.Content = "";

    //            int userNo = int.Parse(txtUserID.Text);

    //            User users = (User)myUserRegionManager.GetAndDisplayUserRecord(userNo);

    //            if (users == null)
    //            {
    //                lblMessage.Foreground = Brushes.Red;
    //                lblMessage.Content = "Users NO: " + userNo + "  Not Found in DataBase, try again";
    //                txtUserID.Text = null;
    //                txtUserName.Text = null;
    //                txtRegionID.Text = null;
    //            }
    //            else
    //            {
    //                txtUserName.Text = users.UserName;
    //                if (users.RegionID == -1)
    //                {
    //                    txtRegionID.Text = "";
    //                }
    //                else
    //                {
    //                    txtRegionID.Text = users.RegionID.ToString();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //       grdUserList.ItemsSource = null;
    //            //       lblUserCount.Content = "Count: 0";
    //            MessageBox.Show("USER..... failure here : " + ex.Message + ex.StackTrace);


    //        }
    //    }

    //    private void btnSave_Click(object sender, RoutedEventArgs e)
    //    {
    //        try
    //        {
    //            //          var count = myUserManager.GetUserCount(Active.active);
    //            //          lblUserCount.Content = "Count: " + count.ToString();
    //            int userNo = int.Parse(txtUserID.Text);
    //            int regionNo = int.Parse(txtRegionID.Text);

    //            bool updateStatus = myUserRegionManager.EditUserData(userNo, regionNo);
    //            lblMessage.Content = "User NO: " + userNo + " RegionID: " + regionNo
    //                + "   UpdateStatus: " + updateStatus;

    //            if (updateStatus == false)
    //            {
    //                lblMessage.Content = "User NO: " + userNo + " is not Updated, try again";
    //                txtUserID.Text = null;
    //                txtUserName.Text = null;
    //                txtRegionID.Text = null;
    //            }
    //            else
    //            {
    //                lblMessage.Content = "User NO: " + userNo + "  Updated Succesfully";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //       grdUserList.ItemsSource = null;
    //            //       lblUserCount.Content = "Count: 0";
    //            MessageBox.Show("USER..... failure here : " + ex.Message + ex.StackTrace);
    //            lblMessage.Content = "Reached Exception in User Update... ";
    //        }
    //    }
    //}
}
