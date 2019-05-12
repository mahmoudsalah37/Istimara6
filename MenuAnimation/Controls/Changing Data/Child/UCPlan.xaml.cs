
using Astmara6.Data;
using Astmara6.Model;
using MenuAnimado1.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

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

        private void getDepartmwents()
        {
            var listBranches = (from p in context.Branches
                                 select p).ToList();
            foreach (var branch in listBranches)
            {
                item = new ComboboxItem();
                item.Text = branch.Name;
                item.Value = branch.Id;
                CBDepartment.Items.Add(item);
            }
        }
        private void getDoctors()
        {
            var listTeachers = (from p in context.Teachers
                                select p).ToList();
            foreach (var teacher in listTeachers)
            {
                if (teacher.WorkHour.AcademicOrVirtual == true)
                {
                    item = new ComboboxItem();
                    item.Text = teacher.Name;
                    item.Value = teacher.Id;
                    CBDoctorsName.Items.Add(item);
                }
            }
        }
        private void getAssitants()
        {
            var listTeachers = (from p in context.Teachers
                                select p).ToList();
            foreach (var teacher in listTeachers)
            {
                if (teacher.WorkHour.AcademicOrVirtual == false)
                {
                    item = new ComboboxItem();
                    item.Text = teacher.Name;
                    item.Value = teacher.Id;
                    CBAssistantsName.Items.Add(item);
                }
            }
        }
        private void getSubjects()
        {
            var listSubjects = (from p in context.Subjects
                                select p).ToList();
            foreach (var subject in listSubjects)
            {
                    item = new ComboboxItem();
                    item.Text = subject.Name;
                    item.Value = subject.Id;
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
        public void loadData()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                          select p).ToList();
            DGPlanShow.ItemsSource = subjectTeachers;
            foreach(var subjectTeacher in subjectTeachers)
            {
                
            }
            
        }
        public UCPlan()
        {
            InitializeComponent();
            getDepartmwents();
            getDoctors();
            //getAssitants();
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
            Form.gridShow.Children.Add(new UCDataPrint());
            STRNamePage = "الاستمارات و الطباعة";
            Form.ChFormName(STRNamePage);
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ComboboxItem CBIDepartment = CBDepartment.SelectedItem as ComboboxItem;
            int department = (int)CBIDepartment.Value;
            ComboboxItem CBIDoctor = CBDoctorsName.SelectedItem as ComboboxItem;
            int doctor = (int)CBIDoctor.Value;
            //ComboboxItem CBIAssistant = CBAssistantsName.SelectedItem as ComboboxItem;
            //int assistant = (int)CBIAssistant.Value;
            ComboboxItem CBISubject = CBSubjects.SelectedItem as ComboboxItem;
            ComboboxItem CBILevel = CBLevels.SelectedItem as ComboboxItem;
            int subject = (int)CBISubject.Value;
            int level = (int)CBILevel.Value;
            context.SubjectTeachers.Add(new SubjectTeacher()
            {
                IdBranch = department,
                IdTeacher = doctor,
                IdLevel = level,
                IdSubject = subject
            }
            );
            context.SaveChanges();
            loadData();
        }
    }
}
