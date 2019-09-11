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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            databasePageBtn.Click += onDatabasePageBtn_Clicked;
            postPageBtn.Click += onPostPageBtn_Clicked;
            graphViewBtn.Click += onGraphPageBtn_Clicked;
            view.Navigate(new PostPage());

        }

        private void onGraphPageBtn_Clicked(object sender, RoutedEventArgs e)
        {
            selectView(ViewOption.GraphPage);
        }

        private void onPostPageBtn_Clicked(object sender, RoutedEventArgs e)
        {
            selectView(ViewOption.PostPage);
        }

        private void onDatabasePageBtn_Clicked(object sender, RoutedEventArgs e)
        {
            selectView(ViewOption.DatabasePage);
        }

       
    }
}
