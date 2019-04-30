using MenuAnimado1.Controls;
using Astmara6Con.Controls;
using System.Windows;


namespace Astmara6Con
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class FRMMainWindow : Window
    {
        public FRMMainWindow()
        {
            InitializeComponent();
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCFixedData());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void listViewItem_Selected(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCFixedData());
            string STRNamePage = "البيانات الثابتة";
            ChFormName(STRNamePage);

        }
        public void home()
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCLevels());
            string STRNamePage = "المستويات";
            ChFormName(STRNamePage);
        }

     
        public void ChFormName(string STRNamePage)
        {
            formname.Content = STRNamePage;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void listViewItem1_Selected(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCChangingData());
            string STRNamePage = "البيانات المتغيرة";
            ChFormName(STRNamePage);
        }


        private void ListViewItem2_Selected(object sender, RoutedEventArgs e)
        {
            gridShow.Children.Clear();
            gridShow.Children.Add(new UCDataPrint());
            string STRNamePage = "طباعة البيانات";
            ChFormName(STRNamePage);
        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
              gridShow.Children.Clear();
            gridShow.Children.Add(new UCHome());
            string STRNamePage = "الصفحة الرئيسية";
            ChFormName(STRNamePage);
        }

    }
}
