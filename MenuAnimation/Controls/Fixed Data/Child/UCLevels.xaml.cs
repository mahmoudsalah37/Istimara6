using Astmara6.Data;
using Astmara6Con.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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
        private string STRNamePage;
        private readonly FRMMainWindow form = Application.Current.Windows[0] as FRMMainWindow;
        private readonly CollegeContext context = new CollegeContext();

        public void loadData()
        {

            var levels = (from p in context.Levels
                          select p).ToList();

            DGLevelsView.ItemsSource = levels;
        }

        public UCLevels()
        {
            InitializeComponent();
            loadData();

        }

        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            form.gridShow.Children.Clear();
            form.gridShow.Children.Add(new UCFixedData());
            STRNamePage = "البيانات الثابتة";
            form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            form.gridShow.Children.Clear();
            form.gridShow.Children.Add(new UCDepartment());
            STRNamePage = "الاقسام";
            form.ChFormName(STRNamePage);
        }


        public void check(int length)
        {
           
            List<Level> prelevelname = (from p in context.Levels
                              where p.Name == TBNameLevels.Text
                              select p).ToList();

            if (prelevelname.Count > 0)
            {
                BTNAdd.IsEnabled = false;
                lerror.Content = "لقد ادخلت هذا من قبل ";
            }
            else
            {
                BTNAdd.IsEnabled = true;
            }
            if (length < 1)
            {
                BTNAdd.IsEnabled = false;
                lerror.Content = "ادخل بيانات";
            }

        }


        private void BTNAdd_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                lerror.Content = "";

                if (TBNameLevels.Text == "")
                {
                    MessageBox.Show("انت لم تدخل شيئا!!");

                }
                else
                {
                    context.Levels.Add(new Level()
                    {
                        Name = TBNameLevels.Text,

                    });
                    context.SaveChanges();
                    loadData();
                    TBNameLevels.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

            }
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Level LevelRow = DGLevelsView.SelectedItem as Level;

                Level levels = (from p in context.Levels
                                where p.Id == LevelRow.Id
                                select p).Single();
                levels.Name = LevelRow.Name;
                context.SaveChanges();
                loadData();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
            TBNameLevels.Text = "";

        }

        private void DGLevelsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void DGLevelsView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BTNRemove_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("سوف يتم مسح هذا العنصر؟", "تأكيد الحذف ", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
            {
                Level LevelRow = DGLevelsView.SelectedItem as Level;

                Level levels = (from p in context.Levels
                                where p.Id == LevelRow.Id
                                select p).Single();
                context.Levels.Remove(levels);
                context.SaveChanges();
                loadData();

            }
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                         "تـأكد من ارتباط البيانات بمعومات اخري");
            }
            }
            else
            {
                MessageBox.Show("لاتقلق لم تمسح اي بيانات");
            }

        }

        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("سوف يتم مسح كل البيانات؟", "تأكيد الحذف ", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
            {
                context.Levels.RemoveRange(context.Levels);
                context.SaveChanges();
                loadData();
            }
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                        "تـأكد من ارتباط البيانات بمعومات اخري");
            }
        }
            else
            {
                MessageBox.Show("لاتقلق لم تمسح اي بيانات");
            }


}

        private void TBNameLevels_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            lerror.Content = "";
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;
            check(length);
        }
    }
}
