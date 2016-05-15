using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;
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
using com.GreenThumb.BusinessLogic.Interfaces;
using System.Text.RegularExpressions;


namespace com.GreenThumb.WPF_Presentation.VolunteerPages
{
    /// <summary>
    /// Interaction logic for EditVolunteerAvailability.xaml
    /// Author: Emily West
    /// Allows You to edit the hours you are available to volunteer
    /// </summary>
    public partial class EditVolunteerAvailability : Page
    {
        private DateTime startTime;
        private DateTime finishTime;
        private DateTime datePledged;
        private AccessToken _accessToken;
        private DonationManager donationManager = new DonationManager();
       
        public EditVolunteerAvailability(AccessToken _accessToken)
        {
            InitializeComponent();
        }

      

       
        private void btnSubmitHours_Click(object sender, RoutedEventArgs e)
        {
            donationManager.AddVolunteerHours(startTime, finishTime, datePledged, _accessToken.UserID);
        }

        private void cmbbxStartTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
             startTime = (DateTime)cmbbxStartTime.SelectedItem;
            
            
        }
        private void cmbbxFinishTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           finishTime = (DateTime)cmbbxStartTime.SelectedItem;


        }
        private void cldrDateSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datePledged = (DateTime)cldrDateSelect.SelectedDate;
        }
    }
}
