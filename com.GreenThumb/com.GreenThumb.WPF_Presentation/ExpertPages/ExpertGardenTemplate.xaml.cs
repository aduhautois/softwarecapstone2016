using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Nicholas King
    /// Interaction logic for ExpertGardenTemplate.xaml
    /// </summary>
    public partial class ExpertGardenTemplate : Page
    {
        private AccessToken at;
        private GardenTemplateManager manager;
        private string _templatePath = null;

        public ExpertGardenTemplate(AccessToken accessToken)
        {
            manager = new GardenTemplateManager();
            this.at = accessToken;
            InitializeComponent();
        }

        

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool saveSuccessfull = false;
            if (txtFileName.Text != null || txtFileName.Text != "" && _templatePath != null)
            {
                saveSuccessfull = manager.AddTemplate(_templatePath, at, txtFileName.Text.ToString());
            }


            if (saveSuccessfull == true)
            {
                lblError.Content = "Picture have been saved successfully.";
            }
            else
            {
                lblError.Content = "Picture failed to save.";
            }
        }

        private void btnSelectTemplate_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "png files (*.png)|*.png";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                _templatePath = ofd.FileName.ToString();

                ImageSource imageSrc = new BitmapImage(new Uri(_templatePath));

                imgTemplate.Source = imageSrc;
            }
        }


    }
}
