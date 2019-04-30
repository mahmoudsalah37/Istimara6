using System.Windows;
using System.Windows.Controls;


namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for work_hours.xaml
    /// </summary>
    public partial class UCWorkHours : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCWorkHours()
        {
            InitializeComponent();
        }


        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCCourses());
            STRNamePage = "المواد";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDoctors());
            STRNamePage = "الدكاترة";
            Form.ChFormName(STRNamePage);
        }
    }
}
