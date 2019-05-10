using Data.Context;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Data.Entities;
using System;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for department.xaml
    /// </summary>
    public partial class UCDepartment : UserControl
    {
        public void loadData()
        {
            CollegeContext cd = new CollegeContext();

            var sections = (from p in cd.Sections
                          select p).ToList();

            DGDepartmentView.ItemsSource = sections;
        }
        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

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
                CollegeContext dataContext = new CollegeContext();
                Section DepartmentRow = DGDepartmentView.SelectedItem as Section;

                Section departments = (from p in dataContext.Sections
                                where p.Id == DepartmentRow.Id
                                select p).Single();
                departments.TypeOfSection = DepartmentRow.TypeOfSection;
                dataContext.SaveChanges();
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
           
                CollegeContext cd = new CollegeContext();
                Section DepartmentRow = DGDepartmentView.SelectedItem as Section;

                Section sections = (from p in cd.Sections
                                where p.Id == DepartmentRow.Id
                                select p).Single();
                cd.Sections.Remove(sections);
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

                cd.Sections.RemoveRange(cd.Sections);

                cd.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح كل البيانات");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            CollegeContext db = new CollegeContext();
            db.Sections.Add(new Section()
            {
                TypeOfSection = TBNameDepartment.Text
            });
            db.SaveChanges();
            loadData();
            MessageBox.Show("تم حفظ العملية بنجاح");
            TBNameDepartment.Text = "";
        }

        private void DGDepartmentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
