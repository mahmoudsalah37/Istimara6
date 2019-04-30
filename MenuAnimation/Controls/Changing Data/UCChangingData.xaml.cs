using Astmara6Con;
using Astmara6Con.Controls;
using System;
using System.Windows;
using System.Windows.Controls;


namespace MenuAnimado1.Controls
{
    /// <summary>
    /// Interaction logic for datamot3yra.xaml
    /// </summary>
    public partial class UCChangingData : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCChangingData()
        {
            InitializeComponent();
        }

        private void BTNStudentStatement_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCStudentStatement());
            STRNamePage = "بيان الطلاب";
            Form.ChFormName(STRNamePage);
        }

        private void BTNSubjectsChoice_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCPlan());
            STRNamePage = "نوزيع البيانات";
            Form.ChFormName(STRNamePage);
        }
    }
}
