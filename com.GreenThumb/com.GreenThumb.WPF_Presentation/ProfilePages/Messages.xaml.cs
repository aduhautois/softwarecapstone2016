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
    /// Interaction logic for Messages.xaml
    /// </summary>
    public partial class Messages : Page
    {
        private AccessToken _at;
        private List<Message> _messages;
        private MessageManager _mgr = new MessageManager();
        private Message _selectedMessage;


        public Messages(AccessToken at)
        {
            if (at != null)
            {
                _at = at;
                InitializeComponent();
                PopulateMessageList();
            }

        }

        private void btn_GetMessages_Click(object sender, RoutedEventArgs e)
        {
            PopulateMessageList();
        }

        private void btn_DeleteMessages_Click(object sender, RoutedEventArgs e)
        {
            bool result;
            try
            {
                if (dataInbox.SelectedIndex >= 0)
                {
                    _selectedMessage = (Message)dataInbox.SelectedItem;
                    result = _mgr.EditMessageDeletedReceiver(_at.UserName, _selectedMessage.MessageID);
                    PopulateMessageList();

                }
                else if (dataOutbox.SelectedIndex >= 0)
                {
                    _selectedMessage = (Message)dataOutbox.SelectedItem;
                    result = _mgr.EditMessageDeletedSender(_at.UserName, _selectedMessage.MessageID);
                    PopulateMessageList();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataOutbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataOutbox.SelectedIndex >= 0)
            {
                _selectedMessage = (Message)dataOutbox.SelectedItem;
                txtMessage.Text = _selectedMessage.MessageContent;
            }
            else
            {
                txtMessage.Text = "Select A Message";
            }
        }


        public void dataInbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataInbox.SelectedIndex >= 0)
            {
                _selectedMessage = (Message)dataInbox.SelectedItem;
                try
                {
                    _mgr.EditMessageRead(_at.UserName, _selectedMessage.MessageID);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                txtMessage.Text = _selectedMessage.MessageContent;

            }
            else
            {
                txtMessage.Text = "Select A Message";
            }
        }

        private void PopulateMessageList()
        {
            try
            {
                _messages = _mgr.GetUserMessages(_at.UserName);
                var inbox = _messages.Where(s => s.MessageReceiver.ToUpper() == _at.UserName.ToUpper() && s.RecieverDeleted == false)
                                .OrderByDescending(s => s.MessageDate);
                var outbox = _messages.Where(s => s.MessageSender.ToUpper() == _at.UserName.ToUpper() && s.SenderDeleted == false)
                                .OrderByDescending(s => s.MessageDate);
                dataInbox.ItemsSource = inbox.ToList();
                dataOutbox.ItemsSource = outbox.ToList();
            }
            catch (Exception ex)
            {
                dataInbox.ItemsSource = null;
                dataOutbox.ItemsSource = null;

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_ComposeMessage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProfilePages.MessageCompose(_at));
        }


    }
}
