using Astmara6.Controls.Print_Data.Child;
using Astmara6Con;
using System.Windows;
using System.Windows.Controls;


namespace MenuAnimado1.Controls
{
    /// <summary>
    /// Interaction logic for dataPrint.xaml
    /// </summary>
    public partial class UCDataPrint : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCDataPrint()
        {
            InitializeComponent();
        }

        private void BTNIstimaraA_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCIstimaraA());
            STRNamePage = "إستمارة أ";
            Form.ChFormName(STRNamePage);
        }

        private void BTNIstimaraBAssistants_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCIstimaraBAssistants());
            STRNamePage = "إستمارة ب مساعدين";
            Form.ChFormName(STRNamePage);
        }

        private void BTNIstimaraBDoctors_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCIstimaraBDoctors());
            STRNamePage = "إستمارة ب دكاترة";
            Form.ChFormName(STRNamePage);
        }

        private void BTNPlan_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCPlan());
            STRNamePage = "الخطة";
            Form.ChFormName(STRNamePage);
        }
    }
}
