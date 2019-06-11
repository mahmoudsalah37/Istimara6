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
    }
}
