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

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccessToken _accessToken = null;
        Login _login;
        RoleManager roleManager = new RoleManager();
        MessageManager messageMgr = new MessageManager();
        NewUserCreation _newUser;

        public AccessToken LoggedAccessToken
        {
            get
            {
                return _accessToken;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //clearUnusedSidebars();
            /// Added by Trevor
            /// Checking to see if there are users in the DB----- If not prompt to create admin account
            UserManager um = new UserManager();
            int users = um.GetUserCount();
            if (users == 0)
            {
                _newUser = new NewUserCreation(true);
                _newUser.AccessTokenCreatedEvent += setAccessToken;
                _newUser.ShowDialog();
                if (_accessToken != null)
                {
                    this.btnLogin.Content = "Log Out";
                }
            }

            mainFrame.NavigationService.Navigate(new HomeContent(_accessToken));
            CheckPermissions();
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Check permissions, show the appropriate tabs based on perms
        /// Date: 3/24/16
        /// </summary>
        /// <remarks>
        /// Sara Nanke
        /// Updated: 4/22
        /// Code cleanup
        /// </remarks>
        private void CheckPermissions()
        {
            List<Label> visibleTabs = new List<Label>();
            var allTabs = new Label[] { btnAdmin, btnGardens, btnExpert, btnHome, btnProfile };
            btnGardens.Visibility = Visibility.Collapsed;
            if (_accessToken == null)
            {
                clearSideBar();
            }
            if (_accessToken != null)
            {
                foreach (Role r in _accessToken.Roles)
                {
                    if (r.RoleID == "Admin")
                    {
                        var adminTabs = new Label[] { btnAdmin, btnExpert, btnHome, btnProfile };
                        visibleTabs.AddRange(adminTabs);
                        break;
                    }
                    if (r.RoleID == "Expert")
                    {
                        var expertTabs = new Label[] { btnGardens, btnHome, btnProfile };
                        visibleTabs.AddRange(expertTabs);
                        break;
                    }
                    if (r.RoleID == "GroupLeader")
                    {
                        var expertTabs = new Label[] { btnGardens, btnHome, btnProfile };
                        visibleTabs.AddRange(expertTabs);
                        break;
                    }
                    if (r.RoleID == "GroupMember")
                    {
                        var expertTabs = new Label[] { btnExpert, btnHome, btnProfile };
                        visibleTabs.AddRange(expertTabs);
                        break;
                    }
                    if (r.RoleID == "User")
                    {
                        var expertTabs = new Label[] { btnExpert, btnHome, btnProfile };
                        visibleTabs.AddRange(expertTabs);
                        break;
                    }
                }
            }
            else
            {
                var guestTabs = new Label[] { btnHome };
                visibleTabs.AddRange(guestTabs);
            }
            foreach (Label tab in allTabs)
            {
                tab.Visibility = Visibility.Collapsed;
            }
            foreach (Label tab in visibleTabs)
            {
                tab.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for login button
        /// Date: 3/6/16
        /// </summary>
        /// <remarks>
        /// Ryan Taylor
        /// Updated: 2016/03/07
        /// Fixed the access token creation event
        /// </remarks>
        public void Login_Click(object sender, RoutedEventArgs e)
        {
            if (null == _accessToken)
            {
                _login = new Login();
                _login.AccessTokenCreatedEvent += setAccessToken;
                if (_login.ShowDialog() == true && _accessToken != null) // login succeeded
                {
                    this.btnLogin.Content = "Log Out";
                    // this is where we will set the initial privilages based on roles
                    CheckPermissions();
                    SetHomeButtons();
                }
                else
                {
                    // clear the access token reference
                    _accessToken = null;
                    lblLoggedIn.Content = "";
                    CheckPermissions();
                }
            }
            else // somebody is already logged in
            {
                _accessToken = null;
                this.btnLogin.Content = "Log In";
                // change things back to default here.
                lblLoggedIn.Content = "";
                CheckPermissions();
                btnSignUp.Visibility = System.Windows.Visibility.Visible;
                mainFrame.NavigationService.Navigate(new HomeContent(_accessToken));
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for Loggged in button
        /// this button displays the person that is logged in, and will go to profile menu when clicked (when done)
        /// Date: 3/6/16
        /// </summary>
        private void lblLoggedIn_Click(object sender, RoutedEventArgs e)
        {
            btnProfile.Focus();
            mainFrame.NavigationService.Navigate(new ProfilePages.ProfileMain(_accessToken));
            CheckPermissions();
            //SetProfileButtons();
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/05/2016
        /// A method to subscribe to the login event that sets access token.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a">The access token being sent</param>
        private void setAccessToken(object sender, AccessToken a)
        {
            if (sender == _login || sender == _newUser) // Made changes to login when user registers By : Poonam Dubey
            {
                this._accessToken = a;
                lblLoggedIn.Content = "Logged in as " + a.FirstName + " " + a.LastName;
                btnSignUp.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        /// <summary>
        /// ADDED by Trevor Glisch
        /// Method to change the label when someone logs in or checks messages
        /// </summary>
        /// <param name="a"></param>
        public void SetNewMessageLabel(AccessToken a)
        {
            try
            {
                int count = messageMgr.GetUnreadMessageCount(_accessToken.UserName);
                string message = "";
                if (count > 0)
                {
                    message = count.ToString();
                }
                else
                {
                    message = "No";
                }

                lblCurrentMessages.Content = a.FirstName + " You Have " + message + " New Messages";
            }
            catch (Exception)
            {
                lblCurrentMessages.Content = a.FirstName + " You Have No New Messages";
            }

        }
        /// <summary>
        /// Author: Ryan Taylor
        /// Click logic for New user button
        /// Date: 2/26/16
        /// </summary>
        public void NewUser_Click(object sender, RoutedEventArgs e)
        {
            // Made changes to login when user registers By : Poonam Dubey
            _newUser = new NewUserCreation();
            _newUser.AccessTokenCreatedEvent += setAccessToken;
            _newUser.ShowDialog();
            if (_accessToken != null)
            {
                this.btnLogin.Content = "Log Out";
                CheckPermissions();
            }
        }
        /// <summary>
        /// Author: Sara Nanke
        /// Click logic for collapse
        /// Date: 4/22/16
        /// </summary>
        private void collapse_Click(object sender, RoutedEventArgs e)
        {
            if (sidePanelDefinition.Width.Value > 0)
            {
                sidePanelDefinition.Width = new GridLength(0);
                btnCollapse.Content = "\u276F\u276F"; // ">>"
            }
            else
            {
                sidePanelDefinition.Width = new GridLength(220);
                btnCollapse.Content = "\u276E\u276E"; // "<<"
            }
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnHome
        /// Date: 3/6/16
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new HomeContent(_accessToken));
            clearSideBar();
            SetHomeButtons();
            clearUnusedSidebars();
            boldCurrent(sender);
        }
        // Chris S - Had to refactor - using in two places
        private void SetHomeButtons()
        {
            
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnGardens
        /// Date: 3/6/16
        /// </summary>
        private void btnGardens_click(object sender, RoutedEventArgs e)
        {
            //mainFrame.NavigationService.Navigate(new GardenPages.GardenMain(_accessToken));
            ////btnSideBar1.Content = "Create a Garden";
            //clearSideBar();
            //btnSideBar1.Content = "DO NOT USE";
            //btnSideBar2.Content = "DO NOT USE";
            //btnSideBar3.Content = "DO NOT USE";
            //btnSideBar4.Content = "Complete A Task";
            //btnSideBar5.Content = "Create a Task";
            //btnSideBar6.Content = "Sign Up for Task";

            //Role role = new Role();
            //role.RoleID = "Admin";
            //if (_accessToken.Roles.Contains(role))
            //{
            //    btnSideBar7.Content = "Manage Garden Group";
            //}
            //else
            //{
            //    btnSideBar7.Content = "";
            //}
            //btnSideBar7.Content = "Create Garden";
            //btnSideBar8.Content = "View Tasks By Garden";
            //btnSideBar10.Content = "View Groups";
            //btnSideBar11.Content = "Your Groups";
            //btnSideBar12.Content = "Request to be a Group Leader";
            //btnSideBar13.Content = "Aasign Task to a Member";
            //clearUnusedSidebars();
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnExpert
        /// Date: 3/6/16
        /// </summary>
        private void btnExpert_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new HomePages.ViewBlog(_accessToken));
            clearSideBar();
            btnSideBar1.Content = "Articles";
            btnSideBar2.Content = _accessToken != null ? "Ask a Question" : "";
            btnSideBar3.Content = "Search for Questions";
            btnSideBar4.Content = roleManager.ConfirmUserIsAssignedRole(_accessToken, "Expert") ? "Answer Questions" : "";
            btnSideBar5.Content = "View Recipes";
            btnSideBar6.Content = roleManager.ConfirmUserIsAssignedRole(_accessToken, "Expert") ? "Add a Recipe" : "";
            btnSideBar7.Content = "Plants";
            btnSideBar8.Content = roleManager.ConfirmUserIsAssignedRole(_accessToken, "Expert") ? "Upload Garden Template" : "";
            btnSideBar9.Content = "View Garden Templates";
            btnSideBar10.Content = "Upload Garden Blueprints";
            btnSideBar11.Content = "View Garden Blueprints";
            btnSideBar12.Content = roleManager.ConfirmUserIsAssignedRole(_accessToken, "Expert") ||
               roleManager.ConfirmUserIsAssignedRole(_accessToken, "Admin") ? "" : "Become an Expert";
            clearUnusedSidebars();
            boldCurrent(sender);
            //foreach (Role r in _accessToken.Roles)
            //{
            //    if (r.RoleID == "Expert")
            //    {
            //        var expertTabs = new Label[] { btnGardens, btnExpert, btnHome, btnProfile };
            //        visibleTabs.AddRange(expertTabs);
            //        break;
            //    }
            //    clearUnusedSidebars();
            //}
        }

        /// <summary>
        /// Author: Emily West
        /// Click logic for button btnVolunteer        /// 
        /// </summary>
        /// <remarks>
        /// Chris sheehan   
        /// removing volunteer tab, commenting it out at first
        /// 
        /// </remarks>
        //private void btnVolunteer_Click(object sender, RoutedEventArgs e)
        //{
        //    mainFrame.NavigationService.Navigate(new VolunteerPages.VolunteerHome(_accessToken));
        //    clearSideBar();
        //    btnSideBar1.Content = "Edit Volunteer Availability";
        //    btnSideBar2.Content = "Volunteer Sign Up";
        //    clearUnusedSidebars();

        //}

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnAdmin
        /// Date: 3/9/16
        /// </summary>
        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new AdminPages.AdminHome(_accessToken));
            clearSideBar();
            btnSideBar1.Content = "Users";
            btnSideBar2.Content = "Messages";
            btnSideBar3.Content = "Expert Requests";
            btnSideBar6.Content = "";
            clearUnusedSidebars();
            boldCurrent(sender);
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnProfile
        /// Date: 3/9/16
        /// </summary>
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new ProfilePages.ProfileMain(_accessToken));
            clearSideBar();
            SetProfileButtons();
            clearUnusedSidebars();
            boldCurrent(sender);
        }
        //Chris S - had to refactor - using in multiple places
        private void SetProfileButtons()
        {
            btnSideBar1.Content = "Profile Menu";
            btnSideBar2.Content = "Messages";
        }

        //Author: Sara Nanke
        public void clearSideBar()
        {
            //clear side panel content
            foreach (Label label in sidePanel.Children)
            {
                label.Content = "";
            }
        }

        public void boldCurrent(Object sender)
        {
            try
            {
                foreach (Label label in grdTabs.Children)
                {
                    label.FontWeight = FontWeights.Regular;
                }
                Label current = (Label)sender;
                current.FontWeight = FontWeights.Bold;

                //open sidebar
                sidePanelDefinition.Width = new GridLength(220);
                btnCollapse.Content = "\u276E\u276E"; // "<<"
            }
            catch (Exception) 
            { 
                //contine 
            }
        }

        /// <summary>
        /// Author: Sara Nanke
        /// Click logic for the btnsidebarclick event
        /// Date: 3/9/16
        /// Updated by many people
        /// Condensed by Sara Nanke
        /// Combined all sidebar clicks together in same method
        /// Date: 4/18/15
        /// 
        /// </summary>
        private void btnSideBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ColorConverter cc = new ColorConverter();
            foreach (Label label in sidePanel.Children)
            {
                label.FontWeight = FontWeights.Regular;
            }
            Label current = (Label)sender;
            current.FontWeight = FontWeights.Bold;
            String content = current.Content.ToString();
            Page page = new HomePages.ViewBlog();
            try
            {
                switch (content)
                {
                    case "Edit Personal Info":
                        page = new ProfilePages.EditPersonalInfo(_accessToken);
                        break;
                    case "Edit Volunteer Availability":
                        page = new VolunteerPages.EditVolunteerAvailability(_accessToken);
                        break;
                    case "Messages":
                        page = new ProfilePages.Messages(_accessToken);
                        break;
                    case "Add a Recipe":
                        page = new ExpertPages.RecipeInput(_accessToken);
                        break;
                    case "Volunteer Sign Up":
                        page = new VolunteerPages.VolunteerSignUp(_accessToken);
                        break;
                    case "Profile Menu":
                        page = new ProfilePages.ProfileMain(_accessToken);
                        break;
                    case "Expert Requests":
                        page = new AdminPages.AdminProcessExpertRequests(_accessToken);
                        break;
                    case "Search for Questions":
                        page = new ExpertPages.SearchForQuestions(_accessToken);
                        break;
                    case "Complete A Task":
                        page = new ProfilePages.Messages(_accessToken);
                        break;
                    case "Ask a Question":
                        page = new ExpertPages.ExpertAdvice(_accessToken);
                        break;
                    case "User Role":
                        page = new AdminPages.UserRole(_accessToken);
                        return;
                    case "Answer Questions":
                        page = new ExpertPages.ExpertAdviceRespond(_accessToken);
                        break;
                    case "Create a Task":
                        page = new GardenPages.ManageTask(_accessToken);
                        break;
                    case "User Region":
            //            page = new Uri("AdminPages/RegionPage.xaml", UriKind.Relative);
                        page = new AdminPages.RegionPage();
                        break;
                    case "Users":
                        page = new AdminPages.AdminProfile(_accessToken);
                        break;
                    case "Upload Garden Template":
                        page = new ExpertPages.UploadTemplates(_accessToken);
                        break;
                    case "Sign Up for Task":
                        page = new GardenPages.SelectTasks(_accessToken);
                        break;
                    case "View Garden Templates":
                        page = new ExpertPages.ViewTemplates(_accessToken);
                        break;
                    case "create garden":
                        page = new GardenPages.CreateGarden(_accessToken);
                        break;
                    case "View Tasks By Garden":
                        page = new GardenPages.ViewTasks(_accessToken);
                        break;
                    case "View Recipes":
                        page = new ExpertPages.ViewRecipe(_accessToken);
                        break;
                    case "Plants":
                        page = new ExpertPages.ViewPlants(_accessToken);
                        break;
                    case "view groups":
                        page = new GardenPages.ViewGroups(_accessToken);
                        break;
                    case "Your Groups":
                        page = new GardenPages.GroupMain(_accessToken);
                        break;
                    case "Request to be a Group Leader":
                        page = new GardenPages.RequestGroupLeader(_accessToken);
                        break;
                    case "Assign Task to a Member":
                        page = new GardenPages.AssignTask(_accessToken);
                        break;
                    case "Articles":
                        page = new HomePages.ViewBlog(_accessToken);
                        break;
                    case "Become an Expert":
                        page = new ExpertPages.RequestExpert(_accessToken);
                        break;   
                     case "Upload Garden Blueprints":
                        page = new ExpertPages.UploadBlueprint(_accessToken);
                        break;
                     case "View Garden Blueprints":
                        page = new ExpertPages.ViewBluePrints(_accessToken);
                        break;                          
                    default: //Blog
                        page = page = new HomePages.ViewBlog(_accessToken);
                        break;
                }
            }
            catch (Exception ex)
            {
                page = new HomePages.ViewBlog();
            }

            mainFrame.NavigationService.Navigate(page);
        }

        public void clearUnusedSidebars()
        {
            foreach (Label label in sidePanel.Children)
            {
                if (String.IsNullOrWhiteSpace(label.Content.ToString()))
                {
                    label.Visibility = Visibility.Collapsed;
                }
                else
                {
                    label.Visibility = Visibility.Visible;
                }
            }
        }

        private void lblCurrentMessages_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new ProfilePages.Messages(_accessToken));
        }
    }
}
