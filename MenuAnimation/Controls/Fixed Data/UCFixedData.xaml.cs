using System.Windows;
using System.Windows.Controls;


namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for dataAsasya.xaml
    /// </summary>
    public partial class UCFixedData : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCFixedData()
        {
            InitializeComponent();
        }

        private void BtNLevels_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCLevels());
            STRNamePage = "المستويات";
            Form.ChFormName(STRNamePage);
        }

        private void BTNDepartment_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDepartment());
            STRNamePage = "الأقسام";
            Form.ChFormName(STRNamePage);
        }

        private void BTNMajors_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCMajors());
            STRNamePage = "الشعب";
            Form.ChFormName(STRNamePage);
        }

        private void BTNCourses_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCCourses());
            STRNamePage = "المواد";
            Form.ChFormName(STRNamePage);
        }

        private void BTNWorkHours_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCWorkHours());
            STRNamePage = "ساعات العمل";
            Form.ChFormName(STRNamePage);
        }

        private void BTNDoctors_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDoctors());
            STRNamePage = "الدكاترة";
            Form.ChFormName(STRNamePage);
        }

        private void BTNAssistant_Click(object sender, RoutedEventArgs e)
        {
            //Form.gridShow.Children.Clear();
            //Form.gridShow.Children.Add(new UCAssistant());
            //STRNamePage = "السماعدين";
            //Form.ChFormName(STRNamePage);
        }
    }
}
