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
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Interaction logic for SearchForQuestions.xaml
    /// </summary>
    public partial class SearchForQuestions : Page
    {
        AccessToken _accessToken = new AccessToken();
        QuestionManager questionManager = new QuestionManager();
        ResponseManager responseManager = new ResponseManager();
        BlogManager blogManager = new BlogManager();
        UserManager userManager = new UserManager();
        bool hasChangedQuestion = false;
        List<Question> questions = new List<Question>();

        // page variables
        PageDetails pageDetails = new PageDetails();
        Paginate<Question> paginate = new Paginate<Question>();
        List<Question> fullList = new List<Question>();

        public SearchForQuestions(AccessToken accessToken)
        {
            InitializeComponent();

            _accessToken = accessToken;
            ValidateAccessToken();

            pageDetails = InitializePageDetails();
            fullList = questionManager.GetQuestions();

            questions = paginate.GetList(pageDetails, questionManager.GetQuestions());

            gridQuestions.ItemsSource = questions;
        }

        public SearchForQuestions(AccessToken accessToken, Question question)
        {
            InitializeComponent();

            _accessToken = accessToken;
            ValidateAccessToken();
            cmbMyQuestions.SelectedIndex = cmbMyQuestions.Items.Count - 1;
            ChangeQuestionAndResponses(question.QuestionID);
            lblNoReplies.Content = "Your question has been successfully submitted. Come back later to check replies.";
            lblContent.Text = question.Content;
            lblQuestion.Content = userManager.GetUser(question.CreatedBy).UserName + " asks...";
            lblQuestionDate.Content = question.CreatedDate.ToString(("MMMM dd, yyyy MM:hh tt"));

            pageDetails = InitializePageDetails();
            fullList = questionManager.GetQuestions();

            questions = paginate.GetList(pageDetails, fullList);

            gridQuestions.ItemsSource = questions;
        }

        private void ValidateAccessToken()
        {
            List<Question> questions = new List<Question>();

            if (_accessToken != null)
            {
                questions = questionManager.GetQuestionsByUserID(_accessToken.UserID);
                if (questions.Count > 0)
                {
                    gridMyQuestions.Visibility = System.Windows.Visibility.Visible;
                    cmbMyQuestions.ItemsSource = questions;
                    cmbMyQuestions.SelectedIndex = 0;
                }
                else
                {
                    gridMyQuestions.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            else
            {
                gridMyQuestions.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void txtKeywords_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                fullList = questionManager.GetQuestionsWithKeyword(txtKeywords.Text);
                pageDetails.CurrentPage = 1;
                pageDetails.Count = fullList.Count;
                questions = paginate.GetList(pageDetails, fullList);

                gridQuestions.ItemsSource = questions;
            }
            catch (Exception)
            {

            }

            ChangeGridVisibility();
        }

        private void gridQuestions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Question question = (Question)gridQuestions.SelectedItem;
                ChangeQuestionAndResponses(question.QuestionID);
            }
            catch (Exception)
            {

            }
        }

        private void ChangeGridVisibility()
        {
            if (gridQuestions.Items.Count > 0)
            {
                //gridQuestions.Visibility = System.Windows.Visibility.Visible;
                //lblNoMatch.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                //gridQuestions.Visibility = System.Windows.Visibility.Collapsed;
                //lblNoMatch.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ChangeQuestionAndResponses(int questionID)
        {
            //gridQuestion.Visibility = System.Windows.Visibility.Visible;
            Question question = questionManager.GetQuestionByID(questionID);
            lblContent.Text = question.Content;
            lblQuestion.Content = userManager.GetUser(question.CreatedBy).UserName + " asks...";
            lblQuestionDate.Content = question.CreatedDate.ToString(("MMMM dd, yyyy MM:hh tt"));
            List<Response> responses = new List<Response>();

            try
            {
                responses = responseManager.GetResponsesByQuestionID(questionID);
            }
            catch (Exception)
            {
                lblNoReplies.Visibility = System.Windows.Visibility.Visible;
            }


            if (responses.Count > 0)
            {
                icResponses.Items.Clear();

                foreach (Response r in responses)
                {
                    User user = userManager.GetUser(r.UserID);

                    if (r.BlogID != null)
                    {
                        icResponses.Items.Add(new ResponseView
                        {
                            Name = user.UserName,
                            UserResponse = r.UserResponse,
                            Date = r.Date,
                            ArticleName = blogManager.GetBlogById((int)r.BlogID).BlogTitle,
                            BlogID = r.BlogID
                        });
                    }
                    else
                    {
                        icResponses.Items.Add(new ResponseView
                        {
                            Name = user.UserName,
                            UserResponse = r.UserResponse,
                            Date = r.Date
                        });
                    }
                }
            }
            else
            {
                if (hasChangedQuestion)
                {
                    lblNoReplies.Content = "There are no replies for this question";
                }

                icResponses.Items.Clear();
            }

            hasChangedQuestion = true;
        }

        private void cmbMyQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtKeywords.Text = "";
            Question question = (Question)cmbMyQuestions.SelectedItem;
            ChangeQuestionAndResponses(question.QuestionID);
            //gridQuestions.Visibility = System.Windows.Visibility.Collapsed;
        }

        /* Rhett Allen - adding pages */
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (pageDetails.CurrentPage > 1)
            {
                pageDetails.CurrentPage--;
            }

            SelectQuestions();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (pageDetails.CurrentPage < pageDetails.MaxPages)
            {
                pageDetails.CurrentPage++;
            }

            SelectQuestions();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            pageDetails.CurrentPage = 1;
            SelectQuestions();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            pageDetails.CurrentPage = pageDetails.MaxPages;
            SelectQuestions();
        }

        private void SelectQuestions()
        {
            try
            {
                gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);
                lblPage.Content = "Page " + pageDetails.CurrentPage;
            }
            catch (Exception)
            {
                gridQuestions.ItemsSource = new List<Question>();
            }
        }

        private PageDetails InitializePageDetails()
        {
            PageDetails p = new PageDetails();
            p.Count = questionManager.GetQuestions().Count;
            p.CurrentPage = 1;
            p.PerPage = 5;

            return p;
        }

        private void btnArticle_Click(object sender, RoutedEventArgs e)
        {
            ResponseView article = ((Button)sender).Tag as ResponseView;
            this.NavigationService.Navigate(new HomePages.ViewBlog(_accessToken, (int)article.BlogID));
        }
    }
}
