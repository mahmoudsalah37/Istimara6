using System.Windows.Controls;
using Astmara6.Data;
using System.Linq;
using Astmara6.Classes;

namespace Astmara6.Controls.Print_Data.Child
{
    /// <summary>
    /// Interaction logic for UCIstimaraBAssistants.xaml
    /// </summary>
    public partial class UCIstimaraBAssistants : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        public void loadData()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false).ToList();
            DGAstmraBDoc.ItemsSource = subjectTeachers;

        }
        public UCIstimaraBAssistants()
        {
            InitializeComponent();
            totalHours();
            loadData();
        }
        private void totalHours()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false).ToList();
            foreach (var subjectTeacher in subjectTeachers)
            {
                subjectTeacher.SumOfSubject = (subjectTeacher.NumberOfVirtual + subjectTeacher.NumberOfExprement);
            }

            var teachers = (from p in context.SubjectTeachers
                            select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false).Select(t => new { t.Teacher.Name, t.TotalOfHour }).Distinct().ToList();
            foreach (var teacher in teachers)
            {
                int? totalHour = (from p in context.SubjectTeachers
                                  select p).Where(t => t.Teacher.Name == teacher.Name.ToString()).Sum(t => t.SumOfSubject);
                var subjectsofTeashers = (from p in context.SubjectTeachers
                                          select p).Where(t => t.Teacher.Name == teacher.Name.ToString()).ToList();
                foreach (var subjectsofTeasher in subjectsofTeashers)
                {
                    subjectsofTeasher.TotalOfHour = totalHour;
                }
            }
            context.SaveChanges();
        }
        public void complitehour()
        {
            var teachers = (from p in context.SubjectTeachers
                            select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).Select(t => new { t.Teacher.Name, t.Teacher.Id, t.Teacher.WorkHour.Quorum, t.TotalOfHour }).Distinct().ToList();
            foreach (var teacher in teachers)
            {
                int? totalHour = (from p in context.SubjectTeachers
                                  select p).Where(t => t.Teacher.Name == teacher.Name.ToString()).Sum(t => t.SumOfSubject);
                int? NumOfQuorum = teacher.Quorum;
                if (totalHour >= NumOfQuorum)
                {
                    var subjectsofTeashers = (from p in context.SubjectTeachers
                                              select p).Where(t => t.Teacher.Name == teacher.Name.ToString()).ToList();
                    foreach (var subjectsofTeasher in subjectsofTeashers)
                    {
                        subjectsofTeasher.TotalOfHour = totalHour;
                    }
                }
                else
                {
                    var subjectsofTeashers = (from p in context.SubjectTeachers
                                              select p).ToList();
                    int? shortage = teacher.Quorum - totalHour;
                    if (shortage <= 10)
                    {
                        context.SubjectTeachers.Add(new SubjectTeacher()
                        {
                            IdTeacher = teacher.Id,
                            IdSubject = 5,
                            SumOfSubject = shortage,
                        }
                        );
                        complitehour();
                    }
                    else if (shortage <= 20 & shortage > 10)
                    {
                        int? shortage2 = 0;
                        if (shortage % 2 == 0)
                            shortage2 = (shortage / 2);
                        else
                            shortage2 = (shortage / 2) + 1;


                        for (int i = 5; i < 7; i++)
                        {
                            context.SubjectTeachers.Add(new SubjectTeacher()
                            {
                                IdTeacher = teacher.Id,
                                IdSubject = i,
                                SumOfSubject = shortage2,
                            }
                           );

                        }
                        complitehour();
                    }
                    else if (shortage > 20)
                    {
                        int? shortage2 = 0;
                        if (shortage % 3 == 0)
                            shortage2 = (shortage / 3);
                        else
                            shortage2 = (shortage / 3) + 1;
                        for (int i = 5; i <= 7; i++)
                        {
                            context.SubjectTeachers.Add(new SubjectTeacher()
                            {
                                IdTeacher = teacher.Id,
                                IdSubject = i,
                                SumOfSubject = shortage2,
                            }
                           );
                        }
                        //complitehour();
                    }

                }
            }
            context.SaveChanges();
        }

        private void BtnExportData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Print.data2Exel(DGAstmraBDoc);
        }
    }
}
