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
using System.IO;
using System.Diagnostics;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;


namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// stenner kvindlog
    /// Interaction logic for ViewTemplates.xaml
    /// </summary>
    public partial class ViewTemplates : Page
    { 
        
        List<Template> TemplateList;
        AccessToken _accessToken;
        TemplateManager myTemplateManager = new TemplateManager();

        public ViewTemplates(AccessToken _AccessToken)
        {
            InitializeComponent();
            _accessToken = _AccessToken;
            populateTemplatesList();
       }

        public void getTemplates()
        {   // retrive all templates from database                                        
            TemplateList = myTemplateManager.retriveAllTemplate();
        }


        public void populateTemplatesList()
        {
            try
            {                
                    getTemplates();
                    lvTemplates.Items.Clear();
                    this.lvTemplates.ItemsSource = TemplateList;      
            }
            catch (Exception)
            {
                MessageBox.Show("No Files Have Been Uploaded");
            }         
        }

        // open file with default program 
        private void templates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Template myTemplate = new Template();
            myTemplate = (Template)lvTemplates.SelectedItem;  
          
            // open file with default program 
            System.Diagnostics.Process.Start(myTemplate.FilePath);
        }

    }
}
