using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
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
    /// Interaction logic for ViewGardenTemplate.xaml
    /// </summary>
    public partial class ViewGardenTemplate : Page
    {
        private GardenTemplateManager manager = new GardenTemplateManager();
        private List<GardenTemplate> templateList;
        private string selectedTemplate;

        // page variables
        PageDetails pageDetails = new PageDetails();
        Paginate<GardenTemplate> paginate = new Paginate<GardenTemplate>();
        List<GardenTemplate> fullList = new List<GardenTemplate>();
        public ViewGardenTemplate()
        {
            templateList = manager.GetTemplateList();
            InitializeComponent();

            fullList = templateList;
            pageDetails = InitializePageDetails();
            icTemplates.ItemsSource = templateList;

        }

        private void DisplayImage()
        {
            if (selectedTemplate != "" && selectedTemplate != null)
            {
                try
                {
                    var data = manager.AddLoadTemplate(selectedTemplate);
                    var stream = new MemoryStream(data);

                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    ImgTemplate.Source = bitmap;
                }
                catch (Exception)
                {
                    lblError.Content = "Error loading image.";
                }
            }
        }

        /* Rhett Allen - adding pages */
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (pageDetails.CurrentPage > 1)
            {
                pageDetails.CurrentPage--;
            }

            SelectTemplates();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (pageDetails.CurrentPage < pageDetails.MaxPages)
            {
                pageDetails.CurrentPage++;
            }

            SelectTemplates();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            pageDetails.CurrentPage = 1;
            SelectTemplates();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            pageDetails.CurrentPage = pageDetails.MaxPages;
            SelectTemplates();
        }

        private void SelectTemplates()
        {
            try
            {
                icTemplates.ItemsSource = paginate.GetList(pageDetails, fullList);
                lblPage.Content = "Page " + pageDetails.CurrentPage;
            }
            catch (Exception)
            {
                icTemplates.ItemsSource = new List<GardenTemplate>();
            }
        }

        private PageDetails InitializePageDetails()
        {
            PageDetails p = new PageDetails();
            p.Count = manager.GetTemplateList().Count;
            p.CurrentPage = 1;
            p.PerPage = 5;

            return p;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            selectedTemplate = (((Button)sender).Tag as GardenTemplate).ToString();
            DisplayImage();
        }
    }
}
