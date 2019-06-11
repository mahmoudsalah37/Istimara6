using System.Windows.Controls;
using Astmara6.Data;
using System.Linq;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

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
                    if(update.Teacher.WorkHour.AcademicOrVirtual == true)
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
        }

        private void BtnExportData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            data2Exel(DGAstmaraA);
        }

        private void data2Exel(DataGrid dataGrid)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true; //www.yazilimkodlama.com
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dataGrid.Columns.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                myRange.Value2 = dataGrid.Columns[j].Header;
            }
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            { //www.yazilimkodlama.com
                for (int j = 0; j < dataGrid.Items.Count; j++)
                {
                    TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }
    }
    
}
