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
    /// Interaction logic for PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {

        public bool isEditing { get; set; }
        public Post postToEdit { get; set; }
        public PostPage()
        {
            InitializeComponent();
            this.isEditing = false;
            populateTopicList();
            title.GotFocus += onTitleFocus;
            title.LostFocus += onTitleUnFocus;
            addBtn.Click += onAddBtnClicked;
            removeBtn.Click += onRemoveBtnClicked;
            submitBtn.Click += onSubmitBtnClicked;
            otherCatagory.GotFocus += onOtherCatagoryFocus;
            otherCatagory.LostFocus += onOtherCatagoryUnfocus;
            otherCatagory.KeyDown += onOtherCatagoryKeyDown;
            listBox1.KeyDown += onListbox1KeyDown;
            listBox2.KeyDown += onListbox2KeyDown;
            dateTime.Text = DateTime.Now.ToString();
            sortLists();
        }

        

        public PostPage(Post post)
        {
            InitializeComponent();
            this.isEditing = true;
            this.postToEdit = post;
            populateTopicList();
            title.GotFocus += onTitleFocus;
            title.LostFocus += onTitleUnFocus;
            addBtn.Click += onAddBtnClicked;
            removeBtn.Click += onRemoveBtnClicked;
            submitBtn.Click += onSubmitBtnClicked;
            otherCatagory.GotFocus += onOtherCatagoryFocus;
            otherCatagory.LostFocus += onOtherCatagoryUnfocus;
            otherCatagory.KeyDown += onOtherCatagoryKeyDown;
            listBox1.KeyDown += onListbox1KeyDown;
            listBox2.KeyDown += onListbox2KeyDown;
            title.Text = this.postToEdit.Title;
            dateTime.Text = this.postToEdit.Date.ToString();
            string[] tags = this.postToEdit.Tags.Split(',');
            foreach (var item in tags)
            {
                string tag = item.ToUpper();
                if (!string.IsNullOrEmpty(tag))
                {
                    listBox2.Items.Add(tag);
                    listBox1.Items.Remove(tag);
                }
            }
            body.Text = this.postToEdit.Body;
            sortLists();
        }

        private void onOtherCatagoryUnfocus(object sender, RoutedEventArgs e)
        {
            if (otherCatagory.Text == "")
            {
                otherCatagory.Text = "Other Catagory";
            }
            else return;
        }

        private void onListbox2KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                onRemoveBtnClicked(sender, e);
            }
        }

        private void onListbox1KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                onAddBtnClicked(sender, e);
            }
            else return;
        }

        private void onOtherCatagoryKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                onAddBtnClicked(sender, e);
            }
            else return;
        }

        

        private void onOtherCatagoryFocus(object sender, RoutedEventArgs e)
        {
            if (otherCatagory.Text == "Other Catagory")
            {
                otherCatagory.Text = "";
            }
            else return;
        }

        private void onTitleUnFocus(object sender, RoutedEventArgs e)
        {
            if (title.Text == "")
            {
                title.Text = "Enter a Title";
            }
            else return;
        }



        private void onTitleFocus(object sender, RoutedEventArgs e)
        {
            if (title.Text == "Enter a Title")
            {
                title.Text = "";
            }
            else return;

        }



        private async void onSubmitBtnClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(body.Text.Length);
            if (title.Text != "" && title.Text != "Enter a Title" && listBox2.Items.Count != 0 && body.Text != "")
            {
                using (var context = new myStudyJournalEntities())
                {
                    if (isEditing)
                    {
                        Post edit = new Post()
                        {
                            Id = this.postToEdit.Id,
                            Date = DateTime.Parse(dateTime.Text),
                            Title = title.Text,
                            Tags = tagsToString(),
                            Body = body.Text
                        };
                        var confirmEdit = MessageBox.Show("Save edit?", "Database Action", MessageBoxButton.OKCancel);
                        if(confirmEdit == MessageBoxResult.OK)
                        {
                            var selected = context.Posts.Find(this.postToEdit.Id);
                            context.Entry(selected).CurrentValues.SetValues(edit);
                            await context.SaveChangesAsync();
                            MessageBox.Show("edit saved", "Database Action");
                        }
                        else
                        {
                            return;
                        }
                        
                    }
                    else
                    {
                        Post entry = new Post()
                        {
                            Date = DateTime.Parse(dateTime.Text),
                            Title = title.Text,
                            Tags = tagsToString(),
                            Body = body.Text
                        };
                        var confirmation = MessageBox.Show("Add post?", "Database Action", MessageBoxButton.OKCancel);
                        if(confirmation == MessageBoxResult.OK)
                        {
                            context.Posts.Add(entry);
                            await context.SaveChangesAsync();
                            MessageBox.Show("Post saved", "Database Action");
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("All fields must be entered to post.", "Error");
                return;
            }

            string tagsToString()
            {
                string[] tags = new string[listBox2.Items.Count];
                listBox2.Items.CopyTo(tags, 0);
                string items = String.Join(",", tags).ToUpper();
                return items;
            }

        }



        private void onRemoveBtnClicked(object sender, RoutedEventArgs e)
        {
            var selectedItem = listBox2.SelectedItem;
            if (selectedItem == null) return;
            listBox2.Items.Remove(selectedItem);
            listBox1.Items.Add(selectedItem);
            sortLists();
        }



        private void onAddBtnClicked(object sender, RoutedEventArgs e)
        {
            var listBoxCollection = listBox2.Items.Cast<string>();
            var selectedItem = listBox1.SelectedItem;
            if(listBox1.SelectedItem == null && !listBoxCollection.Contains(otherCatagory.Text) && otherCatagory.Text != "Other Catagory" && otherCatagory.Text != "")
            {
                listBox2.Items.Add(otherCatagory.Text.ToUpper());
                sortLists();
                otherCatagory.Text = "";

            }
            else if(selectedItem != null)
            {
                listBox2.Items.Add(selectedItem);
                listBox1.Items.Remove(selectedItem);
                sortLists();
            }
        }


        private void sortLists()
        {
            //Listbox1 Sort
            var listbox1Items = listBox1.Items;
            List<string> listbox1Collection = new List<string>();
            foreach (var item in listbox1Items)
            {
                listbox1Collection.Add(item.ToString());
            }
            listBox1.Items.Clear();
            listbox1Collection.Sort();
            populateList(listBox1, listbox1Collection);
            
            //Listbox2 sort


            var listbox2Items = listBox2.Items;
            List<string> listbox2Collection = new List<string>();
            foreach (var item in listbox2Items)
            {
                listbox2Collection.Add(item.ToString());
            }
            listBox2.Items.Clear();
            listbox2Collection.Sort();
            populateList(listBox2, listbox2Collection);
        }
    }
}
