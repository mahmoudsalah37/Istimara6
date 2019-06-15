using System.Windows.Controls;
using Astmara6.Data;
using System.Linq;
using Astmara6.Classes;
using Astmara6.Model;

namespace Astmara6.Controls.Print_Data.Child
{
    /// <summary>
    /// Interaction logic for UCIstimaraBAssistants.xaml
    /// </summary>
    public partial class UCIstimaraBAssistants : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        private ComboboxItem item;
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
        public void loadData()
        {
            string x = "null";
            ComboboxItem it = CBDepartments.SelectedItem as ComboboxItem;
            if (it != null)
            {
                x = it.Text;
            }
            var astmaraBs = (from p in context.AstmaraBs
                             select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false& t.Teacher.Section.TypeOfSection == x).OrderBy(t=>t.IdDoctor).ToList();
            DGAstmraBDoc.ItemsSource = astmaraBs;

        }
        public UCIstimaraBAssistants()
        {
            InitializeComponent();
            getDepartments();
            context.AstmaraBs.RemoveRange(context.AstmaraBs);
            context.SaveChanges();
            insertdata();
            loadData();
        }
        public void insertdata()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false).ToList();
            foreach (var subjectTeacher in subjectTeachers)
            {
                subjectTeacher.SumOfSubject = (subjectTeacher.NumberOfExprement + subjectTeacher.NumberOfVirtual);
            }
            var teachers = (from p in context.SubjectTeachers
                            select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false).ToList();
            foreach (var teacher in teachers)
            {
                context.AstmaraBs.Add(new AstmaraB
                {
                    IdDoctor = teacher.IdTeacher,
                    IdWorkHours = teacher.Teacher.IdWorkHours,
                    Subject = teacher.Subject.Name,
                    IdLevel = teacher.IdLevel,
                    Paper = teacher.NumOfPaper,
                    Virtial = teacher.NumberOfVirtual,
                    Experment = teacher.NumberOfExprement,
                    Sum = teacher.SumOfSubject,
                });

                context.SaveChanges();
            }
            totalHours();
            com();
            context.SaveChanges();
        }


        private void totalHours()
        {
            

            var teachers = (from p in context.AstmaraBs
                            select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false).Select(t => new { t.IdDoctor }).Distinct().ToList();
            foreach (var teacher in teachers)
            {

                int? totalHour = (from p in context.AstmaraBs
                                  select p).Where(t => t.IdDoctor == teacher.IdDoctor).Sum(t => t.Sum);
                var astmara8s = (from p in context.AstmaraBs
                                 select p).Where(t => t.IdDoctor == teacher.IdDoctor).ToList();
                foreach (var astmara8 in astmara8s)
                {
                    astmara8.Total = totalHour;
                }
                context.SaveChanges();
            }

        }
        public void com()
        {
            var astmara8s = (from p in context.AstmaraBs
                             select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false).Select(t => new { t.IdDoctor, t.Teacher.WorkHour.Quorum, t.Total }).Distinct().ToList();
            foreach (var astmaa8 in astmara8s)
            {
                int? totalHour = astmaa8.Total;
                int? NumOfQuorum = astmaa8.Quorum;
                if (totalHour >= NumOfQuorum)
                {
                    var teachercoms = (from p in context.AstmaraBs
                                       select p).Where(t => t.IdDoctor == astmaa8.IdDoctor).ToList();
                    foreach (var teachercom in teachercoms)
                    {
                        teachercom.Total = totalHour;
                        context.SaveChanges();
                    }
                }
                else
                {
                    var teacher = (from p in context.AstmaraBs
                                   select p).Where(t => t.IdDoctor == astmaa8.IdDoctor).ToList();
                    int? shortage = NumOfQuorum - totalHour;
                    if (shortage <= 10)
                    {
                        context.AstmaraBs.Add(new AstmaraB()
                        {
                            IdDoctor = astmaa8.IdDoctor,
                            Subject = "انشطة ثقافية",
                            Sum = shortage,
                        }
                        );

                    }
                    else if (shortage <= 20 & shortage > 10)
                    {
                        int? shortage2 = 0;
                        if (shortage % 2 == 0)
                            shortage2 = (shortage / 2);
                        else
                            shortage2 = (shortage / 2) + 1;



                        context.AstmaraBs.Add(new AstmaraB()
                        {
                            IdDoctor = astmaa8.IdDoctor,
                            Subject = "انشطة ثقافية",
                            Sum = shortage2,
                        }
                       );
                        context.AstmaraBs.Add(new AstmaraB()
                        {
                            IdDoctor = astmaa8.IdDoctor,
                            Subject = "انشطة اجتماعية",
                            Sum = shortage2,
                        });
                    }
                    else if (shortage > 20)
                    {
                        int? shortage2 = 0;
                        if (shortage % 3 == 0)
                            shortage2 = (shortage / 3);
                        else
                            shortage2 = (shortage / 3) + 1;

                        context.AstmaraBs.Add(new AstmaraB()
                        {
                            IdDoctor = astmaa8.IdDoctor,
                            Subject = "انشطة ثقافية",
                            Sum = shortage2,
                        }
                       );
                        context.AstmaraBs.Add(new AstmaraB()
                        {
                            IdDoctor = astmaa8.IdDoctor,
                            Subject = "انشطة اجتماعية",
                            Sum = shortage2,
                        });
                        context.AstmaraBs.Add(new AstmaraB()
                        {
                            IdDoctor = astmaa8.IdDoctor,
                            Subject = "انشطة رياضية",
                            Sum = shortage2,
                        });
                    }
                    context.SaveChanges();
                }
            }
            totalHours();
        }

        private void BtnExportData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string semester = TransferData.Semester;
                string year = TransferData.Year;
                Print.data2Exel(this, DGAstmraBDoc,3,semester,year);
            });
        }

        private void CBDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData();
        }
    }
}
