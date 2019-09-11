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
using LiveCharts;
using LiveCharts.Wpf;

namespace myStudyJournal2
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphPage : Page
    {
        public List<string> labels { get; set; }
        public Func<int,string> labelFormat { get; set; }
        public SeriesCollection chartData { get; set; }
        public class tagProperties
        {
            public string tagName { get; set; }
            public int tagCount { get; set; }
        }
        public GraphPage()
        {
            InitializeComponent();
            chartView.SeriesColors = new ColorsCollection()
            {
                Color.FromArgb(140, 255, 100, 2),
            };
            
            var count = new ChartValues<int>();
            var name = new List<string>();
            foreach (var item in getStudyData())
            {
                name.Add(item.tagName);
                count.Add(item.tagCount);
            }
            labels = name;
            labelFormat = value => value.ToString(); 
            chartData = new SeriesCollection()
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = count
                }
               
            };
            DataContext = this;
        }

        public List<tagProperties> getStudyData()
        {
            using (var context = new myStudyJournalEntities())
            {
                var dbCollection = context.Posts.SqlQuery("SELECT * FROM dbo.Post");
                var tagData = new List<tagProperties>();
                var tagCollection = new List<string>();
                var uniqueTags = new List<string>();
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
                            tagCollection.Add(currentTag);
                        }
                    }
                }
                uniqueTags.Sort();
                foreach (var item in uniqueTags)
                {
                    var likeTags = tagCollection.Where(x => x == item);
                    int count = likeTags.Count();
                    tagData.Add(new tagProperties()
                    {
                        tagName = item,
                        tagCount = count
                    });
                }
                return tagData;
            }
        }
    }
}
