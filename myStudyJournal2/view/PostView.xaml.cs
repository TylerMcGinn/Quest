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
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class PostView : Page
    {
        //TODO: make a better format for post view.
        public PostView(Post post)
        {
            InitializeComponent();
            this.post = post;
        }
        public Post post { get; set; }

        public void renderPost()
        {
            var formattedTitle = $"Title: {this.post.Title}\nDate: {this.post.Date}\nTags: {this.post.Tags.ToUpper()}";
            postTitle.Text = formattedTitle;
            postViewer.Text = this.post.Body;
        }

        private void PostViewer_Loaded(object sender, RoutedEventArgs e)
        {
            renderPost();
        }
    }
}
