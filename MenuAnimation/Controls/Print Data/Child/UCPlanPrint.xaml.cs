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
            var subjectTeachers = (from p in context.SubjectTeachers
                                   select p).Where(t=>t.Teacher.Section.TypeOfSection==x).ToList();
            DGPlanShow.ItemsSource = subjectTeachers;

        }
       
        public UCPlanPrint()
        {
            InitializeComponent();
            getDepartments();
            loadData();
        }

        private void BtnExportData_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Print.data2Exel(this, DGPlanShow);
            });
        }

        private void CBDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData();
        }
    }
}
