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

namespace myStudyJournal2
{
    /// <summary>
    /// Interaction logic for DatabasePage.xaml
    /// </summary>
    public partial class DatabasePage : Page
    {
        public Frame viewFrame { get; set; }
        public MainWindow.ViewOption postView { get; set; }
        public DatabasePage(Frame frame)
        {
            InitializeComponent();
            populateTopicSelector();
            topicSelector.SelectionChanged += TopicSelector_SelectionChanged;
            this.viewFrame = frame;
            
        }
        private void populateTopicSelector()
        {
            using(var context = new myStudyJournalEntities())
            {
                var dbCollection = context.Posts;
                List<string> uniqueTags = new List<string>();
                foreach (var post in dbCollection)
                {
                    string[] tagArray = post.Tags.Split(',');
                    foreach (var tag in tagArray)
                    {
                        if (!string.IsNullOrEmpty(tag))
                        {
                            string currentTag = tag.Trim().ToUpper();
                            if (!uniqueTags.Contains(currentTag))
                            {
                                uniqueTags.Add(currentTag);
                            }
                        }
                    }
                    
                }
                uniqueTags.Sort();
                foreach (var item in uniqueTags)
                {
                    topicSelector.Items.Add(item);
                }
            }
        }
        private void TopicSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(topicSelector.SelectedItem != null)
            {
                string selectedTopic = topicSelector.SelectedItem.ToString();

                using(var context = new myStudyJournalEntities())
                {
                    var quariedPosts = context.Posts.Where(x => x.Tags.Contains(selectedTopic));
                    List<Post> selectedPosts = new List<Post>(quariedPosts);
                    dataGrid.DataContext = selectedPosts;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            using(var context = new myStudyJournalEntities())
            {
                var dbSet = context.Posts;
                List<Post> posts = new List<Post>(dbSet);
                dataGrid.DataContext = posts;
            }

        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem.ToString();
            var post = Post.parseString(selected);
            var res = MessageBox.Show($"Are you sure you Want to delete Post?", "Database Action", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                using (var context = new myStudyJournalEntities())
                {
                    var postToDelete = context.Posts.Find(post.Id);
                    context.Posts.Remove(postToDelete);
                    await context.SaveChangesAsync();
                }
                MessageBox.Show($"Post ID: {post.Id} Deleted Successfully!", "Database Action");
                this.viewFrame.Navigate(new DatabasePage(this.viewFrame));
            }
            else return;
            
            
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem.ToString();
            var post = Post.parseString(selected);
            this.viewFrame.Navigate(new PostPage(post));
        }

        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem.ToString();
            var post = Post.parseString(selected);
            this.viewFrame.Navigate(new PostView(post));
        }



    }
}
