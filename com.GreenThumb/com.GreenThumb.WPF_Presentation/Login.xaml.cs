/// <summary>
/// Ryan Taylor
/// Created: 2016/03/01
/// Interaction logic for Login.xaml
/// </summary>

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
using System.Windows.Shapes;
using com.GreenThumb.BusinessLogic.Interfaces;

namespace com.GreenThumb.WPF_Presentation
{
    public partial class Login : Window
    {
        static AccessToken _accessToken;
        private ISecurityManager _security = new SecurityManager();
        public Login()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _accessToken = null;
            this.DialogResult = false;
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        public void Submit()
        {
            string username = this.txtUsername.Text;
            string password = this.txtPassword.Password;

            try
            {
                _accessToken = _security.ValidateExistingUser(username, password);
                this.DialogResult = true;
            }
            catch (Exception ex) // login failed
            {
                MessageBox.Show("Login failed:\n" + ex.Message);
                this.txtPassword.Clear();
                this.txtUsername.Focus();
                this.txtUsername.SelectionStart = 0;
                this.txtUsername.SelectionLength = this.txtUsername.Text.Length;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtUsername.Focus();
        }

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
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Submit();
            }
        }
    }
}
