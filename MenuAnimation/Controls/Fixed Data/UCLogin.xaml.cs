using Astmara6.Data;
using Astmara6Con;
using Astmara6Con.Controls;
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

namespace Astmara6.Controls.Fixed_Data
{
    /// <summary>
    /// Interaction logic for UCLogin.xaml
    /// </summary>
    public partial class UCLogin : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCLogin()
        {
            Form.ButtonOpenMenu.Visibility=Visibility.Hidden;
            InitializeComponent();
        }

        private void BTNLogin_Click(object sender, RoutedEventArgs e)
        {
            String User = TBUserName.Text;
            String pass = TBPass.Password.ToString();
            string datauser = (from p in context.Users
                               select p).First().Password;
            string datapass = (from p in context.Users
                               select p).First().Name;
            if (User == datauser & pass == datapass)
            {
                MessageBox.Show("(; مرحبا");
                Form.gridShow.Children.Clear();
                Form.gridShow.Children.Add(new UCFixedData());
                string STRNamePage = "الصفحة الرئيسية";
                Form.ChFormName(STRNamePage);
                Form.ButtonOpenMenu.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("راجع بياناتك مرة اخري ");
            }

        }
    }
}
