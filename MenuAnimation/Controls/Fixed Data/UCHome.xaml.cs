using System;
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
            //MEVedio.Source = new Uri("ms-appx:///Assets/vedio2.mp4");
            //MEVedio.SetValue = Astmara6.Properties.Resources.vedio2;
            //var path = System.IO.Directory.GetDirectories("Assets\vedio2.mp4").ToString();
            //MEVedio.Source = new Uri(path);

            MEVedio.Play();
            //System.IO.Directory.GetCurrentDirectory().ToString() + "Assets\vedio2.mp4";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form.home();
        }
    }
}
