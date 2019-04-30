using System.Windows;
using System.Windows.Controls;


namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class UCHome : UserControl
    {
        FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        public UCHome()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form.home();
        }
    }
}
