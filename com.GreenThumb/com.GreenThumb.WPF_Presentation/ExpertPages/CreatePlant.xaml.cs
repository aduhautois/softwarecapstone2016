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
    /// Interaction logic for AddPlant.xaml
    /// 
    /// //Created by Stenner Kvindlog 
    /// //3/4/16
    /// 
    /// Motified by Sara Nanke
    /// 3/31/16
    /// --set view to grid
    /// --added title
    /// --removed plantId (should not be entered for create)
    /// --changed season and category to dropdown
    /// --added photo upload and save
    /// </summary>
    public partial class CreatePlant : Page
    {
        AccessToken user = new AccessToken();
        Plant newPlant = new Plant();
        bool?[] regions = new bool?[10];

        public CreatePlant()
        {
            InitializeComponent();
        }
        public CreatePlant(AccessToken ax)
        {
            InitializeComponent();
            for (int i = 0; i < regions.Length - 1; i++)
            {
                regions[i] = false;
            }
            user = ax;
        }

        PlantManager myPlantManager = new PlantManager();

        //check if the text feilds are validated to enable save button 
        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.name.Text != null && this.type.Text != null && this.description.Text != null && this.category.SelectedValue != null)
            {
                this.save.IsEnabled = true;
            }
        }

        public bool saveDetails()
        {
            //validate feilds  implement createdBy user
            bool myBool = false;
            int myInt = 0;
            try
            {

                newPlant.PlantID = null; //int.Parse(this.plantId.Text);
                newPlant.Name = this.name.Text;
                newPlant.Type = this.type.Text;
                newPlant.Category = this.category.Text;
                newPlant.Season = this.season.Text;
                newPlant.Description = this.description.Text;
                newPlant.CreatedDate = DateTime.Now;
                newPlant.CreatedBy = user.UserID;

                myInt = myPlantManager.AddPlant(newPlant);
                if (myInt > 100)
                {
                    newPlant.PlantID = myInt;
                    myBool = myPlantManager.AddPlantRegions(newPlant, regions);
                }
            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }

            return myBool;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //save the data back to the database 
            bool myBool = saveDetails();
            if (myBool == true)
            {
                MessageBox.Show("Your Record Has Been Created");
                //save the data back to the database 
                try
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    String fileName = "plant" + newPlant.Name;
                    String photolocation = "../../Images/Plants/" + fileName + ".jpg";  //file name 

                    encoder.Frames.Add(BitmapFrame.Create((BitmapImage)imgPreview.Source));

                    using (var filestream = new FileStream(photolocation, FileMode.Create))
                        encoder.Save(filestream);
                }
                catch (Exception ex) { }
            }
            else if (myBool == false)
            {
                MessageBox.Show("Your Record Has Not Been Created");
            }
            this.NavigationService.Navigate(new ExpertPages.ViewPlants(user));
        }


        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            //exit without making changes to data
            this.NavigationService.Navigate(new ExpertPages.ViewPlants(user));
        }

        //private void plantId_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (plantId.Text == null)
        //    {
        //        plantId.BorderBrush = new SolidColorBrush(Colors.Red);
        //    }
        //    else
        //    {
        //        plantId.BorderBrush = new SolidColorBrush(Colors.Green);
        //    }
        //}

        private void name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (name.Text == null)
            {
                name.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                name.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }

        private void type_LostFocus(object sender, RoutedEventArgs e)
        {

            if (type.Text == null)
            {
                name.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                type.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }

        private void description_LostFocus(object sender, RoutedEventArgs e)
        {

            if (description.Text == null)
            {
                description.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                description.BorderBrush = new SolidColorBrush(Colors.Green);
            }

        }

        private void category_LostFocus(object sender, RoutedEventArgs e)
        {

            if (category.Text == null)
            {
                category.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                category.BorderBrush = new SolidColorBrush(Colors.Green);
            }

        }

        private void btnUpLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                imgPreview.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void region_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox box = (CheckBox)sender;
                int regionNumber = Int32.Parse(box.Content.ToString()) - 1;

                regions[regionNumber] = box.IsChecked;
            }
            catch (Exception) { }
        }

    }
}
