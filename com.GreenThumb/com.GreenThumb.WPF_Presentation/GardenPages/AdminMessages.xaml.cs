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


namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for AdminMessages.xaml
    /// </summary>
    public partial class AdminMessages : Page
    {
        //private Message message = new Message();
        //private static MessageManager messageManager = new MessageManager();
        ////private List<Message> messages = messageManager.GetUserMessages();
        
        //public AdminMessages(AccessToken _assessToken)
        //{
        //    InitializeComponent();
        //    if(_assessToken != null)
        //    {

        //    }
        //    else
        //    { }
        //}

        ///// <summary>
        ///// This method will populate the list of emails
        ///// </summary>
        //public void bindListView()
        //{
        //    lvEmails.ItemsSource = messages;
        //}

        ///// <summary>
        ///// Binds the Message content
        ///// </summary>
        //public void BindLabel(int index)
        //{
        //    lblEmailContent.Content = "";

        //    if (messages.Count > 0)
        //    {
        //        Message message = (Message) lvEmails.Items[index];
        //        lblEmailContent.Content += message.MessageDate+ "\n\n";
        //        lblEmailContent.Content += message.MessageContent;
        //    }
        //}

        //private void lvEmails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    BindLabel(lvEmails.SelectedIndex);
        //}


        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    bindListView();
        //}

        //private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void lvEmails_Loaded(object sender, RoutedEventArgs e)
        //{
        //    bindListView();
        //}
    }
}

