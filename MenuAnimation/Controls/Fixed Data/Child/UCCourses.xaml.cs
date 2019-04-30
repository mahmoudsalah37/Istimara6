using System.Windows;

using System.Windows.Controls;


namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for courses.xaml
    /// </summary>
    public partial class UCCourses : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCCourses()
        {
            InitializeComponent();
        }


        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCMajors());
            STRNamePage = "الشعب";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCWorkHours());
            STRNamePage = "ٍساعات العمل";
            Form.ChFormName(STRNamePage);
        }
    }
}
