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
    /// Interaction logic for EditUserEntry.xaml
    /// //Created by Stenner Kvindlog 
    /// </summary>
    public partial class EditUserEntry : Window
    {
        //form to edit existing user 
        UserManager myUserManager = new UserManager();
        User oldUser = new User();
        

        //check if the text feilds are validated to enable save button 
        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.firstName.Text != null && this.lastName.Text != null && this.userName.Text != null && 
                this.password.Text != null && this.lastName.Text != null && isValidEmail(email.Text))
            {
                this.save.IsEnabled = true;
            }
        }

        public EditUserEntry(int userId)
        {
            InitializeComponent();          
            //take user id and call method to get user, put data in feilds to be edited
            fetchUserDetails(userId);
        }

		// fetch details or single user on userID
        public void fetchUserDetails(int userId)
        {
            try
            {             
                var myUser = myUserManager.GetUser(userId);
                oldUser = myUser;
                this.firstName.Text = myUser.FirstName;
                this.lastName.Text = myUser.LastName;
                this.userName.Text = myUser.UserName;
                this.password.Text = myUser.Password;
                this.email.Text = myUser.EmailAddress;
                this.regionId.Text = myUser.RegionId.ToString();
                this.zip.Text = myUser.Zip.ToString();
               
                if (myUser.Active == true)
                {
                    this.active.IsChecked = true;
                }
                else
                {
                    this.active.IsChecked = false;
                }

            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }
        }

		// save edited user to database
        public bool saveDetails()
        {
            //validate all feilds are filled out except email , regionid and zip 
            bool myBool = false;

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

                myBool = myUserManager.EditUser(newUser, oldUser);
            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }
            return myBool;
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

        private void userName_LostFocus(object sender, RoutedEventArgs e)// make validation to make sure username is not taken 
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
        private void password_LostFocus(object sender, RoutedEventArgs e){
            if (password.Text == null)
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
