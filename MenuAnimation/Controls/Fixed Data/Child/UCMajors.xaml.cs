using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System;
using Astmara6.Data;

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
            MessageBox.Show("تم حفظ البيانات بنجاح ");


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
            Form.gridShow.Children.Add(new UCWorkHours());
            STRNamePage = "النصاب القانوني";
            Form.ChFormName(STRNamePage);
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            TakeDataFromCombo();

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


                MessageBox.Show("تم تعديل الصف بنجاح");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }

        private void BTNRemove_Click_1(object sender, RoutedEventArgs e)
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

                MessageBox.Show("تم مسح العنصر بنجاح");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }

        }
        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                context.Branches.RemoveRange(context.Branches);

                context.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح كل البيانات");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
        }

    } 

     
    
}
