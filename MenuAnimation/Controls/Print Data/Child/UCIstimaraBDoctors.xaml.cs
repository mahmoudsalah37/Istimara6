using System.Windows.Controls;
using Astmara6.Data;
using System.Linq;
using Astmara6.Classes;
using System;
using System.Threading.Tasks;
using Astmara6.Model;
using System.Windows;
using System.Collections.Generic;

namespace Astmara6.Controls.Print_Data.Child
{
    /// <summary>
    /// Interaction logic for UCIstimaraBDoctors.xaml
    /// </summary>
    public partial class UCIstimaraBDoctors : UserControl
    {
        private readonly CollegeContext context = new CollegeContext();
        private ComboboxItem item;
        private List<AstmaraB> astmaraBs;
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
                                select p).Where(t => t.Section.TypeOfSection == x&&t.WorkHour.AcademicOrVirtual==true).ToList();
            foreach (var teacher in listTeachers)
            {

                item = new ComboboxItem();
                item.Text = teacher.Name;
                item.Value = teacher.Id;
                CBDoctorsName.Items.Add(item);

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
            astmaraBs = (from p in context.AstmaraBs
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual==true & t.Teacher.Section.TypeOfSection==x).OrderBy(t => t.IdDoctor).ToList();
            DGAstmraBDoc.ItemsSource = astmaraBs;

        }
        public UCIstimaraBDoctors()
        {
            InitializeComponent();
            getDepartments();
            context.AstmaraBs.RemoveRange(context.AstmaraBs);
            context.SaveChanges();
            insertdata();
            getDoctors();
            loadData();
        }
        public void insertdata()
        {
            var teachers = (from p in context.AstmaraAs
                            select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).ToList();
            foreach(var teacher in teachers)
            {
                context.AstmaraBs.Add(new AstmaraB
                {
                    IdDoctor=teacher.IdTeacher,
                    IdWorkHours=teacher.Teacher.IdWorkHours,
                    Subject=teacher.Subject.Name,
                    IdLevel=teacher.IdLevel,
                    Paper=teacher.NumOfPaper,
                    Virtial=teacher.NumberOfVirtual,
                    Experment=teacher.NumberOfExprement,
                    Sum=teacher.SumOfSubject,
                });
               
                context.SaveChanges();
            }
            totalHours();
            com();
            context.SaveChanges();
        }
     

        private void totalHours()
        {
            var subjectTeachers = (from p in context.AstmaraAs
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).ToList();
            foreach(var subjectTeacher in subjectTeachers)
            {
                subjectTeacher.SumOfSubject = (subjectTeacher.NumOfPaper + subjectTeacher.NumberOfSuperVision);
            }
           
            var teachers = (from p in context.AstmaraBs
                            select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).Select(t => new { t.IdDoctor }).Distinct().ToList();
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
                             select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).Select(t => new { t.IdDoctor, t.Teacher.WorkHour.Quorum, t.Total }).Distinct().ToList();
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
                Print.data2ExelIstmaraBD(this, DGAstmraBDoc, astmaraBs, semester, year);

            });

        }

        private void CBDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBDoctorsName.Items.Clear();
            getDoctors();
            loadData();
            
        }

        private void CBDoctorsName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
                string x = "null";
                string y = "null";
                ComboboxItem it = CBDepartments.SelectedItem as ComboboxItem;
                ComboboxItem ad = CBDoctorsName.SelectedItem as ComboboxItem;
                if (it != null || ad != null)
                {
                    x = it.Text;
                    y = ad.Text;


                }
                var astmaraBs = (from p in context.AstmaraBs
                                 select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true & t.Teacher.Name == y & t.Teacher.Section.TypeOfSection == x).ToList();
                DGAstmraBDoc.ItemsSource = astmaraBs;
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");
            }
        }
    }
}
