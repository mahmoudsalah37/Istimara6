using Astmara6.Data;
using Astmara6Con.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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


        public void cheak(int length)
        {
           
            List<Level> prelevelname = (from p in context.Levels
                              where p.Name == TBNameLevels.Text
                              select p).ToList();

            if (prelevelname.Count > 0)
            {
                lerror.Content = "لقد ادخلت هذا من قبل ";
                BTNAdd.IsEnabled = false;
            }
            else
            {
                BTNAdd.IsEnabled = true;
            }
            if (length < 1)
                BTNAdd.IsEnabled = false;

        }


        private void BTNAdd_Click_1(object sender, RoutedEventArgs e)
        {
            lerror.Content="";
            
            if (TBNameLevels.Text =="")
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
                MessageBox.Show("تم حفظ العملية بنجاح");
                TBNameLevels.Text = "";
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
            try
            {
                Level LevelRow = DGLevelsView.SelectedItem as Level;

                Level levels = (from p in context.Levels
                                where p.Id == LevelRow.Id
                                select p).Single();
                context.Levels.Remove(levels);
                context.SaveChanges();
                loadData();

                MessageBox.Show("تم حذف الصف بنجاح");
            }
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                         "تـأكد من ارتباط البيانات بمعومات اخري");
            }

        }

        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                context.Levels.RemoveRange(context.Levels);

                context.SaveChanges();
                loadData();

                MessageBox.Show("كل البيانات حذفت بنجاح");
            }
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                        "تـأكد من ارتباط البيانات بمعومات اخري");
            }


        }

        private void TBNameLevels_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            lerror.Content = "";
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;
            cheak(length);
            
            
        }
    }
}
