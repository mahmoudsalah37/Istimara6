using System.Windows;
using System.Linq;

using System.Windows.Controls;
using System.Collections.Generic;
using System;
using Astmara6.Data;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for work_hours.xaml
    /// </summary>
    public partial class UCWorkHours : UserControl
    {
        private string STRNamePage;
        private readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        private readonly CollegeContext context = new CollegeContext();

        private void loadData()
        {
            List<WorkHour> workHours = (from p in context.WorkHours
                            select p).ToList();
            DGWorkHours.ItemsSource = workHours;
        }
        public UCWorkHours()
        {
            InitializeComponent();
            loadData();
        }


        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCCourses());
            STRNamePage = "المواد";
            Form.ChFormName(STRNamePage);
        }

        private void BTNNext_Click(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCDoctors());
            STRNamePage = "الدكاترة";
            Form.ChFormName(STRNamePage);
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            context.WorkHours.Add(new WorkHour()
                {
                    Rank = TBRank.Text,
                    Quorum = int.Parse(TBLegalMonument.Text),
                    AcademicOrVirtual = RBPaper.IsChecked,
                }
            );
            context.SaveChanges();
            loadData();
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WorkHour workHour = DGWorkHours.SelectedItem as WorkHour;
                WorkHour row = (from p in context.WorkHours
                                where p.Id == workHour.Id
                                select p).Single();
                row.Rank = workHour.Rank;
                row.Quorum = workHour.Quorum;
                row.AcademicOrVirtual = workHour.AcademicOrVirtual;
                context.SaveChanges();
                loadData();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
        }

        private void BTNRemove_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                WorkHour workHour = DGWorkHours.SelectedItem as WorkHour;
                WorkHour row = (from p in context.WorkHours
                                where p.Id == workHour.Id
                                select p).Single();
                context.WorkHours.Remove(row);
                context.SaveChanges();
                loadData();
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }

        }

        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                context.WorkHours.RemoveRange(context.WorkHours);
                context.SaveChanges();
                loadData();
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
        }
    }
}
