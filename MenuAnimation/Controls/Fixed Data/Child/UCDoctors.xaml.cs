
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
using System.Linq;
using Astmara6.Data;

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
            string name = TBName.Text;
            string nickname = TBNickName.Text;
            ComboboxItem CBIRank = CBRank.SelectedItem as ComboboxItem;
            int rank = (int)CBIRank.Value;
            ComboboxItem CBIDepartment = CBDepartment.SelectedItem as ComboboxItem;
            int department = (int)CBIDepartment.Value;
            context.Teachers.Add(new Teacher(name,nickname,rank,department));
            context.SaveChanges();
            loadData();
        }
    }
}
