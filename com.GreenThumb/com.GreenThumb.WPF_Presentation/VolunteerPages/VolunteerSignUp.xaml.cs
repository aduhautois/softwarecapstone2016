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
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.WPF_Presentation.VolunteerPages
{
    /// <summary>
    /// Interaction logic for VolunteerSignUp.xaml
    /// </summary>
    public partial class VolunteerSignUp : Page
    {
        VolunteerManager _volMgr = new VolunteerManager();
        AccessToken accessToken = new AccessToken();
        public VolunteerSignUp(AccessToken _accessToken)
        {
            accessToken = _accessToken;
            InitializeComponent();
        }
        public bool saveVolunteerDetails()
        {
            //validate feilds  implement createdBy user
            bool myBool = false;

            try
            {
                Volunteer newVolunteer = new Volunteer();

                
                newVolunteer.UserID = int.Parse(this.UserID.Text);
                newVolunteer.DateWillVolunteer = DateTime.Parse(DateCanVolunteer.Text);
                newVolunteer.Description = this.description.Text;
                _volMgr.AddVolunteer(newVolunteer);

            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }

            return myBool;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            bool volunteerBool = saveVolunteerDetails();
            if (volunteerBool == true)
            {
                MessageBox.Show("You are a volunteer.");
            }
            else if (volunteerBool == false)
            {
                MessageBox.Show("Your attempt to sign up to volunteer was unsuccessful.");
            }
         
        }
    }
}
