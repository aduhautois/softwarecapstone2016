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



namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Created by : Poonam Dubey
    /// Interaction logic for NewUserCreation.xaml
    /// </summary>
    public partial class NewUserCreation : Window
    {
        private UserManager _userManagerObj = new UserManager();
        static AccessToken _accessToken;
        private ISecurityManager _security = new SecurityManager();
       
        private bool FirstRun;
        public NewUserCreation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is for the first time that the program is run
        /// the boolean value is used to tell the window that this account
        /// should be created as an admin because it is the INITIAL account
        /// Created with the software
        /// </summary>
        /// <param name="FirstRun"></param>
        public NewUserCreation(bool FirstRun)
        {
            MessageBox.Show("Please Create the Admin account for this computer! The user you are creating will be the firs Admin for this application!");
            this.FirstRun = FirstRun;
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string fName = this.txtFName.Text;
            string lName = this.txtLName.Text;
            string username = this.txtnewUsername.Text;
            string password = this.txtnewPassword.Password;
            string passConfirm = this.txtPassConfirm.Password;
            bool isActive = true;
            bool regexFnameFailed = false;
            bool regexLnameFailed = false;
            bool regexPasswordFailed = false;
            try
            {
                if (!string.IsNullOrEmpty(fName) && !string.IsNullOrEmpty(lName) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(passConfirm))
                {
                    if (Regex.IsMatch(fName, @"(?i)^[a-z]+"))
                    {
                        regexFnameFailed = false;
                    }
                    else
                    {
                        regexFnameFailed = true;
                    }

                    if (Regex.IsMatch(lName, @"(?i)^[a-z]+"))
                    {
                        regexLnameFailed = false;
                    }
                    else
                    {
                        regexLnameFailed = true;
                    }

                    if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$"))
                    {
                        regexPasswordFailed = false;
                    }
                    else
                    {
                        regexPasswordFailed = true;
                    }

                    if (!regexFnameFailed && !regexLnameFailed && !regexPasswordFailed)
                    {
                        if (password != passConfirm)
                            MessageBox.Show("Passwords dont match!");
                        else
                        {
                            if (username.Length < 5)
                            {
                                MessageBox.Show("Username should be atleast 5 characters long");
                            }
                            else
                            {
                                /// Added by Trevor Glisch for first Run
                                if (FirstRun)
                                {
                                    if (_userManagerObj.AddNewUser(fName, lName, string.Empty, string.Empty, username, password.HashSha256(), isActive, null) == 1)
                                    {
                                        ClearControls();
                                        MessageBoxResult result = MessageBox.Show("User has been created successfully!!", "User Created", MessageBoxButton.OK);
                                        if (result == MessageBoxResult.OK)
                                        {
                                            _accessToken = _security.ValidateExistingUser(username, password);
                                           
                                        }
                                        UserRoleManager urm = new UserRoleManager();

                                        if (urm.AddNewUserRole(_accessToken.UserID, "Admin"))
                                        {
                                            this.DialogResult = true;

                                        }
                                        else
                                        {
                                            MessageBox.Show("Failed to Add User as Admin!");
                                        }

                                        

                                    }
                                    else
                                    {
                                        txtnewUsername.Text = string.Empty;
                                        MessageBox.Show("Username entered already exists. Please try a different username.");

                                    }




                                    
                                    
                                    
                                }
                                else
                                {
                                    if (_userManagerObj.AddNewUser(fName, lName, string.Empty, string.Empty, username, password.HashSha256(), isActive, null) == 1)
                                    {
                                        ClearControls();
                                        MessageBoxResult result = MessageBox.Show("User has been created successfully!!", "User Created", MessageBoxButton.OK);
                                        if (result == MessageBoxResult.OK)
                                        {
                                            _accessToken = _security.ValidateExistingUser(username, password);
                                            this.DialogResult = true;
                                        }
                                    }
                                    else
                                    {
                                        txtnewUsername.Text = string.Empty;
                                        MessageBox.Show("Username entered already exists. Please try a different username.");

                                    }
                                }

                               
                            }
                        }
                    }
                    else
                    {
                        if (regexFnameFailed)
                            MessageBox.Show("Please enter only characters in first name");
                        else if (regexLnameFailed)
                            MessageBox.Show("Please enter only characters in last name");
                        else
                            MessageBox.Show("Password should contain 1 uppercase, 1 lowercase, 1 digit and a special character and should be minimum 6 characters long.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter all the fields");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            ClearControls();
            this.Close();
        }

        private void ClearControls()
        {
            this.txtFName.Text = string.Empty;
            this.txtLName.Text = string.Empty;
            this.txtnewUsername.Text = string.Empty;
            this.txtnewPassword.Password = string.Empty;
            this.txtPassConfirm.Password = string.Empty;
        }
        // Made changes to login when user registers By : Poonam Dubey
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_accessToken != null)  // don't raise the event if no one logged in
            {
                RaiseAccessTokenCreatedEvent();
            }
        }
        // Declare the delegate that will be the prototype of event subscribers
        public delegate void AccessTokenCreatedEventHandler(object sender, AccessToken a);

        // Declare the event from a delegate, so it knows what sort of subscribers to accept
        public event AccessTokenCreatedEventHandler AccessTokenCreatedEvent;
        protected virtual void RaiseAccessTokenCreatedEvent()  // we need a method to raise the event
        {
            // Raise the event
            if (AccessTokenCreatedEvent != null)  // if there are subscribers/listeners/handlers
                AccessTokenCreatedEvent(this, _accessToken); // go ahead and raise the event
        }
    }
}
