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
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.WPF_Presentation
{
    ///<author>
    ///Sara Nanke
    /// </author>
    /// <summary>
    /// Interaction logic for HomeContent.xaml
    /// Creaged 5/5/2016
    /// </summary>
    public partial class HomeContent : Page
    {
        private AccessToken accessToken = null;

        public HomeContent(AccessToken _accessToken)
        {
            InitializeComponent();
            checkAccessToken(_accessToken);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.Login_Click(sender, e);
            checkAccessToken(main.LoggedAccessToken);
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.NewUser_Click(sender, e);
            checkAccessToken(main.LoggedAccessToken);
        }

        private void checkAccessToken(AccessToken _accessToken)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            if (_accessToken != null)
            {
                grdLogin.Visibility = Visibility.Hidden;
                this.accessToken = _accessToken;
            }
            else
            {
                grdLogin.Visibility = Visibility.Visible;
            }
        }
    }
}
