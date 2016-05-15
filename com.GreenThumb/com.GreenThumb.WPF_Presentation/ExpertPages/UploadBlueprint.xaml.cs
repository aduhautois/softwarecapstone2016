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
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Stenner Kvindlog
    /// Interaction logic for UploadBlueprint.xaml
    /// Form to upload blueprint to folder in project 
    /// </summary>
    public partial class UploadBlueprint : Page
    {

        string destFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "../../" + @"Blueprints";         
        string sourceFilePath;
        string title;
        string description;
        string fileName;
        int modifiedBy;
        DateTime dateCreated;
        AccessToken _accessToken;
        public UploadBlueprint(AccessToken _AccessToken)
        {
             InitializeComponent();
            _accessToken = _AccessToken;     
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            //Open file dialog Window to select the file 
            if (fileDialog.ShowDialog() == true)
            {
                new FileInfo(fileDialog.FileName);
               
                sourceFilePath = fileDialog.FileName;
                fileName = System.IO.Path.GetFileName(sourceFilePath);
            }

            this.txtFileName.Text = sourceFilePath;      
        }

        private void uploadFile(object sender, RoutedEventArgs e)
        {
            // move file to folder in project 
            // save meta data to database ,  Title, description, user , date, file path. 
            description = this.txtDescription.Text;
            title = this.txtTitle.Text;
            dateCreated = DateTime.Now;
            modifiedBy = _accessToken.UserID;
 
            try
            {
                System.IO.File.Copy(sourceFilePath, destFilePath + @"\" + fileName, true);
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Has Occured");
            }

            destFilePath = destFilePath + @"\"+ fileName;

            Blueprint thisBlueprint = new Blueprint(title, description, dateCreated, modifiedBy, destFilePath );

            //send thisBlueprint to database 
            BlueprintManager myBlueprintManager = new BlueprintManager();

            try
            {
                myBlueprintManager.uploadBlueprint(thisBlueprint);
                MessageBox.Show("File Has Been Uploaded");
            }
            catch (Exception )
            {
                MessageBox.Show("An Error Has Occured");
            }           
        }
    }
}
