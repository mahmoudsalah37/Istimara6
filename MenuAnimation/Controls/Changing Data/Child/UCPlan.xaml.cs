
using Astmara6.Data;
using Astmara6.Model;
using MenuAnimado1.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using Astmara6.Classes;
using System.Windows.Input;

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
            STRNamePage = "بيانات الطلاب";
            Form.ChFormName(STRNamePage);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            CheckInput.numberOnly(e);
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
               
                ComboboxItem CBISubject = CBSubjects.SelectedItem as ComboboxItem;
                ComboboxItem CBILevel = CBLevels.SelectedItem as ComboboxItem;
                int subject = (int)CBISubject.Value;
                int level = (int)CBILevel.Value;
                int ?numberOfStudent = (from p in context.StudentStatments
                                        select p).Where(t => t.IdSubject == subject && t.IdLevel == level).First().NumberOfStudent;
                int div = 0;
                if (TBSEC.Text == "") {
                     div = 25;
                }
                else
                {
                    div = int.Parse(TBSEC.Text);
                }
                int ?numSection = numberOfStudent / div;
                if (numberOfStudent % div != 0)
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
                    TotalPaper = totalp,
                    TotalVirtual = totalv * numSection,
                    TotalExperment = totalE * numSection,
                    TotalSuperVision = numSection * supervision,
                    NumOfPaper = totalPdoc,
                    NumberOfSuperVision = int.Parse(TBSUPER.Text),
                    NumOfStudent = numberOfStudent,
                    SumOfSubject = int.Parse(TBSUPER.Text) + totalPdoc,
                }

                );
                context.SaveChanges();
                MessageBox.Show("تمت الاضافة بنجاح");
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("تأكد من وجود المادة في بيان الطلاب ووجودعدد كافي من الطلاب ");

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
                context.SaveChanges();
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

            };
        }

        public void superhours()
        {
            try
            {
                ComboboxItem it = CBDepartments.SelectedItem as ComboboxItem;
                string department = it.Text;
                ComboboxItem i2 = CBLevels.SelectedItem as ComboboxItem;
                string level = i2.Text;
                ComboboxItem i3 = CBSubjects.SelectedItem as ComboboxItem;
                string subject = i3.Text;
                int? numberOfStudent = (from p in context.StudentStatments
                                        select p).Where(t => t.Subject.Name == subject & t.Branch.Section.TypeOfSection == department & t.Level.Name == level).First().NumberOfStudent;
                int div = 0;
                if (TBSEC.Text == "")
                {
                    div = 25;
                }
                else
                {
                    div = int.Parse(TBSEC.Text);
                }
                int? numSection = numberOfStudent / div;
                if (numberOfStudent % div != 0)
                {
                    numSection = numSection + 1;
                }
                int? totalv = context.Subjects.Where(t => t.Name == subject).Single().Virtual;
                int? totalE = context.Subjects.Where(t => t.Name == subject).Single().Exprement;
                int? supervision = totalE + totalv;
                var subjectdoc = (from p in context.SubjectTeachers
                                  select p).Where(t => t.Subject.Name == subject & t.Branch.Section.TypeOfSection == department & t.Level.Name == level).ToList();
                int? count = subjectdoc.Count();
                int? totalsuper = supervision * numSection;
                if (count == null)
                {
                    LALnumber.Content = totalsuper.ToString();
                }
                else
                {
                    foreach (var ss in subjectdoc)
                    {
                        totalsuper = totalsuper - ss.NumberOfSuperVision;
                    }
                    LALnumber.Content = totalsuper.ToString();

                }
            }
            catch(Exception ex)
            {
            }
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

        private void CBSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            superhours();
            string name = null;
            ComboboxItem item = CBSubjects.SelectedItem as ComboboxItem;
            if (item != null)
                name = item.Text;

            if (name != null)
            {
                var listBranhes = (from p in context.Subjects
                                   select p).Where(t => t.Name == name).ToList();

                foreach (var branch in listBranhes)
                {
                    TBCode.Text = branch.Code;
                }

            }
            checkover();
        }

        private void CBLevels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            superhours();
            checkover();
        }

        private void CBDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getBranches();
            getDoctors();
            superhours();
        }
        private void TBCode_TextChanged(object sender, TextChangedEventArgs e)
        {

            string code = TBCode.Text;
            ComboboxItem item = CBSubjects.SelectedItem as ComboboxItem;
            if (item == null)
            {

                if (code != null)
                {
                    CBSubjects.Items.Clear();

                    var listBranhes = (from p in context.Subjects
                                       select p).Where(t => t.Code == code).ToList();
                    int num = listBranhes.Count();
                    if (num == 0)
                    {
                        getSubjects();
                    }
                    else
                    {
                        foreach (var branch in listBranhes)
                        {
                            item = new ComboboxItem();
                            item.Text = branch.Name;
                            item.Value = branch.Id;
                            CBSubjects.Items.Add(item);
                        }
                    }
                }
                else
                {
                    getSubjects();
                }
            }

        }

        private void TBSEC_TextChanged(object sender, TextChangedEventArgs e)
        {
            superhours();
        }
    }
}
