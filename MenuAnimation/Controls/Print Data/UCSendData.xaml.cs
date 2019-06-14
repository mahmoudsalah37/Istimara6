using Astmara6.Classes;
using MenuAnimado1.Controls;
using System.Windows;
using System.Windows.Controls;


namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for dataAsasya.xaml
    /// </summary>
    public partial class UCSendData : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCSendData()
        {
            InitializeComponent();
            if (TransferData.Semester != null)
                TBSemester.Text = TransferData.Semester;
            if (TransferData.Year != null)
                TBYear.Text = TransferData.Year;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TransferData.Semester = TBSemester.Text;
            TransferData.Year = TBYear.Text;
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDataPrint());
            STRNamePage = "طباعة البيانات";
            Form.ChFormName(STRNamePage);
        }
    }
}
