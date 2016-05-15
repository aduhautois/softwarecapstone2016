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

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for Layout.xaml
    /// </summary>
    public partial class Layout : Window
    {
        private bool isCollapsed = false;
        public Layout()
        {
            InitializeComponent();
        }

        private void btnCollapseMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isCollapsed)
            {
                cdMenuPanel.Width = new GridLength(250);
                cdMenuPanel.MinWidth = 200;
                btnCollapseMenu.Content = " << ";
                isCollapsed = false;
            }
            else
            {
                cdMenuPanel.Width = new GridLength(0);
                cdMenuPanel.MinWidth = 0;
                btnCollapseMenu.Content = " >> ";
                isCollapsed = true;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login _login = new Login();
            _login.ShowDialog();
        }

        
    }
}
