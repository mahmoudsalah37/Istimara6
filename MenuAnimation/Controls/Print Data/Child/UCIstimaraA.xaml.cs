using System.Windows.Controls;
using Astmara6.Data;
using System.Linq;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Astmara6.Classes;
using Astmara6.Model;
using System.Collections.Generic;

namespace Astmara6.Controls.Print_Data.Child
{
    /// <summary>
    /// Interaction logic for UCIstimaraA.xaml
    /// </summary>
    public partial class UCIstimaraA : UserControl
    {
        
        private readonly CollegeContext context = new CollegeContext();
        private ComboboxItem item;
        private List<AstmaraA> subjectTeachers;
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

             subjectTeachers = (from p in context.AstmaraAs
                                   select p).Where(t=>t.Branch.Section.TypeOfSection==x).ToList();
            DGAstmaraA.ItemsSource = subjectTeachers;

        }

        public UCIstimaraA()
        {
            
            InitializeComponent();
            context.AstmaraAs.RemoveRange(context.AstmaraAs);
            context.SaveChanges();
            getDepartments();
            teacherProced();
            loadData();
        }


        private void teacherProced()
        {
            
            int? indExperimentOrVersial=0;
            var listLevelAndSubject= (from p in context.SubjectTeachers
                     select p).Select(t=> new {t.Level,t.Subject,t.Branch.Section.TypeOfSection }).Distinct().ToList();
            foreach (var levelAndSubject in listLevelAndSubject)
            {   int? indSuperVision=4;
                int? superVision = (from p in context.SubjectTeachers
                                    select p).Where(t => t.Level.Id == levelAndSubject.Level.Id & t.Branch.Section.TypeOfSection==levelAndSubject.TypeOfSection & t.Subject.Id == levelAndSubject.Subject.Id).First().TotalSuperVision;
                int countDoc = (from p in context.SubjectTeachers
                                select p).Where(t => t.Level.Id == levelAndSubject.Level.Id & t.Branch.Section.TypeOfSection == levelAndSubject.TypeOfSection & t.Teacher.WorkHour.AcademicOrVirtual == true && t.Subject.Id == levelAndSubject.Subject.Id).ToList().Count();

                int countAss = (from p in context.SubjectTeachers
                                select p).Where(t => t.Level.Id == levelAndSubject.Level.Id & t.Branch.Section.TypeOfSection == levelAndSubject.TypeOfSection & t.Teacher.WorkHour.AcademicOrVirtual == false && t.Subject.Id == levelAndSubject.Subject.Id).ToList().Count();
                
                bool checkDivtionDoc = false;
                bool checkDivtionAss = false;
                int? DivtionDoc = 0;
                int? DivtionASS = 0;
                if (countDoc > 0)
                {
                    int? numofsec = (from p in context.SubjectTeachers
                                     select p).Where(t => t.Level.Id == levelAndSubject.Level.Id & t.Branch.Section.TypeOfSection == levelAndSubject.TypeOfSection & t.Teacher.WorkHour.AcademicOrVirtual == true && t.Subject.Id == levelAndSubject.Subject.Id).First().NumberOfSections;

                
                    if (numofsec>=2)
                    {
                        checkDivtionDoc = true;
                    }
                    else
                    {
                        indSuperVision = superVision;
                    }
                    if (superVision == 0)
                    {
                        DivtionDoc = 1;
                    }
                    else
                    {
                        DivtionDoc = superVision / 4;
                        if (superVision % 4 != 0)
                        {
                            DivtionDoc++;
                        }
                    }
                }
                if (countAss > 0)
                {
                    indExperimentOrVersial = superVision / countAss;
                    if (superVision % countAss != 0)
                    {
                        checkDivtionAss = true;
                        DivtionASS = superVision % countAss;

                    }

                }

                var updates = (from p in context.SubjectTeachers
                               select p).Where(t => t.Level.Id == levelAndSubject.Level.Id & t.Branch.Section.TypeOfSection == levelAndSubject.TypeOfSection & t.Subject.Id == levelAndSubject.Subject.Id).ToList();

                foreach (SubjectTeacher update in updates)
                {
                    int countDo = (from p in context.SubjectTeachers
                                    select p).Where(t => t.Level.Id == update.Level.Id & t.Branch.Section.TypeOfSection == levelAndSubject.TypeOfSection & t.Teacher.WorkHour.AcademicOrVirtual == true && t.Subject.Id == update.Subject.Id).ToList().Count();
                    bool checkdoc= false;
                    if (update.Teacher.WorkHour.AcademicOrVirtual == true)
                        checkdoc = true;
                    if (countDo > 1 & checkdoc)
                    {
                        int i = 0;
                        var us = updates.Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).ToList();
                        foreach (var u in us)
                        {
                            i++;
                            if (countDo <= 3)
                            {
                                int? totnumofpaper = 0;
                                int? totsuper = 0;
                                int? numofpaper = u.NumOfPaper / countDo;
                                int? super = u.NumberOfSuperVision / countDo;
                                if (u.NumOfPaper % countDo != 0 || u.NumberOfSuperVision % countDo!=0)
                                {
                                    totnumofpaper = u.NumOfPaper%countDo;
                                    totsuper = u.NumberOfSuperVision % countDo;
                                }
                                if (i == 1)
                                {
                                    u.NumOfPaper = totnumofpaper + numofpaper;
                                    u.NumberOfSuperVision = totsuper + super;
                                
                                }
                                else
                                {
                                    u.NumOfPaper = numofpaper;
                                    u.NumberOfSuperVision = super;
                                
                                }
                                context.SaveChanges();
                            }
                            //if (i != 1)
                            //{
                            //    u.NumOfPaper = 0;
                            //    context.SaveChanges();
                            //}
                        }
                    }
                    if (update.Teacher.WorkHour.AcademicOrVirtual == true)
                    {
                        if (checkDivtionDoc == true)
                        {
                            int? totalsupvision = update.TotalSuperVision;
                           for(int i = 0; i < DivtionDoc; i++)
                           {
                                
                                if (totalsupvision < 4)
                                {
                                    indSuperVision = totalsupvision;
                                }
                                if (i == 0)
                                {

                                    context.AstmaraAs.Add(new AstmaraA()
                                    {
                                        IdBranch = update.IdBranch,
                                        IdLevel = update.IdLevel,
                                        IdSubject = update.IdSubject,
                                        IdTeacher = update.IdTeacher,
                                        NumberOfSections = update.NumberOfSections,
                                        TotalPaper = update.TotalPaper,
                                        TotalExperment = update.TotalExperment,
                                        TotalVirtual = update.TotalVirtual,
                                        TotalSuperVision = update.TotalSuperVision,
                                        NumberOfExprement = update.NumberOfExprement,
                                        NumberOfVirtual = update.NumberOfVirtual,
                                        NumOfPaper = update.NumOfPaper,
                                        NumberOfSuperVision = update.NumberOfSuperVision,
                                        NumOfStudent = update.NumOfStudent,
                                        SumOfSubject = update.NumberOfSuperVision + update.NumOfPaper,
                                    });
                                    totalHours();
                                    totalsupvision = totalsupvision - update.NumberOfSuperVision;
                                }
                                else
                                {

                                    var sortedDoc = (from p in context.Teachers
                                                     select p).Where(t => t.idSection == update.Branch.IdSection).OrderBy(t => t.TotalHours);
                                    int? NewDocId = sortedDoc.First().Id;
                                    context.AstmaraAs.Add(new AstmaraA()
                                    {
                                        IdBranch = update.IdBranch,
                                        IdLevel = update.IdLevel,
                                        IdSubject = update.IdSubject,
                                        IdTeacher = NewDocId,
                                        NumberOfSections = update.NumberOfSections,
                                        TotalPaper = 0,
                                        TotalExperment = 0,
                                        TotalVirtual = 0,
                                        TotalSuperVision = 0,
                                        NumberOfExprement = 0,
                                        NumberOfVirtual = 0,
                                        NumOfPaper =0,
                                        NumberOfSuperVision = indSuperVision,
                                        NumOfStudent = update.NumOfStudent,
                                        SumOfSubject = indSuperVision
                                    });
                                    totalHours();
                                    totalsupvision = totalsupvision - indSuperVision;
                                }

                                if (totalsupvision == 0)
                                    break;


                                
                           }
                        }
                        else
                        {
                            context.AstmaraAs.Add(new AstmaraA()
                            {
                                IdBranch = update.IdBranch,
                                IdLevel = update.IdLevel,
                                IdSubject = update.IdSubject,
                                IdTeacher = update.IdTeacher,
                                NumberOfSections = update.NumberOfSections,
                                TotalPaper = update.TotalPaper,
                                TotalExperment = update.TotalExperment,
                                TotalVirtual = update.TotalVirtual,
                                TotalSuperVision = update.TotalSuperVision,
                                NumberOfExprement = update.NumberOfExprement,
                                NumberOfVirtual = update.NumberOfVirtual,
                                NumOfPaper = update.NumOfPaper,
                                NumberOfSuperVision = indSuperVision,
                                NumOfStudent = update.NumOfStudent,
                                SumOfSubject = indSuperVision + update.NumOfPaper,
                            });
                            totalHours();

                        }

                    }
                    else
                    {
                        if (checkDivtionAss == true)
                        {
                            if (update.TotalExperment == 0)
                            {
                                context.AstmaraAs.Add(new AstmaraA()
                                {
                                    IdBranch = update.IdBranch,
                                    IdLevel = update.IdLevel,
                                    IdSubject = update.IdSubject,
                                    IdTeacher = update.IdTeacher,
                                    NumberOfSections = update.NumberOfSections,
                                    TotalPaper = update.TotalPaper,
                                    TotalExperment = update.TotalExperment,
                                    TotalVirtual = update.TotalVirtual,
                                    TotalSuperVision = update.TotalSuperVision,
                                    NumberOfExprement = 0,
                                    NumberOfVirtual = indExperimentOrVersial + DivtionASS,
                                    NumOfPaper = update.NumOfPaper,
                                    NumberOfSuperVision = 0,
                                    NumOfStudent = update.NumOfStudent,
                                    SumOfSubject = indSuperVision,
                                });
                                totalHours();
                              
                            }
                            else
                            {
                                context.AstmaraAs.Add(new AstmaraA()
                                {
                                    IdBranch = update.IdBranch,
                                    IdLevel = update.IdLevel,
                                    IdSubject = update.IdSubject,
                                    IdTeacher = update.IdTeacher,
                                    NumberOfSections = update.NumberOfSections,
                                    TotalPaper = update.TotalPaper,
                                    TotalExperment = update.TotalExperment,
                                    TotalVirtual = update.TotalVirtual,
                                    TotalSuperVision = update.TotalSuperVision,
                                    NumberOfExprement = indExperimentOrVersial + DivtionASS,
                                    NumberOfVirtual = 0,
                                    NumOfPaper = update.NumOfPaper,
                                    NumberOfSuperVision = 0,
                                    NumOfStudent = update.NumOfStudent,
                                    SumOfSubject = indSuperVision,
                                });
                                
                            }
                            checkDivtionAss = false;
                        }
                        else
                        {
                            if (update.TotalExperment == 0)
                            {
                                context.AstmaraAs.Add(new AstmaraA()
                                {
                                    IdBranch = update.IdBranch,
                                    IdLevel = update.IdLevel,
                                    IdSubject = update.IdSubject,
                                    IdTeacher = update.IdTeacher,
                                    NumberOfSections = update.NumberOfSections,
                                    TotalPaper = update.TotalPaper,
                                    TotalExperment = update.TotalExperment,
                                    TotalVirtual = update.TotalVirtual,
                                    TotalSuperVision = update.TotalSuperVision,
                                    NumberOfExprement = 0,
                                    NumberOfVirtual = indExperimentOrVersial,
                                    NumOfPaper = update.NumOfPaper,
                                    NumberOfSuperVision = 0,
                                    NumOfStudent = update.NumOfStudent,
                                    SumOfSubject = indSuperVision,
                                });
                                totalHours();
    
                            }
                            else
                            {
                                context.AstmaraAs.Add(new AstmaraA()
                                {
                                    IdBranch = update.IdBranch,
                                    IdLevel = update.IdLevel,
                                    IdSubject = update.IdSubject,
                                    IdTeacher = update.IdTeacher,
                                    NumberOfSections = update.NumberOfSections,
                                    TotalPaper = update.TotalPaper,
                                    TotalExperment = update.TotalExperment,
                                    TotalVirtual = update.TotalVirtual,
                                    TotalSuperVision = update.TotalSuperVision,
                                    NumberOfExprement = indExperimentOrVersial,
                                    NumberOfVirtual = 0,
                                    NumOfPaper = update.NumOfPaper,
                                    NumberOfSuperVision = 0,
                                    NumOfStudent = update.NumOfStudent,
                                    SumOfSubject = indSuperVision,
                                });
                            }
                        }

                      
                    }
                }
                context.SaveChanges();
            }
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).ToList();
            foreach (var subjectTeacher in subjectTeachers)
            {
                subjectTeacher.SumOfSubject = (subjectTeacher.NumOfPaper + subjectTeacher.NumberOfSuperVision);
                context.SaveChanges();
            }
        }
        public void totalhoursCalc()
        {

        }
        
        private void BtnExportData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string semester = TransferData.Semester;
                string year = TransferData.Year;
                Print.data2ExelIstmaraA(this, DGAstmaraA, subjectTeachers, semester, year);

            });
        }
        private void totalHours()
        {
            var subjectTeachers = (from p in context.AstmaraAs
                                   select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true).ToList();
            foreach (var subjectTeacher in subjectTeachers)
            {
                subjectTeacher.SumOfSubject = (subjectTeacher.NumOfPaper + subjectTeacher.NumberOfSuperVision);
            }

            var teachers = (from p in context.Teachers
                            select p).Where(t => t.WorkHour.AcademicOrVirtual == true).ToList();
            foreach (var teacher in teachers)
            {

                int? totalHour = (from p in context.AstmaraAs
                                  select p).Where(t => t.IdTeacher==teacher.Id).Sum(t => t.SumOfSubject);
                if (totalHour == null)
                {
                    totalHour = 0;
                }
                var teachs = (from p in context.Teachers
                                 select p).Where(t =>t.Id==teacher.Id ).ToList();
                foreach (var teach in teachs)
                {
                    teach.TotalHours= totalHour;
                }
                context.SaveChanges();
            }

        }

        private void CBDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData();
        }
    }
    
}
