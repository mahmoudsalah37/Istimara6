using Astmara6.Data;
using MenuAnimado1.Controls;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using Astmara6.Model;
using System;
using System.Collections.Generic;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for student_inf.xaml
    /// </summary>
    public partial class UCStudentStatement : UserControl
    {
        private string STRNamePage;
        private ComboboxItem item;
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        private readonly CollegeContext context = new CollegeContext();

        private void getDepartments()
        {
            var listBranches = (from p in context.Branches
                                select p).ToList();
            foreach (var branch in listBranches)
            {
                item = new ComboboxItem();
                item.Text = branch.Name;
                item.Value = branch.Id;
                CBDepartments.Items.Add(item);
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

        private void getBranches()
        {
            var listBranhes = (from p in context.Branches
                              select p).ToList();
            foreach (var branch in listBranhes)
            {
                item = new ComboboxItem();
                item.Text = branch.Name;
                item.Value = branch.Id;
                CBBranches.Items.Add(item);
            }
        }

        public UCStudentStatement()
        {
            InitializeComponent();
            getDepartments();
            getBranches();
            getLevels();
            getSubjects();
            loadData();
        }

        private void loadData()
        {
            var studentStatement = (from p in context.StudentStatments
                                    select p).ToList();
            DGStudentStatments.ItemsSource = studentStatement;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCPlan());
            STRNamePage = "الخطة";
            Form.ChFormName(STRNamePage);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCChangingData());
            STRNamePage = "البيانات المتغيرة";
            Form.ChFormName(STRNamePage);

        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            ComboboxItem CBIDepartment = CBDepartments.SelectedItem as ComboboxItem;
            int department = (int)CBIDepartment.Value;
            ComboboxItem CBIBranch = CBBranches.SelectedItem as ComboboxItem;
            int branch = (int)CBIBranch.Value;
            ComboboxItem CBILevel = CBLevels.SelectedItem as ComboboxItem;
            int level = (int)CBILevel.Value;
            ComboboxItem CBISubject = CBSubjects.SelectedItem as ComboboxItem;
            int subject = (int)CBISubject.Value;
            int numberStudents = int.Parse(TBNumberStudents.Text);
            context.StudentStatments.Add(new StudentStatment() {
                IdBranch = branch,
                IdLevel = level,
                IdSubject = subject,
                NumberOfStudent = numberStudents
            });
            context.SaveChanges();
            loadData();
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            StudentStatment studentStatment = DGStudentStatments.SelectedItem as StudentStatment;
            StudentStatment student = (from p in context.StudentStatments
                                where p.Id == studentStatment.Id
                                select p).Single();
            student.NumberOfStudent = studentStatment.NumberOfStudent;
            context.SaveChanges();
            loadData();
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            StudentStatment studentStatment = DGStudentStatments.SelectedItem as StudentStatment;
            StudentStatment student = (from p in context.StudentStatments
                                       where p.Id == studentStatment.Id
                                       select p).Single();
            context.StudentStatments.Remove(student);
            context.SaveChanges();
            loadData();
        }

        private void BTNDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            context.StudentStatments.RemoveRange(context.StudentStatments);
            context.SaveChanges();
            loadData();
        }

        private bool checkBranch()
        {
            ComboboxItem item = CBDepartments.SelectedItem as ComboboxItem;
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
            if (listBranches.Count > 0)
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

        private bool checkSection()
        {
            ComboboxItem item = CBDepartments.SelectedItem as ComboboxItem;
            int id;
            if (item != null)
                id = (int)item.Value;
            else
            {
                LErrorSection.Content = "برجاء الاختيار من القائمه";
                return false;
            }
            List<Section> listSections = (from p in context.Sections
                                         where p.Id == id
                                         select p).ToList();
            if (listSections.Count > 0)
            {
                LErrorSection.Content = "";
                return true;
            }
            else
            {
                LErrorSection.Content = "برجاء الاختيارمن القائمه";
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
            if (listSubjects.Count > 0)
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
            if (ListLevels.Count > 0)
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

        private bool checkNumberOfStudents()
        {
            if (TBNumberStudents.Text.Length > 0)
            {
                LErrorNumberStudent.Content = "";
                return true;
            }
            else
            {
                LErrorNumberStudent.Content = "ادخل بيانات";
                return false;
            }
        }

        private void CBDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LErrorLevel.Content = "";
            LErrorBranch.Content = "";
            LErrorNumberStudent.Content = "";
            LErrorSection.Content = "";
            LErrorSubject.Content = "";
            if (checkLevel() && checkBranch() && checkSubject()&& checkNumberOfStudents())
                BTNAdd.IsEnabled = true;
            else
            {
                BTNAdd.IsEnabled = false;
                checkBranch();
                checkNumberOfStudents();
                checkSubject();
                checkLevel();
            }
        }

        private void CBBranches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LErrorLevel.Content = "";
            LErrorBranch.Content = "";
            LErrorNumberStudent.Content = "";
            LErrorSection.Content = "";
            LErrorSubject.Content = "";
            if (checkLevel() && checkSection() && checkSubject() && checkNumberOfStudents())
                BTNAdd.IsEnabled = true;
            else
            {
                BTNAdd.IsEnabled = false;
                checkSection();
                checkSubject();
                checkNumberOfStudents();
                checkLevel();
            }
        }

        private void CBLevels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LErrorLevel.Content = "";
            LErrorBranch.Content = "";
            LErrorNumberStudent.Content = "";
            LErrorSection.Content = "";
            LErrorSubject.Content = "";
            if (checkBranch() && checkSection() && checkSubject()&& checkNumberOfStudents())
                BTNAdd.IsEnabled = true;
            else
            {
                BTNAdd.IsEnabled = false;
                checkSection();
                checkSubject();
                checkNumberOfStudents();
                checkBranch();
            }
        }

        private void CBSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LErrorLevel.Content = "";
            LErrorBranch.Content = "";
            LErrorNumberStudent.Content = "";
            LErrorSection.Content = "";
            LErrorSubject.Content = "";
            if (checkBranch() && checkSection() && checkLevel()&& checkNumberOfStudents())
                BTNAdd.IsEnabled = true;
            else
            {
                BTNAdd.IsEnabled = false;
                checkSection();
                checkNumberOfStudents();
                checkLevel();
                checkBranch();
            }
        }

        private void TBNumberStudents_TextChanged(object sender, TextChangedEventArgs e)
        {
            LErrorLevel.Content = "";
            LErrorBranch.Content = "";
            LErrorNumberStudent.Content = "";
            LErrorSection.Content = "";
            LErrorSubject.Content = "";
            if (checkBranch() && checkSection() && checkLevel()&&checkSubject() && checkNumberOfStudents())
                BTNAdd.IsEnabled = true;
            else
            {
                BTNAdd.IsEnabled = false;
                checkSection();
                checkSubject();
                checkNumberOfStudents();
                checkLevel();
                checkBranch();
            }
        }
    }
}
