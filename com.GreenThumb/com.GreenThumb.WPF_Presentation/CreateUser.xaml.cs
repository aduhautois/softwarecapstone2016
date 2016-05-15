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
    /// Interaction logic for CreateUser.xaml
    /// //Created by Stenner Kvindlog 
    /// //3/4/16
    /// </summary>
    public partial class CreateUser : Window
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        UserManager myUserManager = new UserManager();
        //check if the text feilds are validated to enable save button 
        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.firstName.Text != null && this.lastName.Text != null && this.userName.Text != null &&
                this.password.Text != null && this.lastName.Text != null && isValidEmail(email.Text))
            {
                this.save.IsEnabled = true;
            }
        }

        public int saveDetails()
        {
            int flag = 0;

            //verify username is not in use 
            UserManager _myUserManager = new UserManager();

            try
            {
                User myUser = _myUserManager.GetUserByUserName(this.userName.Text);
                MessageBox.Show("Username Already in use.");
            }
            catch (Exception)
            {
                try
                {
                    User newUser = new User();

                    newUser.FirstName = this.firstName.Text;
                    newUser.LastName = this.lastName.Text;
                    newUser.UserName = this.userName.Text;
                    newUser.Password = this.password.Text;

                    newUser.EmailAddress = this.email.Text;
                    newUser.RegionId = Int32.Parse(this.regionId.Text);
                    newUser.Zip = this.zip.Text;


                    if (this.active.IsChecked == true)
                    {
                        newUser.Active = true;
                    }
                    else
                    {
                        newUser.Active = false;
                    }

                    flag = myUserManager.AddUser(newUser);

                }
                catch (Exception ax)
                {
                    MessageBox.Show(ax.Message);
                }
     
            }

            return flag;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //save the data back to the database 
            int flag = saveDetails();
            if (flag == 0)
            {
                MessageBox.Show("Your Record Has Been Created");
            }
            else if (flag == 1)
            {
                MessageBox.Show("Your Record Has Not Been Created");
            }
            this.Close(); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //exit without making changes to data
            this.Close(); 
        }


        private void firstName_LostFocus(object sender, RoutedEventArgs e)
        {

            if (firstName.Text == null)
            {
                firstName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                firstName.BorderBrush = new SolidColorBrush(Colors.Green);
            }

        }

        private void lastName_LostFocus(object sender, RoutedEventArgs e)
        {

            if (lastName.Text == null)
            {
                lastName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                lastName.BorderBrush = new SolidColorBrush(Colors.Green);
            }

        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {

            if (userName.Text == null)
            {
                userName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                userName.BorderBrush = new SolidColorBrush(Colors.Green);
            }

        }
        // make template to validate password 1 uppercase 1 lowercase 1 number at least 7 chars       
        private void password_LostFocus(object sender, RoutedEventArgs e) {

            if (password.Text.Length < 5 || password.Text.Length > 12)
            {
                password.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                password.BorderBrush = new SolidColorBrush(Colors.Green);
            }

        }

        private void email_LostFocus(object sender, RoutedEventArgs e)
        {

            if (email.Text == null)
            {
                
            }
            else
            {
                if (isValidEmail(email.Text))
                {
                    email.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    email.BorderBrush = new SolidColorBrush(Colors.Green);
                }
            }

        }

        private bool isValidEmail(string email)
        {
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
