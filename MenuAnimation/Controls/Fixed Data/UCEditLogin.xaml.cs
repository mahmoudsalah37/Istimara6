using Astmara6.Controls.Fixed_Data.Child;
using Astmara6.Data;
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

namespace Astmara6.Controls.Fixed_Data
{
    /// <summary>
    /// Interaction logic for UCEditLogin.xaml
    /// </summary>
    public partial class UCEditLogin : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        public UCEditLogin()
        {
            InitializeComponent();
        }

        private void BTNLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String User = TBUserName.Text;
                String pass = TBPass.Password.ToString();
                string datauser = (from p in context.Users
                                   select p).First().Name;
                string datapass = (from p in context.Users
                                   select p).First().Password;
                if (User == datauser & pass == datapass)
                {
                    MessageBox.Show("الان يمطنك التعديل");
                    Form.gridShow.Children.Clear();
                    Form.gridShow.Children.Add(new UCEditPassword());
                    string STRNamePage = "تعديل الباسورد ";
                    Form.ChFormName(STRNamePage);
                }
                else
                {
                    MessageBox.Show("راجع بياناتك مرة اخري ");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ");

            }
        }
    }
}
