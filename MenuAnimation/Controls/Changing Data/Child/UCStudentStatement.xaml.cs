using Astmara6.Data;
using MenuAnimado1.Controls;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using Astmara6.Model;

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
                CBDepartment.Items.Add(item);
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
                CBSubject.Items.Add(item);
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
                CBLevel.Items.Add(item);
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
                CBBranch.Items.Add(item);
            }
        }

        public UCStudentStatement()
        {
            InitializeComponent();
            getDepartments();
            getBranches();
            getLevels();
            getSubjects();
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
            ComboboxItem CBIDepartment = CBDepartment.SelectedItem as ComboboxItem;
            int department = (int)CBIDepartment.Value;
            ComboboxItem CBIBranch = CBDepartment.SelectedItem as ComboboxItem;
            int branch = (int)CBIBranch.Value;
            ComboboxItem CBILevel = CBDepartment.SelectedItem as ComboboxItem;
            int level = (int)CBILevel.Value;
            ComboboxItem CBISubject = CBDepartment.SelectedItem as ComboboxItem;
            int subject = (int)CBISubject.Value;
            int numberStudents = int.Parse(CBNumberStudents.Text);
            context.StudentStatments.Add(new StudentStatment() {
                IdBranch = branch,
                IdLevel = level,
                IdSubject = subject,
                NumberOfStudent = numberStudents
            });

        }
    }
}
