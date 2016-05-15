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
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for CreateOrganization.xaml
    /// </summary>
    public partial class CreateOrganization : Page
    {
        private AccessToken _accessToken;
        private CreateOrgManager myCreateOrgManager = new CreateOrgManager();
        public CreateOrganization(AccessToken _accessToken)
        {
            InitializeComponent();
            this._accessToken = _accessToken;
        }
        private void btnAddOrganization_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                myCreateOrgManager.AddNewOrganization(_accessToken.UserID, txtOrganizationName.Text, txtLocalPhone.Text);

                MessageBox.Show("Your Oragnization is created!");
                txtOrganizationName.Clear();
                txtLocalPhone.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
