using Astmara6.Data;
using Astmara6.Model;
using MenuAnimado1.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using Astmara6.Classes;

namespace Astmara6.Controls.Print_Data.Child
{
    /// <summary>
    /// Interaction logic for UCPlan.xaml
    /// </summary>
    public partial class UCPlanPrint : UserControl
    {
        public void loadData()
        {
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).ToList();
            DGPlanShow.ItemsSource = subjectTeachers;

        }
        private readonly CollegeContext context = new CollegeContext();
        public UCPlanPrint()
        {
            InitializeComponent();
            loadData();
        }

        private void BtnExportData_Click(object sender, RoutedEventArgs e)
        {
            Print.data2Exel(DGPlanShow);
        }
    }
}
