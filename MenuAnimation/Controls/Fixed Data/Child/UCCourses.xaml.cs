using Data.Context;
using MenuAnimado1.Controls;
using System.Windows;
using System.Linq;

using System.Windows.Controls;
using Data.Entities;
using System;
using System.Text.RegularExpressions;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for courses.xaml
    /// </summary>
    public partial class UCCourses : UserControl
    {
        private int? TotalHour;
        private int? Expremente =null;
        private int? Virtuale=null;
        private string codee;
        private int? Academee;
        private string STRNamePage;
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        private readonly CollegeContext context = new CollegeContext();

        private void loadData()
        {

            var subjects = (from p in context.Subjects
                            select p).ToList();

            DGCoursesView.ItemsSource = subjects;
        }
        
        public UCCourses()
        {

            InitializeComponent();
            loadData();
          
            
        }


        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCMajors());
            STRNamePage = "الشعب";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCChangingData());
            STRNamePage = "ٍساعات العمل";
            Form.ChFormName(STRNamePage);
        }
        
        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
             codee = TBCode.Text;
              Academee = int.Parse(TBPaper.Text);
            
            context.Subjects.Add(new Subject()
            {
                Code = codee,
                Name = TBNameCourse.Text,
                Academic = Academee,

                Exprement = Expremente,
                Virtual = Virtuale,
                TotalHours = TotalHour
                
            });
            context.SaveChanges();
            loadData();
            MessageBox.Show("تم حفظ العملية بنجاح");
            
        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TBVirtual_TextChanged(object sender, TextChangedEventArgs e)
        {
             if (!String.IsNullOrEmpty(TBVirtual.Text))
            {
                TBExprement.IsReadOnly = true;
              TotalHour  =
              int.Parse(TBPaper.Text)
              + int.Parse(TBVirtual.Text);
                Virtuale = int.Parse(TBVirtual.Text);
                
            }
            else
            {
                TBExprement.IsReadOnly = false;

            }

        }

        private void TBExprement_TextChanged(object sender, TextChangedEventArgs e)
        {
           


            if (!String.IsNullOrEmpty(TBExprement.Text))
            {
                TBVirtual.IsReadOnly = true;

                TotalHour =
                 int.Parse(TBPaper.Text)
                + int.Parse(TBExprement.Text);
                Expremente = int.Parse(TBExprement.Text);
                TotalHour = Expremente + Academee;


            }
            else
            {
                TBVirtual.IsReadOnly = false;

            }
        }

       

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
           //can't write but numbers for TBexprement
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            
        }

        private void NumberValidationTextBox1(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //can't write but numbers for TBVirtual

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

     

        private void NumberValidationTextBox2(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //can't write but numbers for TBPaper
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TBPaper_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Subject CoursesRow = DGCoursesView.SelectedItem as Subject;
                Subject subjects = (from p in context.Subjects
                                    where p.Id == CoursesRow.Id
                                    select p).Single();
                subjects.Name = CoursesRow.Name;
                subjects.Code = CoursesRow.Code;
                subjects.Academic = CoursesRow.Academic;
                subjects.Virtual = CoursesRow.Virtual;
                subjects.Exprement = CoursesRow.Exprement;
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

                Subject SubjectRow = DGCoursesView.SelectedItem as Subject;

                Subject subjects = (from p in context.Subjects
                                    where p.Id == SubjectRow.Id
                                    select p).Single();
                context.Subjects.Remove(subjects);
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
                context.Subjects.RemoveRange(context.Subjects);

                context.SaveChanges();
                loadData();
                MessageBox.Show("تم مسح كل البيانات");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
        }
    }
}
