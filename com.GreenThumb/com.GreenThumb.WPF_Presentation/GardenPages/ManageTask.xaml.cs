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

/// <summary>
/// Retrieve and select a task for a garden
/// Created By: Nasr Mohammed 3/4/2016 
/// </summary>

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for ManageTask.xaml
    /// </summary>
    public partial class ManageTask : Page
    {


        private GardenManager gardenManager = new GardenManager();
        private JobManager jobManager = new JobManager();
        AccessToken accessToken = new AccessToken();
        Job joby = new Job();
        Garden gardy = new Garden();

        public ManageTask(AccessToken _accessToken)
        {
            InitializeComponent();
            if (_accessToken != null)
            {
                accessToken = _accessToken;
                DisplayGardenData(accessToken.UserID);
                //DisplayTaskData(gardy);
            }
            else { btnAddTask.IsEnabled = false; }

        }

        private void DisplayGardenData(int userID)
        {
            try
            {
                List<Garden> gardens = jobManager.GetGardensForUser(userID);
                int gar;
                gar = gardy.GardenID;

                cmbGardenName.ItemsSource = gardens;
                DisplayTaskData(gar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        public bool saveDetails()
        {
            bool myBool = false;

            try
            {

                int gardenId = int.Parse(cmbGardenName.SelectedValue.ToString());
                string gDesc = txtTaskDescription.Text.ToString();
                string gNote = txtuserNotes.Text.ToString();


                Job newJob = new Job();

                newJob.GardenID = gardenId;
                newJob.Description = this.txtTaskDescription.Text;
                newJob.DateAssigned = DateTime.Now;
                newJob.AssignedFrom = accessToken.UserID;
                newJob.UserNotes = this.txtuserNotes.Text;

                myBool = jobManager.AddNewTask(newJob);

            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }

            return myBool;
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {


            int gar;
            gar = gardy.GardenID;


            bool myBool = saveDetails();
            if (myBool == true)
            {
                MessageBox.Show("Your task created succssfully!");
                DisplayTaskData(gar);
                txtTaskDescription.Clear();
                txtuserNotes.Clear();

            }
            else if (myBool == false)
            {
                MessageBox.Show("Your task has not created succssfully, something went wrong!");
            }


        }

        private void btnUpdateTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowTasks_Click(object sender, RoutedEventArgs e)
        {
            int gar;
            gar = gardy.GardenID;
            this.grdTasks.Visibility = Visibility.Visible;
            DisplayTaskData(gar);
        }

        private void grdTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void DisplayTaskData(int gardenId)
        {
            try
            {
                var job = jobManager.GetTaskList();
                grdTasks.ItemsSource = job.Select(o => new { TaskDescription = o.Description, UserNotes = o.UserNotes, DateCreated = o.DateAssigned }).ToList();

            }
            catch (Exception)
            {

            }
        }
    }
}
