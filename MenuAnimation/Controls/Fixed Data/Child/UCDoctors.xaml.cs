using MenuAnimado1.Controls;
using System.Data;
using System.Linq;
﻿using Astmara6;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Astmara6.Model;
using Astmara6.Data;
using System;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for docinfo.xaml
    /// </summary>
    public partial class UCDoctors : UserControl
    {
        string STRNamePage;

        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;
        readonly CollegeContext context = new CollegeContext();

        private void loadRank() { 

            var listWorkHours = (from p in context.WorkHours
                            select p).ToList();
            foreach(var workHour in listWorkHours)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = workHour.Rank;
                item.Value = workHour.Id;
                CBRank.Items.Add(item);
            }
        }
        private void loadDepartment()
        {

            var listSections = (from p in context.Sections
                                 select p).ToList();
            foreach (var section in listSections)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = section.TypeOfSection;
                item.Value = section.Id;
                CBDepartment.Items.Add(item);
            }
        }
        private void loadData()
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CollegeContext"].ConnectionString))
            {
                List<TeacherAndWorkHours> listData = new List<TeacherAndWorkHours>();
                string oString = "Select * from Teachers t LEFT JOIN WorkHours w ON t.IdWorkHours = w.Id";
                SqlCommand oCmd = new SqlCommand(oString, connection);
                connection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        TeacherAndWorkHours teacherAndWork = new TeacherAndWorkHours();
                        teacherAndWork.Id = (int)oReader["Id"];
                        teacherAndWork.NickName = oReader["NickName"].ToString(); 
                        teacherAndWork.Name = oReader["Name"].ToString();
                        teacherAndWork.Rank = oReader["Rank"].ToString();
                        teacherAndWork.Quorum = (int)oReader["Quorum"];
                        teacherAndWork.AcademicOrVirtual = (bool)oReader["AcademicOrVirtual"];
                        listData.Add(teacherAndWork);
                    }
                    connection.Close();
                    DGTeachersView.ItemsSource = listData;
                }
            }
        }

        public UCDoctors()
        {
            InitializeComponent();
            loadRank();
            loadDepartment();
            loadData();
        }

   

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCWorkHours());
            STRNamePage = "النصاب القانوني";
            Form.ChFormName(STRNamePage);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCChangingData());
            STRNamePage = "البيانات المتغيرة";
            Form.ChFormName(STRNamePage);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = TBName.Text;
                string nickname = TBNickName.Text;
                ComboboxItem CBIRank = CBRank.SelectedItem as ComboboxItem;
                int rank = (int)CBIRank.Value;
                ComboboxItem CBIDepartment = CBDepartment.SelectedItem as ComboboxItem;
                int department = (int)CBIDepartment.Value;
                context.Teachers.Add(new Teacher(name, nickname, rank, department));
                context.SaveChanges();
                loadData();
                TBName.Text = "";
                TBNickName.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأ تأكد من البيانات و حاول مرة اخري");

            }

        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TeacherAndWorkHours teacherrow = DGTeachersView.SelectedItem as TeacherAndWorkHours;
                Teacher Teachers = (from p in context.Teachers
                                    where p.Id == teacherrow.Id
                                    select p).Single();
                Teachers.Name = teacherrow.Name;
                Teachers.NickName = teacherrow.NickName;
                context.SaveChanges();
                loadData();
                MessageBox.Show("تم تعديل الصف بنجاح");
            }
            catch (Exception)
            {
                MessageBox.Show("يوجد خطأما الرجاء مراجعة البيانات ثم اعادة المحاولة ");

            }


        }

        private void BTNRemove_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                TeacherAndWorkHours teacherrow = DGTeachersView.SelectedItem as TeacherAndWorkHours;

                Teacher Teachers = (from p in context.Teachers
                                    where p.Id == teacherrow.Id
                                    select p).Single();

                context.Teachers.Remove(Teachers);
                context.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح العنصر بنجاح");
        }
                catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                    "تـأكد من انه غير مرتبط باي مادة  اولاً"); }
}

        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                context.Teachers.RemoveRange(context.Teachers);

                context.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح كل البيانات");
            }
            catch (Exception) {
                MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري" +
                                      "تـأكد من انه لأ يوجد اي احد مرتبط باي مادة  اولاً");
            }


            context.Teachers.RemoveRange(context.Teachers);
            context.SaveChanges();
            loadData();
        }
        public Boolean checkName(int length)
        {
            Boolean result1 = true;
            List<Teacher> precoucode = (from p in context.Teachers
                                                    where p.Name == TBName.Text
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
                result1= true;
            }

            return result1;
        }
        public Boolean checkNickName(int length)
        {
            Boolean result2 = true;
            List<Teacher> precoucode = (from p in context.Teachers
                                        where p.NickName == TBNickName.Text
                                        select p).ToList();

            if (precoucode.Count > 0)
            {
                lerrorcou_name.Content = "لقد ادخلت هذا من قبل ";
                result2= false;
            }
            else
            {
                result2= true;
            }
            if (length < 1)
            {
                lerrorcou_name.Content = "لم تكتب شئ ";
                result2 = false;
            }

            return result2;
        }
        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lerrorcou_code.Content = "";
            lerrorcou_name.Content = "";
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;
            Boolean r1= checkName(length);
            int r = TBNickName.Text.Length;
            Boolean r2 = checkNickName(r);
            if (r1 & r2)
            {
                BTNAdd.IsEnabled = true;
            }
            else
                BTNAdd.IsEnabled = false;
        }
       


        public void TBNickName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lerrorcou_code.Content = "";
            lerrorcou_name.Content = "";
            TextBox objTextBox = (TextBox)sender;
            int length = objTextBox.Text.Length;           
            Boolean r2 = checkNickName(length);
            int r = TBName.Text.Length;
            Boolean r1 = checkName(r);
            if (r1 & r2)
            {
                BTNAdd.IsEnabled = true;
            }
            else
                BTNAdd.IsEnabled = false;
        }
    }
}
