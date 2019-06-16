
using Astmara6.Data;
using Astmara6.Model;
using MenuAnimado1.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for El5ta.xaml
    /// </summary>
    public partial class UCPlan : UserControl
    {
        private string STRNamePage;
        private ComboboxItem item;
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        private readonly CollegeContext context = new CollegeContext();
        private void getDepartments()
        {
            var listSection = (from p in context.Sections
                               select p).ToList();
            foreach (var section in listSection)
            {
                item = new ComboboxItem();
                item.Text = section.TypeOfSection;
                item.Value = section.Id;
                CBDepartments.Items.Add(item);
            }
        }

        private void getBranches()
        {
            string x = "null";
            ComboboxItem it = CBDepartments.SelectedItem as ComboboxItem;
            if (it != null)
            {
                CBBRanches.Items.Clear();
                x = it.Text;
            }
            var listBranches = (from p in context.Branches
                                 select p).Where(t=>t.Section.TypeOfSection==x).ToList();
            foreach (var branch in listBranches)
            {
                item = new ComboboxItem();
                item.Text = branch.Name;
                item.Value = branch.Id;
                CBBRanches.Items.Add(item);
            }
        }
       
        private void getDoctors()
        {
            string x = "null";
            ComboboxItem it = CBDepartments.SelectedItem as ComboboxItem;
            if (it != null)
            {
                CBDoctorsName.Items.Clear();
                x = it.Text;
            }
            var listTeachers = (from p in context.Teachers
                                select p).Where(t=>t.Section.TypeOfSection==x).ToList();
            foreach (var teacher in listTeachers)
            {
                
                    item = new ComboboxItem();
                    item.Text = teacher.Name;
                    item.Value = teacher.Id;
                    CBDoctorsName.Items.Add(item);
                
            }
        }
        private void getSubjects()
        {
            var studentStatments = (from p in context.StudentStatments
                                 select  p.Subject ).Distinct().ToList();
            foreach (var subjectTeacher in studentStatments)
            {
                item = new ComboboxItem();
                item.Text = subjectTeacher.Name;
                item.Value = subjectTeacher.Id;
                CBSubjects.Items.Add(item);
                item = new ComboboxItem();
                item.Text = subjectTeacher.Code;
                item.Value = subjectTeacher.Id;
                CBCode.Items.Add(item);
            }
        }
        private void getLevels()
        {
            var listLevels = (from p in context.Levels
                                select p).ToList();
            foreach (var level in listLevels)
            {
                item = new ComboboxItem();
                item.Text = level.Name;
                item.Value = level.Id;
                CBLevels.Items.Add(item);
            }
        }
        public void getNumOFstudent()
        {

        }
        public void loadData()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                          select p).ToList();
            DGPlanShow.ItemsSource = subjectTeachers;
            
        }
        public UCPlan()
        {
            InitializeComponent();
            getBranches();
            getDoctors();
            getDepartments();
            getSubjects();
            getLevels();
            loadData();
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCStudentStatement());
            STRNamePage = "بيان الطلاب";
            Form.ChFormName(STRNamePage);
        }

        private void btnnext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCSendData());
            STRNamePage = "الاستمارات و الطباعة";
            Form.ChFormName(STRNamePage);
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboboxItem CBIDepartment = CBBRanches.SelectedItem as ComboboxItem;
                int department = (int)CBIDepartment.Value;
                ComboboxItem CBIDoctor = CBDoctorsName.SelectedItem as ComboboxItem;
                int doctor = (int)CBIDoctor.Value;
                //ComboboxItem CBIAssistant = CBAssistantsName.SelectedItem as ComboboxItem;
                //int assistant = (int)CBIAssistant.Value;
                ComboboxItem CBISubject = CBSubjects.SelectedItem as ComboboxItem;
                ComboboxItem CBILevel = CBLevels.SelectedItem as ComboboxItem;
                int subject = (int)CBISubject.Value;
                int level = (int)CBILevel.Value;
                int ?numberOfStudent = context.StudentStatments.Where(t => t.IdSubject == subject && t.IdLevel == level).Single().NumberOfStudent;
                int ?numSection = numberOfStudent / 25;
                if (numberOfStudent % 25 != 0)
                {
                    numSection = numSection+1;
                }
                int? totalp= context.Subjects.Where(t => t.Id == subject).Single().Academic;
                int? totalv = context.Subjects.Where(t => t.Id == subject).Single().Virtual;
                int? totalE = context.Subjects.Where(t => t.Id == subject).Single().Exprement;
                int? supervision = totalE + totalv;
                bool? isTeacher = context.Teachers.Where(t => t.Id == doctor).Single().WorkHour.AcademicOrVirtual;
                int? totalPdoc = 0;
                if (isTeacher == true)
                    totalPdoc = totalp;
                context.SubjectTeachers.Add(new SubjectTeacher()
                {
                    IdBranch = department,
                    IdTeacher = doctor,
                    IdLevel = level,
                    IdSubject = subject,
                    NumberOfSections = numSection,
                    TotalPaper=totalp,
                    TotalVirtual=totalv*numSection,
                    TotalExperment=totalE*numSection,
                    TotalSuperVision=numSection*supervision,
                     NumOfPaper = totalPdoc,
                     NumOfStudent=numberOfStudent,
                }

                );
                context.SaveChanges();
                loadData();
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
                SubjectTeacher subjectTeacher = DGPlanShow.SelectedItem as SubjectTeacher;
                SubjectTeacher subject = (from p in context.SubjectTeachers
                                          where p.Id == subjectTeacher.Id
                                          select p).Single();
                //subject.NumberOfStudent = subjectTeacher.NumberOfStudent;
                context.SaveChanges();
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

            };
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("سوف يتم مسح هذاالعنصر؟", "تأكيد الحذف ", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    SubjectTeacher subjectTeacher = DGPlanShow.SelectedItem as SubjectTeacher;
                    SubjectTeacher subject = (from p in context.SubjectTeachers
                                              where p.Id == subjectTeacher.Id
                                              select p).Single();
                    context.SubjectTeachers.Remove(subject);
                    context.SaveChanges();
                    loadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

                }
            }
            else
            {
                MessageBox.Show("لاتقلق لم تمسح اي بيانات");
            }
        }

        private void BTNDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("سوف يتم مسح كل البيانات؟", "تأكيد الحذف ", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                { 
                context.SubjectTeachers.RemoveRange(context.SubjectTeachers);
            context.SaveChanges();
            loadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

                }
            }
            else
            {
                MessageBox.Show("لاتقلق لم تمسح اي بيانات");
            }

        }
        private bool checkBranch()
        {
            ComboboxItem item = CBBRanches.SelectedItem as ComboboxItem;
            int id;
            if (item != null)
                id = (int)item.Value;
            else
            {
                LErrorBranch.Content = "برجاء الاختيار من القائمه";
                return false;
            }
            List<Branch> listBranches = (from p in context.Branches
                                       where p.Id == id
                                       select p).ToList();
            if (listBranches.Count >0)
            {
                LErrorBranch.Content = "";
                return true;
            }
            else
            {
                LErrorBranch.Content = "برجاء الاختيارمن القائمه";
                return false;
            }
        }

        private bool checkDoctor()
        {
            ComboboxItem item = CBDoctorsName.SelectedItem as ComboboxItem;
            int id;
            if (item != null)
                id = (int)item.Value;
            else
            {
                LErrorDoctor.Content = "برجاء الاختيار من القائمه";
                return false;
            }
            List<Teacher> listDoctor = (from p in context.Teachers
                                         where p.Id == id
                                         select p).ToList();
            if (listDoctor.Count >0)
            {
                LErrorDoctor.Content = "";
                return true;
            }
            else
            {
                LErrorDoctor.Content = "برجاء الاختيار من القائمه";
                return false;
            }
        }

        private bool checkSubject()
        {
            ComboboxItem item = CBSubjects.SelectedItem as ComboboxItem;
            int id;
            if (item != null)
                id = (int)item.Value;
            else
            {
                LErrorSubject.Content = "برجاء الاختيار من القائمه";
                return false;
            }
            List<Subject> listSubjects = (from p in context.Subjects
                                         where p.Id == id
                                         select p).ToList();
            if (listSubjects.Count >0)
            {
                LErrorSubject.Content = "";
                return true;
            }
            else
            {
                LErrorSubject.Content = "برجاء الاختيارمن القائمه";
                return false;
            }
        }

        private bool checkLevel()
        {
            ComboboxItem item = CBLevels.SelectedItem as ComboboxItem;
            int id;
            if (item != null)
                id = (int)item.Value;
            else
            {
                LErrorLevel.Content = "برجاء الاختيار من القائمه";
                return false;
            }
            List<Level> ListLevels = (from p in context.Levels
                                         where p.Id == id
                                         select p).ToList();
            if (ListLevels.Count >0)
            {
                LErrorLevel.Content = "";
                return true;
            }
            else
            {
                LErrorLevel.Content = "برجاء الاختيارمن القائمه";
                return false;
            }
        }
        public void checkover()
        {
            LErrorLevel.Content = "";
            LErrorBranch.Content = "";
            LErrorDoctor.Content = "";
            LErrorSubject.Content = "";
            if (checkLevel() && checkDoctor()&& checkBranch() && checkSubject())
                btnAdd.IsEnabled = true;
            else
            {
                btnAdd.IsEnabled = false;
                checkDoctor();
                checkSubject();
                checkLevel();
            }
        }
        private void CBDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkover();
        }

        private void CBDoctorsName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkover();
        }

        private void CBLevels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkover();
        }

        private void CBDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getBranches();
            getDoctors();
        }
     



        private void CBCode_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void CBSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboboxItem CBIRank = CBSubjects.SelectedItem as ComboboxItem;
            if (CBCode != null)
            {
                int value = (int)CBIRank.Value;
                CBCode.SelectedValue = value;
            }
        }

        private void CBCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboboxItem CBIRank = CBCode.SelectedItem as ComboboxItem;
            if (CBCode != null)
            {
                int value = (int)CBIRank.Value;
                CBSubjects.SelectedValue = value;
            }
        }
    }
}
