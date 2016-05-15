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

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for NewUserInformation.xaml
    /// </summary>
    public partial class NewUserInformation : Window
    {
        //static AccessToken _accessToken;
        public NewUserInformation()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string firstName = this.txtbxFirstName.Text;
            string lastName = this.txtbxLastName.Text;
            string zip = this.txtbxZip.Text;
            string email = this.txtbxEmail.Text;


            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
