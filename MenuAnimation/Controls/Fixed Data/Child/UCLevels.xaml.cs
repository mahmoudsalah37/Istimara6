using Astmara6.Data;
using Astmara6Con.Controls;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Astmara6Con
{
    /// <summary>
    /// Interaction logic for levels.xaml
    /// </summary>
    public partial class UCLevels : UserControl
    {
        static Model1 context = new Model1();
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        public UCLevels()
        {
            InitializeComponent();
            getData();
        }
        private void getData()
        {
            DbSet<Level> DSLevels =context.Levels;

            DGLevelsView.ItemsSource = DSLevels.ToArray();
        }
        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCFixedData());
            STRNamePage = "البيانات الثابتة";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDepartment());
            STRNamePage = "الاقسام";
            Form.ChFormName(STRNamePage);
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                var level = new Level
                {
                    name = TBNameLevels.Text,
                };
                TBNameLevels.Text = "";
                context.Levels.Add(level);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطا إثناء إضافة البيانات");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
