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
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Rhett Allen
    /// Created Date: 3/23/16
    /// 
    /// Interaction logic for ExpertAdvice.xaml
    /// </summary>
    public partial class ExpertAdvice : Page
    {
        private AccessToken _accessToken = null;
        private QuestionManager questionManager = new QuestionManager();
        private AdminExpertRequestsManager expertRequestManager = null;
        MessageManager message = new MessageManager();
        private IList<User> _experts
            = null;
        public ExpertAdvice(AccessToken accessToken)
        {
            _accessToken = accessToken;
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string category = txtCategory.Text;
            string content = txtContent.Text;

            Question question = new Question();
            bool errors = false;

            if(title == null || title == "")
            {
                lblErrorTitle.Visibility = System.Windows.Visibility.Visible;
                lblErrorTitle.Content = "Enter a title";
                errors = true;
            }
            else if(title.Length > 50)
            {
                lblErrorTitle.Visibility = System.Windows.Visibility.Visible;
                lblErrorTitle.Content = "Title must be 50 characters or under";
                errors = true;
            }
            else
            {
                lblErrorTitle.Visibility = System.Windows.Visibility.Collapsed;
            }
            if(category.Length > 50)
            {
                lblErrorCategory.Visibility = System.Windows.Visibility.Visible;
                lblErrorCategory.Content = "Category must be 50 characters or less";
                errors = true;
            }
            else
            {
                lblErrorCategory.Visibility = System.Windows.Visibility.Collapsed;
            }
            if(content == null || content == "")
            {
                lblErrorContent.Visibility = System.Windows.Visibility.Visible;
                lblErrorContent.Content = "Enter your question";
                errors = true;
            }
            else
            {
                lblErrorContent.Visibility = System.Windows.Visibility.Collapsed;
            }

            if(errors == false)
            {
                lblErrorCategory.Visibility = System.Windows.Visibility.Collapsed;
                lblErrorContent.Visibility = System.Windows.Visibility.Collapsed;
                lblErrorTitle.Visibility = System.Windows.Visibility.Collapsed;

                question.Title = title;
                if (category != null || category != "")
                {
                    question.Category = category;
                }
                question.Content = content;
                question.CreatedBy = _accessToken.UserID;

                if (_accessToken.RegionId != null)
                {
                    question.RegionID = (short)_accessToken.RegionId;
                }

                question.CreatedDate = DateTime.Now;

                bool created = questionManager.AddQuestion(question);

                if (created)
                {
                        lblSubmit.Content = "Your question has been successfully submitted.";
                    txtCategory.Text = "";
                    txtContent.Text = "";
                    txtTitle.Text = "";

                    this.NavigationService.Navigate(new ExpertPages.SearchForQuestions(_accessToken, question));
                } 
                else
                {
                    lblSubmit.Content = "Your question could not be submitted.";
                }
            }
        }
    }
}
