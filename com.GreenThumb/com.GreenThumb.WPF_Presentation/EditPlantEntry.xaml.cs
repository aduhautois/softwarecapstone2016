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
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for EditPlantEntry.xaml
    /// //Created by Stenner Kvindlog 
    /// // 3/4/16
    /// </summary>
    public partial class EditPlantEntry : Window
    {
        // form to edit existing plant 
        PlantManager myPlantManager = new PlantManager();
        Plant oldPlant = new Plant();
        AccessToken user = new AccessToken();

        //check if the text feilds are validated to enable save button 
        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.name.Text != null && this.plantId.Text != null && this.type.Text != null && this.description.Text != null && this.category.Text != null)
            {
                this.save.IsEnabled = true;
            }
        } 

        public EditPlantEntry(int plantId, AccessToken ax)
        {
            InitializeComponent();
            fetchPlantDetails(plantId);
            user = ax;
        }

        // pull plant from the database 
        public void fetchPlantDetails(int plantId)
        {
            try
            {
                var myPlant = myPlantManager.GetPlant(plantId);
                oldPlant = myPlant;

                this.plantId.Text = myPlant.PlantID.ToString();
                this.name.Text = myPlant.Name;
                this.type.Text = myPlant.Type;
                this.category.Text = myPlant.Category;
                this.season.Text = myPlant.Season;
                this.description.Text = myPlant.Description;

            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }
        }


        public bool saveDetails()
        {

            //validate feilds  
            bool flag = false;

            try
            {
                Plant newPlant = new Plant();

                newPlant.PlantID = int.Parse(this.plantId.Text);
                newPlant.Name = this.name.Text;
                newPlant.Type = this.type.Text;
                newPlant.Category = this.category.Text;
                newPlant.Season = this.season.Text;
                newPlant.Description = this.description.Text;
                newPlant.ModifiedDate = DateTime.Now;
                newPlant.ModifiedBy = user.UserID;


                flag = myPlantManager.EditPlant(newPlant , oldPlant);

            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }

            return flag;
        }

        private void Button_Click(object sender, RoutedEventArgs e, bool myBool)
        {       
            //save the data back to the database 
            saveDetails();
            if (myBool == true)
            {
                MessageBox.Show("Your Record Has Been Edited");
            }
            else if (myBool == false)
            {
                MessageBox.Show("Your Record Has Not Been Edited");
            }
            this.Close(); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //exit without making changes to data
            this.Close(); 
        }

        private void plantId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (plantId.Text == null)
            {
                plantId.BorderBrush = new SolidColorBrush(Colors.Red);

            }
            else
            {
                plantId.BorderBrush = new SolidColorBrush(Colors.Green);

            }
        }

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
    }
}
