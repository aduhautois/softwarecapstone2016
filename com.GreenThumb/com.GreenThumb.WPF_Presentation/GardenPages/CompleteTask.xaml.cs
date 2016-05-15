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

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for CompleteTask.xaml
    /// Created by: Steve Hoover 3/24/16
    /// </summary>
    public partial class CompleteTask : Page
    {
        private JobManager jobManager = new JobManager();
        private AccessToken accessToken;


        public CompleteTask(AccessToken _accessToken)
        {
            accessToken = _accessToken;
            //Console.WriteLine(_accessToken.UserID);
            InitializeComponent();
        }

        private void CompleteSelectedTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // uses existing methods to update DateTime and Active
                // make two copies of job; one to change, one to keep as it sits.
                var job = (Job)grdTasks.SelectedItem;
                var oldJob = (Job)grdTasks.SelectedItem;
                int userId = job.AssignedTo;
                // TEST DATA
                //if(userId == 1001)
                // REAL DATA
                if (userId == accessToken.UserID)
                {
                    var date = DateTime.Now;
                    job.DateCompleted = date;
                    job.Active = false;

                    if (jobManager.EditTask(job, oldJob) == true)
                    {
                        MessageBox.Show("Task completed successfully!");
                        DisplayTaskData();
                    }
                    else { MessageBox.Show("Something's gone wrong. Please pick another task."); }
                }
            }
            catch (Exception)
            {

            }
        }

        private void ShowTasks_Click(object sender, RoutedEventArgs e)
        {
            jobManager.AddTestUser(accessToken.UserID);
            DisplayTaskData();
        }

        private void DisplayTaskData()
        {

            try
            {
                // This will only display records that are assigned to the active user.
                // TEST DATA
                //var jobs = jobManager.RetrieveJobByUserId(1001);
                // REAL DATA
                var jobs = jobManager.RetrieveJobByUserId(accessToken.UserID);
                if (jobs == null)
                {
                    MessageBox.Show("You have no tasks! Go sign up for one!");
                }
                else
                {
                    grdTasks.ItemsSource = jobs;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
