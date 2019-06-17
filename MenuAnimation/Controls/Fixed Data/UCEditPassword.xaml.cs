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

namespace Astmara6.Controls.Fixed_Data.Child
{
    /// <summary>
    /// Interaction logic for UCEditPassword.xaml
    /// </summary>
    public partial class UCEditPassword : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        public UCEditPassword()
        {
            InitializeComponent();

            string datauser = (from p in context.Users
                               select p).First().Name;
            string datapass = (from p in context.Users
                               select p).First().Password;
            TBUserName.Text = datapass;
            TBUserName_Copy.Text = datauser;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Edit = (from p in context.Users
                                   select p).First();
                Edit.Password = TBUserName.Text;
                Edit.Name= TBUserName_Copy.Text;
                context.SaveChanges();
                MessageBox.Show("تم التعديل بنجاح");
            }
            catch (Exception)
            {
                MessageBox.Show("الرجاء المحاولة مرة اخري");
            }
        }
    }
}
