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
    /// Interaction logic for ExpertAdviceRespond.xaml
    /// </summary>
    public partial class ExpertAdviceRespond : Page
    {
        QuestionManager questionManager = new QuestionManager();
        ResponseManager responseManager = new ResponseManager();
        RegionManager regionManager = new RegionManager();
        UserManager userManager = new UserManager();
        MessageManager messageManager = new MessageManager();
        AccessToken _accessToken = new AccessToken();

        // Rhett Allen - 4/22/16 - adding link to article in response
        Blog article = new Blog();
        BlogManager blogManager = new BlogManager();
        List<Blog> blogs = new List<Blog>();

        // page variables
        PageDetails pageDetails = new PageDetails();
        Paginate<Question> paginate = new Paginate<Question>();
        List<Question> fullList = new List<Question>();

        public ExpertAdviceRespond(AccessToken accessToken)
        {
            InitializeComponent();

            List<Region> regions = regionManager.GetRegions();
            _accessToken = accessToken;
            cmbRegions.Items.Add("All regions");
            cmbRegions.Items.Add("No region");
            foreach (Region region in regions)
            {
                cmbRegions.Items.Add(region);
            }

            cmbRegions.SelectedIndex = 0;

            blogs = blogManager.GetBlogs();
            autoBlogs.ItemsSource = blogs;

            fullList = questionManager.GetQuestions();
            pageDetails = InitializePageDetails();
            gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);

        }

        private void btnSearchRegion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Region region = (Region)cmbRegions.SelectedValue;
                fullList = questionManager.GetQuestionsByRegionID(region.RegionID);
                pageDetails.Count = fullList.Count;
                pageDetails.CurrentPage = 1;
                gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);
            }
            catch (Exception)
            {
                if (cmbRegions.SelectedIndex == 1)
                {
                    fullList = questionManager.GetQuestionsWithNoRegion();
                    pageDetails.Count = fullList.Count;
                    pageDetails.CurrentPage = 1;
                    gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);
                }
                else if (cmbRegions.SelectedIndex == 0)
                {
                    fullList = questionManager.GetQuestions();
                    pageDetails.Count = fullList.Count;
                    pageDetails.CurrentPage = 1;
                    gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);
                }
            }

            ChangeGridVisibility();
            txtKeywords.Text = "";
        }

        private void gridQuestions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtResponse.IsEnabled = true;
            btnResponse.IsEnabled = true;
            autoBlogs.IsEnabled = true;
            btnLinkArticle.IsEnabled = true;
            Question question = new Question();
            try
            {
                question = (Question)gridQuestions.SelectedItem;
                lblContent.Text = question.Content;
                lblQuestion.Content = userManager.GetUser(question.CreatedBy).UserName + " asks...";
                lblQuestionDate.Content = question.CreatedDate.ToString(("MMMM dd, yyyy MM:hh tt"));

                Response response = responseManager.GetResponseByQuestionIDAndUser(question.QuestionID, _accessToken.UserID);
                if (response.QuestionID == question.QuestionID)
                {
                    btnResponse.Content = "Edit";
                    txtResponse.Text = response.UserResponse;
                    try
                    {
                        article = blogManager.GetBlogById((int)response.BlogID);
                        btnArticle.Content = article.BlogTitle;
                    }
                    catch (Exception)
                    {
                        article = new Blog();
                        btnArticle.Content = "";
                    }
                }
                else
                {
                    btnResponse.Content = "Send";
                    txtResponse.Text = "";

                    try
                    {
                        article = blogManager.GetBlogById((int)response.BlogID);
                        btnArticle.Content = article.BlogTitle;
                    }
                    catch (Exception)
                    {
                        article = new Blog();
                        btnArticle.Content = "";
                    }
                }
            }
            catch (Exception)
            {
                lblContent.Text = "";
                lblQuestion.Content = "Question:";
            }
        }

        private void ChangeGridVisibility()
        {
            //if (gridQuestions.Items.Count > 0)
            //{
            //    gridQuestions.Visibility = System.Windows.Visibility.Visible;
            //    lblNoMatch.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //else
            //{
            //    gridQuestions.Visibility = System.Windows.Visibility.Collapsed;
            //    lblNoMatch.Visibility = System.Windows.Visibility.Visible;
            //}
        }

        private void txtKeywords_KeyUp(object sender, KeyEventArgs e)
        {
            try // Selected region
            {
                Region region = (Region)cmbRegions.SelectedValue;
                fullList = questionManager.GetQuestionsWithKeywordAndRegion(txtKeywords.Text, region.RegionID);
                pageDetails.Count = fullList.Count;
                pageDetails.CurrentPage = 1;
                gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);
            }
            catch (Exception)
            {
                if (cmbRegions.SelectedIndex == 0) // All regions
                {
                    fullList = questionManager.GetQuestionsWithKeyword(txtKeywords.Text);
                    pageDetails.Count = fullList.Count;
                    pageDetails.CurrentPage = 1;
                    gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);
                }
                else if (cmbRegions.SelectedIndex == 1) // No region
                {
                    fullList = questionManager.GetQuestionsWithKeywordAndRegion(txtKeywords.Text, null);
                    pageDetails.Count = fullList.Count;
                    pageDetails.CurrentPage = 1;
                    gridQuestions.ItemsSource = paginate.GetList(pageDetails, fullList);
                }
            }

            ChangeGridVisibility();
        }

        private void btnResponse_Click(object sender, RoutedEventArgs e)
        {
            Response response = new Response();
            string responseText = txtResponse.Text;

            if (responseText.Length > 0)
            {
                try
                {
                    Question question = (Question)gridQuestions.SelectedItem;

                    response.Date = DateTime.Now;
                    response.QuestionID = question.QuestionID;
                    response.UserID = _accessToken.UserID;
                    response.UserResponse = responseText;
                    string message = "";
                    message = "Your question '" + question.Title + "' has received a new response from Expert " + _accessToken.UserName + ".";

                    if (article.BlogID != 0)
                    {
                        response.BlogID = article.BlogID;

                    }
                    else
                    {
                        response.BlogID = null;
                    }

                    if (responseText.Length <= 250)
                    {
                        responseManager.AddResponse(response);
                        messageManager.SendMessage(message, "Your question has a new response", _accessToken.UserName, userManager.GetUser(question.CreatedBy).UserName);
                        //if (response.BlogID == null)
                        //    messageManager.SendMessage(message, "Your question has a new response", _accessToken.UserName, userManager.GetUser(question.CreatedBy).UserName);
                        btnResponse.Content = "Edit";
                        this.NavigationService.Navigate(new ExpertPages.SearchForQuestions(_accessToken, question));
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        Response oldResponse = responseManager.GetResponseByQuestionIDAndUser(response.QuestionID, _accessToken.UserID);
                        responseManager.EditResponse(response, oldResponse);
                        Question question = (Question)gridQuestions.SelectedItem;
                        this.NavigationService.Navigate(new ExpertPages.SearchForQuestions(_accessToken, question));
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Your response could not get through.");
                    }
                }
            }
            else
            {
                lblReply.Content = "Enter a response";
                lblReply.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void txtResponse_KeyUp(object sender, KeyEventArgs e)
        {
            string reply = txtResponse.Text;

            if (reply.Length > 250)
            {
                lblReply.Foreground = System.Windows.Media.Brushes.Red;
            }
            else
            {
                lblReply.Foreground = System.Windows.Media.Brushes.Black;
            }

            lblReply.Content = "Your reply: " + (250 - reply.Length) + " characters remaining";
        }

        // Rhett Allen - linking article to response
        private void autoBlogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                article = (Blog)autoBlogs.SelectedItem;
            }
            catch
            {
                article = new Blog();
            }
        }

        private void btnLinkArticle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (null != article.BlogTitle)
                {
                    btnArticle.Content = article.BlogTitle;
                    lblReply.Content = "Your reply: ";
                }
            }
            catch (Exception)
            {
                btnArticle.Content = "";
                lblReply.Content = "That article does not exist.";
                lblReply.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void btnArticle_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePages.ViewBlog(_accessToken, article.BlogID));
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
    }
}
