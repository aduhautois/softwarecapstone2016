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


namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    ///<summary>
    ///Author: Stenner Kvindlog         
    ///gets title and description for expert status request
    ///Date: 3/19/16
    ///</summary>
    public partial class RequestExpert : Page
    {
        AccessToken CurrentUser = new AccessToken();
        String Title;
        String Description;
        int userID;
        DateTime Time;


        public RequestExpert(AccessToken ax)
        {
            InitializeComponent();
            CurrentUser = ax;
            userID = CurrentUser.UserID;
            Time = DateTime.Now;
        }

        /// <summary>
        /// Created by: Stenner 
        /// Date: 3/25/16
        /// </summary>
        /// <remarks>
        /// Updated by: Chris Sheehan
        /// Date: 4/28/16 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            // save and submit to database 
            if (this.title.Text == "" || this.title.Text == null || this.description.Text == "" || this.description.Text == null)
            {
                MessageBox.Show("Please fill out both fields before submitting.");
            }
            else
            {
                Title = this.title.Text;
                Description = this.description.Text;

                AdminExpertRequestsManager myAdminExpertRequestsManager = new AdminExpertRequestsManager(CurrentUser);

                try
                {
                    myAdminExpertRequestsManager.AddExpertApplication(Title, Description, userID, Time);
                }
                catch (Exception)
                {
                    MessageBox.Show("Record not updated.  Please fill in all fields.");
                }
                finally
                {
                    MessageBox.Show("Your request has been submitted.  Thank you.");
                    this.NavigationService.Navigate(new HomePages.ViewBlog(CurrentUser));
                }
            }
            
            
        }
        /// <summary>
        /// Created by: Stenner
        /// Date: 3/25/16
        /// </summary>
        /// <remarks>
        /// Updated by: Chris Sheehan
        /// Date: 4/28/16
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            // send user back to pervious page      
            this.NavigationService.Navigate(new HomePages.ViewBlog(CurrentUser));
        }
 
    }
}
