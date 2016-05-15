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
    /// Interaction logic for AssignTask.xaml
    /// </summary>
    public partial class AssignTask : Page
    {
        private UserManager userManager = new UserManager();
        private JobManager jobManager = new JobManager();
        private AccessToken accessToken;
        public AssignTask(AccessToken _accessToken)
        {
            _accessToken = accessToken;
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

        private void ShowAllUser_Click(object sender, RoutedEventArgs e)
        {
            ShowUser();
        }
        private void ShowUser()
        {
            try
            {
                var user = userManager.GetUserList();

                grdUsers.ItemsSource = user;
            }
            catch (Exception)
            {

            }
        }
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            ShowUserByUserName();
        }
        private void ShowUserByUserName()
        {
            int userID = int.Parse(UserIDtxt.Text);
            try
            {
                var user = userManager.GetPersonalInfo(userID);
                grdUsers.ItemsSource = new List<User>{user};


                
            }
            catch(Exception)
            {

            }
        }

        private void AssignTask_Click(object sender, RoutedEventArgs e)
        {
            try
                {
                var user = (User)grdUsers.SelectedItem;
                var job = (Job)grdTasks.SelectedItem;
                var oldJob = (Job)grdTasks.SelectedItem;
                int userID = job.AssignedTo;
                if (userID == null)
                {
                    job.AssignedTo = user.UserID;
                    job.AssignedFrom = accessToken.UserID;
                    
                    job.DateAssigned = DateTime.Now;

                    if (jobManager.EditTask(job, oldJob) == true)
                    {
                        MessageBox.Show("You've assigned this task successfuly");
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
