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

    public partial class MainWindow
    {
        public enum ViewOption
        {
            PostPage,
            DatabasePage,
            GraphPage

        }

        public void selectView(ViewOption option)
        {
            switch (option)
            {
                case ViewOption.PostPage:
                    view.Navigate(new PostPage());
                    break;
                case ViewOption.DatabasePage:
                    view.Navigate(new DatabasePage(this.view));
                    break;
                case ViewOption.GraphPage:
                    view.Navigate(new GraphPage());
                    break;
            }
        }



        
    }

    public partial class PostPage
    {


        public void populateList(ListBox listBox, List<string> collection)
        {
            foreach (var item in collection)
            {
                listBox.Items.Add(item.ToUpper());
            }
        }


        
        public void populateTopicList()
        {
            //var uniqueItemList = new List<string>();

            foreach (var tagArr in getTags())
            {
                foreach (var item in tagArr)
                {
                    Console.WriteLine(item.Trim().ToUpper());
                    if (!listBox1.Items.Contains(item.Trim().ToUpper()) && !string.IsNullOrEmpty(item))
                    {
                        listBox1.Items.Add(item.Trim().ToUpper());
                    }
                }
            }


            List<string[]> getTags()
            {
                var tagCollection = new List<string[]>();
                using (var context = new myStudyJournalEntities())
                {
                    var dbCollection = context.Posts.SqlQuery("SELECT * FROM dbo.Post").ToList<Post>();
                    foreach (var tagEntry in dbCollection)
                    {
                        tagCollection.Add(tagEntry.Tags.Split(','));
                    }
                }
                return tagCollection;
            }
        }



        public void saveEdit(int id)
        {
            using (var context = new myStudyJournalEntities())
            {
                var postToEdit = context.Posts.Find(id);
                var post = new Post()
                {
                    Id = id,
                    Title = title.Text,
                    Date = DateTime.Now.Date,
                    Tags = tagsToString(),
                    Body = body.Text
                };
                context.Entry(postToEdit).CurrentValues.SetValues(post);
                context.SaveChanges();
            }
            

            string tagsToString()
            {
                string[] tags = new string[listBox2.Items.Count];
                listBox2.Items.CopyTo(tags, 0);
                string items = String.Join(",", tags);
                return items;
            }
        }


    }
}