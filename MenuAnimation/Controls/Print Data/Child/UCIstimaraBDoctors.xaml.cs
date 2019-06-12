using System.Windows.Controls;
using Astmara6.Data;
using System.Linq;

namespace Astmara6.Controls.Print_Data.Child
{
    /// <summary>
    /// Interaction logic for UCIstimaraBDoctors.xaml
    /// </summary>
    public partial class UCIstimaraBDoctors : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        public void loadData()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual==true).ToList();
            DGAstmraBDoc.ItemsSource = subjectTeachers;

        }
        public UCIstimaraBDoctors()
        {
            InitializeComponent();
            totalHours();
            loadData();
        }

        private void totalHours()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).ToList();
            foreach(var subjectTeacher in subjectTeachers)
            {
                subjectTeacher.SumOfSubject = (subjectTeacher.NumOfPaper + subjectTeacher.NumberOfSuperVision);
            }

            var teachers = (from p in context.SubjectTeachers
                            select p).Where(t=>t.Teacher.WorkHour.AcademicOrVirtual==true).Select(t => new { t.Teacher.Name,t.TotalOfHour}).Distinct().ToList();
            foreach(var teacher in teachers)
            {
                int? totalHour= (from p in context.SubjectTeachers
                                        select p).Where(t=>t.Teacher.Name== teacher.Name.ToString()).Sum(t => t.SumOfSubject);
                var subjectsofTeashers = (from p in context.SubjectTeachers
                                         select p).Where(t => t.Teacher.Name == teacher.Name.ToString()).ToList();
                foreach(var subjectsofTeasher in subjectsofTeashers)
                {
                    subjectsofTeasher.TotalOfHour = totalHour;
                }
            }
            context.SaveChanges();
        }
    }
}
