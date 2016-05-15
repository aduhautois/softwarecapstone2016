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
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for AddPlant.xaml
    /// //Created by Stenner Kvindlog 
    /// //3/4/16
    /// </summary>
    public partial class CreatePlant : Window
    {
        AccessToken user = new AccessToken();
        public CreatePlant(AccessToken ax)
        {
            InitializeComponent();

            user = ax;
        }

        PlantManager myPlantManager = new PlantManager();

        //check if the text feilds are validated to enable save button 
        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.name.Text != null && this.plantId.Text != null && this.type.Text != null && this.description.Text != null && this.category.Text != null)
            {
                this.save.IsEnabled = true;
            }
        }

        public int saveDetails()
        {
            //validate feilds  implement createdBy user
            //bool myBool = false;
            int myInt = 0;

            try
            {
                Plant newPlant = new Plant();

                newPlant.PlantID = int.Parse(this.plantId.Text);
                newPlant.Name = this.name.Text;
                newPlant.Type = this.type.Text;
                newPlant.Category = this.category.Text;
                newPlant.Season = this.season.Text;
                newPlant.Description = this.description.Text;
                newPlant.CreatedDate = DateTime.Now;
                newPlant.CreatedBy = user.UserID;

                myInt = myPlantManager.AddPlant(newPlant);

            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }

            return myInt;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //save the data back to the database 
            int myInt = saveDetails();
            if (myInt > 0)
            {
                MessageBox.Show("Your Record Has Been Created");
            }
            else
            {
                MessageBox.Show("Your Record Has Not Been Created");
            }
            this.Close();
        }


        private void cancel_Click(object sender, RoutedEventArgs e)
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
