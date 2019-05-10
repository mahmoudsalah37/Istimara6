using Data.Context;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Data.Entities;
using System;
namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for majors.xaml
    /// </summary>
    public partial class UCMajors : UserControl
    {
        public void loadData()
        {
            CollegeContext cd = new CollegeContext();

            var sections = (from p in cd.Sections
                            select p).ToList();
            var branchs = (from p in cd.Branches
                           select p).ToList();

            DGMajorsView.ItemsSource = sections;
            DGMajorsView.ItemsSource = branchs;
        }
        public void TakeDataFromCombo()
        {
            CollegeContext cd = new CollegeContext();
            Section SectionCB = CBNameDepartment.SelectedItem as Section;
            Section sections = (from p in cd.Sections
                                where p.Id == SectionCB.Id
                                select p).Single();

            cd.Branches.Add(new Branch()
            {
                IdSection = SectionCB.Id,
                Name = TBNameMajors.Text
            });
            cd.SaveChanges();
            loadData();
            MessageBox.Show("تم حفظ البيانات بنجاح ");


        }
        public void loadDataCombo()
        {
            CollegeContext cd = new CollegeContext();

            var sections = (from p in cd.Sections
                            select p).ToList();

            CBNameDepartment.ItemsSource = sections;
        }
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

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
                CollegeContext dataContext = new CollegeContext();
                Branch DepartmentRow = DGMajorsView.SelectedItem as Branch;


                Branch departments = (from p in dataContext.Branches
                                      where p.Id == DepartmentRow.Id
                                      select p).Single();
                departments.Name = DepartmentRow.Name;
                dataContext.SaveChanges();
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

                CollegeContext cd = new CollegeContext();
                Branch BranchRow = DGMajorsView.SelectedItem as Branch;

                Branch branchs = (from p in cd.Branches
                                  where p.Id == BranchRow.Id
                                  select p).Single();
                cd.Branches.Remove(branchs);
                cd.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح العنصر بنجاح");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }

        }
        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CollegeContext cd = new CollegeContext();

                cd.Branches.RemoveRange(cd.Branches);

                cd.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح كل البيانات");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
        }

    } 

     
    
}
