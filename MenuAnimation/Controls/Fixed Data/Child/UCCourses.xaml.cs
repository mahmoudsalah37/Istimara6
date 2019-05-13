using MenuAnimado1.Controls;
using System.Windows;
using System.Linq;

using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;
using Astmara6.Data;
using System.Windows.Input;
using Astmara6.Classes;
using System.Collections.Generic;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for courses.xaml
    /// </summary>
    public partial class UCCourses : UserControl
    {
        private int? TotalHour;
        private int? Expremente;
        private int? Virtuale;
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
            Form.gridShow.Children.Add(new UCWorkHours());
            STRNamePage = "ساعات العمل";
            Form.ChFormName(STRNamePage);
        }
        
        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                codee = TBCode.Text;
                Academee = int.Parse(TBPaper.Text);
                int explenght = TBExprement.Text.Length;
                if (explenght < 1)
                {
                    Expremente = 0;

                }
                else
                {
                    Expremente = int.Parse(TBExprement.Text);
                }
                int virlength = TBVirtual.Text.Length;
                if (virlength < 1)
                {
                    Virtuale = 0;
                }
                else
                {
                    Virtuale = int.Parse(TBVirtual.Text);
                }
                context.Subjects.Add(new Subject()
                {
                    Code = codee,
                    Name = TBNameCourse.Text,
                    Academic = Academee,

                    Exprement = Expremente,
                    Virtual = Virtuale,
                    TotalHours = Academee + Expremente + Virtuale

                });
                context.SaveChanges();
                loadData();
                MessageBox.Show("تم حفظ العملية بنجاح");
                TBCode.Text = "";
                TBNameCourse.Text = "";
                TBPaper.Text = "";
                TBVirtual.Text = "";
                TBExprement.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

            }

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
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                                         "تـأكد من ارتباط البيانات بمعومات اخري");
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
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                        "تـأكد من ارتباط البيانات بمعومات اخري");
            }
        }
        public Boolean checkcode(int length)
        {
            Boolean result = true;
            List<Subject> precoucode = (from p in context.Subjects
                                        where p.Code == TBCode.Text
                                        select p).ToList();

            if (precoucode.Count > 0)
            {
                LErrorCode.Content = "لقد ادخلت هذا من قبل ";
                result = false;
            }
            else
            {
                result = true;
            }
            if (length < 1) {
                LErrorCode.Content = "لم تدخل شئ";
                result= false;
            }
            return result;
        }
        public Boolean checkName(int length)
        {
            Boolean result = true;
            List<Subject> precoucode = (from p in context.Subjects
                                        where p.Name == TBNameCourse.Text
                                        select p).ToList();

            if (precoucode.Count > 0)
            {
                LErrorCOurse.Content = "لقد ادخلت هذا من قبل ";
                result = false;
            }
            else
            {
               result= true;
            }
            if (length < 1)
            {
                LErrorCOurse.Content = "لم تكتب شئ ";
                result = false;
            }
            return result;
        }
        private void TBCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            LErrorCode.Content = "";
            LErrorCOurse.Content = "";
            cheakover();
        
        }
    
        private void TBNameCourse_TextChanged(object sender, TextChangedEventArgs e)
        {
            LErrorCode.Content = "";
            LErrorCOurse.Content = "";
            cheakover();
       
        }
        public void cheakover ( )
        {
            int codelength = TBCode.Text.Length;
            int namelenght = TBNameCourse.Text.Length;
            int paperlength = TBPaper.Text.Length;
            int virlength = TBVirtual.Text.Length;
            int explength = TBExprement.Text.Length;
            Boolean checkCode = checkcode(codelength);
            Boolean checkname = checkName(namelenght);
            Boolean cheakpaper = true;
            Boolean checkVirtual = true;
            Boolean checkExp = true;
            if (paperlength < 1)
            {
                cheakpaper = false;

            }
            else
            {
                cheakpaper = true;
            }
            if (virlength < 1)
            {
                checkVirtual = true;

            }
            else
            {
                checkVirtual = false;
            }
            if (explength < 1)
            {
                checkExp = true;
            }
            else
            {
                checkExp = false;
            }
            if (cheakpaper == false)
            {
                LErrorPaper.Content = "لم تدخل شئ";

            }
            else
            {
                LErrorPaper.Content = "";
            }
            if (checkVirtual & checkExp )
            {
                BTNAdd.IsEnabled = false;
                LErrorVirtual.Content = "لم تدخل شئ";
                LErrorExprement.Content = "لم تدخل شئ";
            }
            else if (((checkVirtual || checkExp)&cheakpaper)&(checkname&checkCode))
            {
                LErrorPaper.Content = "";
                LErrorVirtual.Content = "";
                LErrorExprement.Content = "";
                BTNAdd.IsEnabled = true;


            }
            else if (checkVirtual==false & checkExp==false)
            {
                BTNAdd.IsEnabled = false;
                LErrorVirtual.Content = "افرغ احدهما";
                LErrorExprement.Content = "افرغ احدهما";
            }

        }
        private void TBPaper_TextChanged(object sender, TextChangedEventArgs e)
        {
            cheakover();
          

        }

        private void TBVirtual_TextChanged(object sender, TextChangedEventArgs e)
        {
            cheakover();
            
            

        }

        private void TBExprement_TextChanged(object sender, TextChangedEventArgs e)
        {
            cheakover();
           
        }
    }
}
