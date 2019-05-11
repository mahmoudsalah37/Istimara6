

using Data.Context;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for docinfo.xaml
    /// </summary>
    public partial class UCDoctors : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        readonly CollegeContext context = new CollegeContext();

        public void loadData()
        {

            var teachers = (from p in context.Teachers 
                          select p).ToList();
            var teachersANDWorkHours = (from r in context.Teachers  
                             join w in context.WorkHours
                              on r.IdWorkHours equals w.Id
                                        select r).ToList();
            DGTeachersView.ItemsSource = teachersANDWorkHours;
        }

        public UCDoctors()
        {
            InitializeComponent();
            loadData();
        }

   

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCWorkHours());
            STRNamePage = "النصاب القانوني";
            Form.ChFormName(STRNamePage);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Form.gridShow.Children.Clear();
            //Form.gridShow.Children.Add(new UCAssistant());
            //STRNamePage = "المعيدين";
            //Form.ChFormName(STRNamePage);

        }
    }
}
