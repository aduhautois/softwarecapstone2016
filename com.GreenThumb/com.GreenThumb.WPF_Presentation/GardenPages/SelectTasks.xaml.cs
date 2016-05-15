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
    /// Interaction logic for SelectTask.xaml
    /// Dat Tran
    /// </summary>
    public partial class SelectTasks : Page
    {
        private JobManager jobManager = new JobManager();
        private AccessToken accessToken;
        public SelectTasks(AccessToken _accessToken)
        {
            accessToken = _accessToken;
            InitializeComponent();
        }

        private void ShowTasks_Click(object sender, RoutedEventArgs e)
        {
            ShowTasks();
        }
        private void ShowTasks()
        {

            try
            {
                var job = jobManager.GetTaskList();

                grdTasks.ItemsSource = job;


            }
            catch (Exception)
            {

            }
        }

        private void SelecteTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var job = (Job)grdTasks.SelectedItem;
                var oldJob = (Job)grdTasks.SelectedItem;
                int userID = job.AssignedTo;
                if (userID == null)
                {
                    job.AssignedTo = accessToken.UserID;
                    job.DateAssigned = DateTime.Now;

                    if (jobManager.EditTask(job, oldJob) == true)
                    {
                        MessageBox.Show("You've picked this task successfuly");
                        // commented out for test purposes.
                        //DisplayTaskData();
                    }
                    else { MessageBox.Show("Something's gone wrong. Please pick another task."); }
                }
                else { MessageBox.Show("Tast has been assigned to someone else! Please pick another task"); }
            }
            catch (Exception)
            {

            }
        }
    }
}
