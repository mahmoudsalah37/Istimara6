
using Astmara6Con;
using Astmara6Con.Controls;
using MenuAnimado1.Controls;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for El5ta.xaml
    /// </summary>
    public partial class UCPlan : UserControl
    {
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        public UCPlan()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCStudentStatement());
            STRNamePage = "بيان الطلاب";
            Form.ChFormName(STRNamePage);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            STRNamePage = "";
            Form.ChFormName(STRNamePage);
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDataPrint()); 
        }
    }
}
