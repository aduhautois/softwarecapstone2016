using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
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

namespace com.GreenThumb.WPF_Presentation.HomePages
{
    /// <summary>
    /// /// Added by Sara Nanke on 03/22/2016
    /// Interaction logic for CreateBlogPage.xaml
    /// </summary>
    public partial class CreateBlog : Page
    {
        AccessToken accessToken;
        BlogManager blogManager = new BlogManager();

        public CreateBlog()
        {
            InitializeComponent();
        }
        public CreateBlog(AccessToken accessToken)
        {
            InitializeComponent();
            this.accessToken = accessToken;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            String message = "Sorry, we could not add the entry to our system.";
            bool valid = true;
            try
            {
                Blog blog = new Blog()
                {
                    BlogTitle = txtTitle.Text,
                    BlogData = txtContent.Text,
                    CreatedBy = accessToken.UserID,
                    DateCreated = DateTime.Now,
                    Active = true
                };

                if (blog.BlogTitle == "")
                {
                    valid = false;
                    message += "\nYou must enter a title";
                }
                if (blog.BlogData == "")
                {
                    valid = false;
                    message = "\nYou must enter some content";
                }
                if (valid)
                {
                    blogManager.AddBlog(blog);
                    MessageBox.Show("Thank you for submitting your blog!");
                    if (accessToken != null)
                    {
                        this.NavigationService.Navigate(new HomePages.ViewBlog(accessToken));
                    }
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePages.ViewBlog(accessToken));
        }
    }
}


