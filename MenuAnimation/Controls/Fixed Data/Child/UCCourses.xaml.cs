using MenuAnimado1.Controls;
using System.Windows;
using System.Linq;

using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;
using Astmara6.Data;
using System.Windows.Input;
using Astmara6.Classes;

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


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            CheckInput.numberOnly(e);
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
