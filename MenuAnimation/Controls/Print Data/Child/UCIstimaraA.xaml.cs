using System.Windows.Controls;
using Astmara6.Data;
using System.Linq;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Astmara6.Classes;

namespace Astmara6.Controls.Print_Data.Child
{
    /// <summary>
    /// Interaction logic for UCIstimaraA.xaml
    /// </summary>
    public partial class UCIstimaraA : UserControl
    {
        
        private readonly CollegeContext context = new CollegeContext();

        public void loadData()
        {
           var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).ToList();
            DGAstmaraA.ItemsSource = subjectTeachers;

        }

        public UCIstimaraA()
        {
            
            InitializeComponent();
            teacherProced();
            loadData();
        }


        private void teacherProced()
        {
            int? indSuperVision=0;
            int? indExperimentOrVersial=0;
            var listLevelAndSubject= (from p in context.SubjectTeachers
                     select p).Select(t=> new {t.Level,t.Subject }).Distinct().ToList();
            foreach (var levelAndSubject in listLevelAndSubject)
            {
                int? superVision = (from p in context.SubjectTeachers
                                    select p).Where(t => t.Level.Id == levelAndSubject.Level.Id && t.Subject.Id == levelAndSubject.Subject.Id).First().TotalSuperVision;
                int countDoc = (from p in context.SubjectTeachers
                                select p).Where(t => t.Level.Id == levelAndSubject.Level.Id && t.Teacher.WorkHour.AcademicOrVirtual == true && t.Subject.Id == levelAndSubject.Subject.Id).ToList().Count();
                int countAss = (from p in context.SubjectTeachers
                                select p).Where(t => t.Level.Id == levelAndSubject.Level.Id && t.Teacher.WorkHour.AcademicOrVirtual == false && t.Subject.Id == levelAndSubject.Subject.Id).ToList().Count();
                
                bool checkDivtionDoc = false;
                bool checkDivtionAss = false;
                int? DivtionDoc = 0;
                int? DivtionASS = 0;
                if (countDoc > 0)
                {
                    indSuperVision = superVision / countDoc;
                    if (superVision % countDoc != 0)
                    {
                        checkDivtionDoc = true;
                        DivtionDoc = superVision % countDoc;
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
                               select p).Where(t => t.Level.Id == levelAndSubject.Level.Id && t.Subject.Id == levelAndSubject.Subject.Id).ToList();

                foreach (SubjectTeacher update in updates)
                {
                    int countDo = (from p in context.SubjectTeachers
                                    select p).Where(t => t.Level.Id == update.Level.Id && t.Teacher.WorkHour.AcademicOrVirtual == true && t.Subject.Id == update.Subject.Id).ToList().Count();
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
                            if (i != 1)
                            {
                                u.NumOfPaper = 0;
                                context.SaveChanges();
                            }
                        }
                    }
                    if (update.Teacher.WorkHour.AcademicOrVirtual == true)
                    {
                        if (checkDivtionDoc == true)
                        {
                            update.NumberOfSuperVision = indSuperVision+DivtionDoc;
                            checkDivtionDoc = false;
                        }
                        else
                        update.NumberOfSuperVision = indSuperVision;
                    }
                    else
                    {
                        if (checkDivtionAss == true)
                        {
                            if (update.TotalExperment == 0)
                            {
                                update.NumberOfVirtual = indExperimentOrVersial+DivtionASS;
                                update.NumberOfExprement = 0;
                            }
                            else
                            {
                                update.NumberOfExprement = indExperimentOrVersial+DivtionASS;
                                update.NumberOfVirtual = 0;
                            }
                            checkDivtionAss = false;
                        }
                        else
                        {
                            if (update.TotalExperment == 0)
                            {
                                update.NumberOfVirtual = indExperimentOrVersial;
                                update.NumberOfExprement = 0;
                            }
                            else
                            {
                                update.NumberOfExprement = indExperimentOrVersial;
                                update.NumberOfVirtual = 0;
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
            Print.data2Exel(DGAstmaraA);
        }



    }
    
}
