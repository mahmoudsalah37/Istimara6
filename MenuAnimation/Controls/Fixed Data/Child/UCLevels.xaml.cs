using Astmara6Con.Controls;
using System.Windows;
using System.Windows.Controls;


namespace Astmara6Con
{
    /// <summary>
    /// Interaction logic for levels.xaml
    /// </summary>
    public partial class UCLevels : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        public UCLevels()
        {
            InitializeComponent();
        }

        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCFixedData());
            STRNamePage = "البيانات الثابتة";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDepartment());
            STRNamePage = "الاقسام";
            Form.ChFormName(STRNamePage);
        }
    }
}
