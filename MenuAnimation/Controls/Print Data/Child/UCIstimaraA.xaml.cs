using System.Windows.Controls;
using Astmara6.Data;
using Astmara6.Model;
using MenuAnimado1.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;

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
            foreach(var levelAndSubject in listLevelAndSubject)
            {
                int? superVision = (from p in context.SubjectTeachers
                                   select p).Where(t => t.Level.Id == levelAndSubject.Level.Id &&  t.Subject.Id == levelAndSubject.Subject.Id).First().TotalSuperVision;
                int countDoc = (from p in context.SubjectTeachers
                                select p).Where(t => t.Level.Id == levelAndSubject.Level.Id && t.Teacher.WorkHour.AcademicOrVirtual == true && t.Subject.Id == levelAndSubject.Subject.Id).ToList().Count();
                int countAss = (from p in context.SubjectTeachers
                                select p).Where(t => t.Level.Id == levelAndSubject.Level.Id && t.Teacher.WorkHour.AcademicOrVirtual == false && t.Subject.Id == levelAndSubject.Subject.Id).ToList().Count();
                 var updates = (from p in context.SubjectTeachers
                              select p).Where(t => t.Level.Id == levelAndSubject.Level.Id && t.Subject.Id == levelAndSubject.Subject.Id).ToList();
                
                if (countDoc >0)
                  indSuperVision = superVision / countDoc;
                if(countAss>0)
                  indExperimentOrVersial = superVision / countAss;

                foreach (SubjectTeacher update in updates)
                {
                    if(update.Teacher.WorkHour.AcademicOrVirtual == true)
                    {
                        update.NumberOfSuperVision = indSuperVision;
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
                context.SaveChanges();
            }
        }
    }
    
}
