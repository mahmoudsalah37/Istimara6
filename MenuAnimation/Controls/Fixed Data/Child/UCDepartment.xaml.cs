using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System;
using Astmara6.Data;
using System.Collections.Generic;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for department.xaml
    /// </summary>
    public partial class UCDepartment : UserControl
    {
        private string STRNamePage;
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        private readonly CollegeContext context = new CollegeContext();

        public void loadData()
        {

            var sections = (from p in context.Sections
                          select p).ToList();

            DGDepartmentView.ItemsSource = sections;
        }
        public void check(int length)
        {

            List<Section> listLevel = (from p in context.Sections
                                        where p.TypeOfSection == TBNameDepartment.Text
                                        select p).ToList();

            if (listLevel.Count > 0)
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

        public UCDepartment()
        {
            InitializeComponent();
            loadData();

        }

        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCLevels());
            STRNamePage = "المستويات";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCMajors());
            STRNamePage = "الشعب";
            Form.ChFormName(STRNamePage);
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Section DepartmentRow = DGDepartmentView.SelectedItem as Section;

                Section departments = (from p in context.Sections
                                where p.Id == DepartmentRow.Id
                                select p).Single();
                departments.TypeOfSection = DepartmentRow.TypeOfSection;
                context.SaveChanges();
                loadData();


                MessageBox.Show("تم تعديل الصف بنجاح");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
            TBNameDepartment.Text = "";

        }

        private void BTNRemove_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                Section DepartmentRow = DGDepartmentView.SelectedItem as Section;

                Section sections = (from p in context.Sections
                                where p.Id == DepartmentRow.Id
                                select p).Single();
                context.Sections.Remove(sections);
                context.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح العنصر بنجاح");
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
                context.Sections.RemoveRange(context.Sections);

                context.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح كل البيانات");
            }
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                        "تـأكد من ارتباط البيانات بمعومات اخري");
            }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                context.Sections.Add(new Section()
                {
                    TypeOfSection = TBNameDepartment.Text
                });
                context.SaveChanges();
                loadData();
                MessageBox.Show("تم حفظ العملية بنجاح");
                TBNameDepartment.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

            }
        }

        private void DGDepartmentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TBNameDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            lerror.Content = "";
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;
            check(length);
        }
    }
}
