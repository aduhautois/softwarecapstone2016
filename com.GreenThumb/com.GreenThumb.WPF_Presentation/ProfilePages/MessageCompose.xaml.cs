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

namespace com.GreenThumb.WPF_Presentation.ProfilePages
{
    /// <summary>
    /// Ryan Taylor
    /// Created: 4/14/2016
    /// Interaction logic for MessageCompose.xaml
    /// </summary>
    public partial class MessageCompose : Page
    {
        private AccessToken _accessToken;
        private MessageManager _mgr = new MessageManager();
        private List<User> _userList;
        private User _user;
        private List<GroupMember> _Grpm;
        private bool _CCMe;
        private Group _grp;
        private bool _messageAll;
        public MessageCompose(AccessToken accessToken)
        {
            _accessToken = accessToken;
            InitializeComponent();
            FillUserList();
        }

        /// <summary>
        /// ADDED BY  Trevor Glisch
        /// Constructor for the Send all Messages to group members
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="MessageAll"></param>
        /// <param name="group"></param>
        public MessageCompose(AccessToken accessToken, bool MessageAll, List<GroupMember> grpm, Group grp)
        {

            _accessToken = accessToken;
            _Grpm = grpm;
            _messageAll = MessageAll;
            _grp = grp;
            InitializeComponent();
            FillUserList();
            txtTo.Visibility = Visibility.Hidden;
            lblTxT.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 4/14/2016
        /// 
        /// Create A message and send it to a user if username is found.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (_messageAll)
            {
                if ((bool)CCBox.IsChecked)
                {
                    foreach (var a in _Grpm)
                    {
                        SendMessagesAll(a);
                    }

                }
                else
                {
                    foreach (GroupMember a in _Grpm)
                    {
                        if (a.User.UserName != _accessToken.UserName)
                        {
                            SendMessagesAll(a);
                        }

                    }
                }

                MessageBox.Show("Messages Sent Successfully.");
                this.NavigationService.Navigate(new GardenPages.ManageGroup(_accessToken, _grp));
            }
            else if (_userList.Contains(_user))
            {
                Message msg = new Message()
                {
                    MessageSender = _accessToken.UserName,
                    MessageReceiver = _user.UserName,
                    MessageContent = this.txtContent.Text,
                    MessageSubject = this.txtSubject.Text
                };

                try
                {
                    if (_mgr.SendMessage(msg.MessageContent, msg.MessageSubject, msg.MessageSender, msg.MessageReceiver))
                    {
                        MessageBox.Show("Message Sent Successfully.");
                        this.NavigationService.Navigate(new Messages(_accessToken));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.txtTo.Focus();
                }

            }
            else
            {
                lblError.Content = "Not a valid username";
            }
        }

        /// <summary>
        /// Trevor Glisch
        /// Created 04-22-2016
        /// </summary>
        /// <param name="a"></param>
        private void SendMessagesAll(GroupMember a)
        {
            Message msg = new Message()
            {
                MessageSender = _accessToken.UserName,
                MessageReceiver = a.User.UserName,
                MessageContent = this.txtContent.Text,
                MessageSubject = this.txtSubject.Text
            };
            try
            {
                _mgr.SendMessage(msg.MessageContent, msg.MessageSubject, msg.MessageSender, msg.MessageReceiver);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 04/21/2016
        /// 
        /// Method to fill user list for the autocomplete box
        /// </summary>
        private void FillUserList()
        {
            try
            {
                _userList = _mgr.GetUserNames();
                txtTo.ItemsSource = _userList;
                txtTo.ToolTip = "Select a username";
            }
            catch (Exception)
            {
                txtTo.ToolTip = "No users added yet";
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 04/21/2016
        ///
        /// Method to cancel creation and return to the message area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_messageAll)
            {
                this.NavigationService.Navigate(new GardenPages.ManageGroup(_accessToken, _grp));
            }
            this.NavigationService.Navigate(new Messages(_accessToken));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 04/20/2016
        /// 
        /// Method to select the user based on current value in txtTo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblError.Content = "";
            try
            {
                _user = (User)txtTo.SelectedItem;
            }
            catch (Exception)
            {
                _user = null;
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 04/22/2016
        /// 
        /// This method checks to see if the value in the txtTo box matches a known user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTo_LostFocus(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            if (!_userList.Contains(_user))
            {
                lblError.Content = "Not a recognized member";
            }
        }

    }
}
