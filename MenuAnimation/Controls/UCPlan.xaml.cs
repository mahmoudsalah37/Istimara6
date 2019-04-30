using Astmara6Con;
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

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for El5ta.xaml
    /// </summary>
    public partial class UCPlan : UserControl
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        public UCPlan()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCStudentStatement());
            string x = "بيان الطلاب";
            Form.ChFormName(x);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string x = "";
            Form.ChFormName(x);
            UCDataPrint data = new UCDataPrint();
           // data.print.Content = "لقد انتهيت الان يمكنك الطباعة ";
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(data);
           
        }
    }
}
