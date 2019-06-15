using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System;
using Astmara6.Data;
using System.Collections.Generic;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for majors.xaml
    /// </summary>
    public partial class UCMajors : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        private string STRNamePage;
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        public void loadData()
        {

            var sections = (from p in context.Sections
                            select p).ToList();
            var branchs = (from p in context.Branches
                           select p).ToList();

            DGMajorsView.ItemsSource = sections;
            DGMajorsView.ItemsSource = branchs;
        }
        public void TakeDataFromCombo()
        {
            Section SectionCB = CBNameDepartment.SelectedItem as Section;
            Section sections = (from p in context.Sections
                                where p.Id == SectionCB.Id
                                select p).Single();

            context.Branches.Add(new Branch()
            {
                IdSection = SectionCB.Id,
                Name = TBNameMajors.Text
            });
            context.SaveChanges();
            loadData();


        }
        public void loadDataCombo()
        {

            var sections = (from p in context.Sections
                            select p).ToList();

            CBNameDepartment.ItemsSource = sections;
        }

        public UCMajors()
        {
            InitializeComponent();
            loadDataCombo();
            loadData();
        }

        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDepartment());
            STRNamePage = "الاقسام";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCCourses());
            STRNamePage = "المواد";
            Form.ChFormName(STRNamePage);
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TakeDataFromCombo();
                TBNameMajors.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

            }

        }

        private void BranchesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Branch DepartmentRow = DGMajorsView.SelectedItem as Branch;


                Branch departments = (from p in context.Branches
                                      where p.Id == DepartmentRow.Id
                                      select p).Single();
                departments.Name = DepartmentRow.Name;
                context.SaveChanges();
                loadData();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }

        private void BTNRemove_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("سوف يتم مسح هذا العنصر؟", "تأكيد الحذف ", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
            {

                Branch BranchRow = DGMajorsView.SelectedItem as Branch;

                Branch branchs = (from p in context.Branches
                                  where p.Id == BranchRow.Id
                                  select p).Single();
                context.Branches.Remove(branchs);
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
                context.Branches.RemoveRange(context.Branches);

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
        public bool check(int length, string name)
        {

            List<Section> listLevel = (from p in context.Sections
                                       where p.TypeOfSection == name
                                       select p).ToList();
            if (length < 1)
            {
                lerror1.Content = "أخنر من البيانات";
                return false;
            }
            else if (listLevel.Count < 1)
            {
                lerror.Content = "أختر من البيانات";
                return false;
            }
            else
            {
                return true;
            }
            

        }

        public bool checkMajors(int length)
        {

            List<Branch> listBranches = (from p in context.Branches
                                       where p.Name == TBNameMajors.Text
                                       select p).ToList();
            if (length < 1)
            {
                lerror1.Content = "أدخل بيانات";
                return false;
            }
            else if (listBranches.Count > 0)
            {
                lerror1.Content = "لقد ادخلت هذا من قبل";
                return false;
            }
            else
            {
                return true;
            }
            

        }


        private void TBNameMajors_TextChanged(object sender, TextChangedEventArgs e)
        {
            lerror.Content = "";
            lerror1.Content = "";
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;
            if (check(CBNameDepartment.Text.Length, CBNameDepartment.Text) && checkMajors(length))
                BTNAdd.IsEnabled = true;
            else
                BTNAdd.IsEnabled = false;
        }

        private void CBNameDepartment_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            lerror.Content = "";
            lerror1.Content = "";
            if (checkMajors(TBNameMajors.Text.Length))
                BTNAdd.IsEnabled = true;
            else
                BTNAdd.IsEnabled = false;
        }
    } 

     
    
}
