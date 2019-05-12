using System.Windows;
using System.Linq;
using Astmara6.Classes;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using Astmara6.Data;
using System.Text.RegularExpressions;
using System.Windows.Input;

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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
           CheckInput.numberOnly(e);
        }
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
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                        "تـأكد من ارتباط البيانات بمعومات اخري");
            }

        }

        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                context.WorkHours.RemoveRange(context.WorkHours);
                context.SaveChanges();
                loadData();
            }
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                        "تـأكد من ارتباط البيانات بمعومات اخري");
            }
        }
        public Boolean checkName(int length)
        {
            Boolean result1 = true;
            List<WorkHour> precoucode = (from p in context.WorkHours
                                        where p.Rank== TBRank.Text
                                        select p).ToList();
            if (length < 1)
            {
                lerrorcou_code.Content = "لم تكتب شئ ";
                result1 = false;
            }
            if (precoucode.Count > 0)
            {
                lerrorcou_code.Content = "لقد ادخلت هذا من قبل ";
                result1 = false;
            }
            else
            {
                result1 = true;
            }

            return result1;
        }
   
        private void TBRank_TextChanged(object sender, TextChangedEventArgs e)
        {
            lerrorcou_code.Content = "";
            lerrorcou_name.Content = "";
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;
            Boolean r1 = checkName(length);
         
            if (r1)
            {
                BTNAdd.IsEnabled = true;
            }
            else
                BTNAdd.IsEnabled = false;
        }

        private void TBLegalMonument_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;
            if (length < 1)
            {
                BTNAdd.IsEnabled = false;
            }
        }
    }
}
