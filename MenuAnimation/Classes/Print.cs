using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Astmara6.Controls.Print_Data.Child;
using Astmara6.Data;
using System.Linq;
using Microsoft.Office.Interop.Excel;
namespace Astmara6.Classes
{
    public class Print
    {
        private static readonly CollegeContext context = new CollegeContext();

        public static void data2ExelIstmaraA(UserControl v, DataGrid dataGrid, List<AstmaraA> subjectTeachers, string semester, string year)
        {

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DefaultSheetDirection = (int)Constants.xlRTL;
            try
            {
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                sheet1.DisplayRightToLeft = true;
                int plusRow = 4;
                int start = 1;

                sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Merge();
                sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                sheet1.Cells[1, 1].Value2 = "استمارة أ";

                sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Merge();
                sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                sheet1.Cells[2, 1].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                sheet1.Range[sheet1.Cells[3, 2], sheet1.Cells[3, 16]].Merge();
                sheet1.Range[sheet1.Cells[3, 2], sheet1.Cells[3, 16]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                sheet1.Cells[3, 2].Value2 = "القائمون بالتدريس من الكلية";

                sheet1.Range[sheet1.Cells[4, 2], sheet1.Cells[4, 4]].Merge();
                sheet1.Range[sheet1.Cells[4, 2], sheet1.Cells[4, 4]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                sheet1.Cells[4, 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                sheet1.Cells[4, 2].Value2 = "عدد الساعات المقرر بالخطة";
                sheet1.Range[sheet1.Cells[4, 7], sheet1.Cells[4, 10]].Merge();
                sheet1.Range[sheet1.Cells[4, 7], sheet1.Cells[4, 10]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                sheet1.Cells[4, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                sheet1.Cells[4, 7].Value2 = "جملة عدد الساعات";
                sheet1.Range[sheet1.Cells[4, 13], sheet1.Cells[4, 16]].Merge();
                sheet1.Range[sheet1.Cells[4, 13], sheet1.Cells[4, 16]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                sheet1.Cells[4, 13].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                sheet1.Cells[4, 13].Value2 = "جملة عدد الساعات";

                sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 16]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                plusRow = 4;
                start = 1;


                for (int j = 0; j < dataGrid.Columns.Count - start; j++)
                {
                    v.Dispatcher.Invoke(() =>
                    {
                        Range myRange = (Range)sheet1.Cells[1 + plusRow, j + 1];
                        sheet1.Cells[1 + plusRow, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = dataGrid.Columns[j + start].Header;
                    });
                }

                for (int j = 0; j < subjectTeachers.Count; j++)
                {
                    v.Dispatcher.Invoke(() =>
                    {
                        Range myRange = (Range)sheet1.Cells[j + 2 + plusRow, 1];
                        myRange.Value2 = subjectTeachers[j].Subject.Name.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 2];
                        myRange.Value2 = subjectTeachers[j].Subject.Academic.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 3];
                        myRange.Value2 = subjectTeachers[j].Subject.Virtual.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 4];
                        myRange.Value2 = subjectTeachers[j].Subject.Exprement.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 5];
                        myRange.Value2 = subjectTeachers[j].NumOfStudent.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 6];
                        myRange.Value2 = subjectTeachers[j].NumberOfSections.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 7];
                        myRange.Value2 = subjectTeachers[j].TotalPaper.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 8];
                        myRange.Value2 = subjectTeachers[j].TotalVirtual.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 9];
                        myRange.Value2 = subjectTeachers[j].TotalExperment.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 10];
                        myRange.Value2 = subjectTeachers[j].TotalSuperVision.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 11];
                        myRange.Value2 = subjectTeachers[j].Teacher.Name.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 12];
                        myRange.Value2 = subjectTeachers[j].Teacher.WorkHour.Rank.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 13];
                        myRange.Value2 = subjectTeachers[j].NumOfPaper.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 14];
                        myRange.Value2 = subjectTeachers[j].NumberOfVirtual.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 15];
                        myRange.Value2 = subjectTeachers[j].NumberOfExprement.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 16];
                        myRange.Value2 = subjectTeachers[j].NumberOfSuperVision.ToString();
                    });

                }

                for (int i = 1; i <= dataGrid.Items.Count; i++)
                {
                    Range myRange = (Range)sheet1.Cells[i + 2 + plusRow - 1, 2];
                    string before = myRange.Value2;
                    if (before == "" || before == null)
                        break;
                    Range myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 2];
                    string after = myRange1.Value2;
                    while (before == after)
                    {

                        Range myRange2 = sheet1.Range[sheet1.Cells[i + 2 + plusRow, 1], sheet1.Cells[i + 2 + plusRow, 11]];
                        myRange2.Value2 = "";
                        myRange1.Value2 = "";
                        i++;
                        myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 2];
                        after = myRange1.Value2;
                    }
                    sheet1.Range[sheet1.Cells[i + 1 + plusRow, 1], sheet1.Cells[i + 1 + plusRow, 16]].Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d; ;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void data2ExelIstmaraBD(UserControl v, DataGrid dataGrid, List<AstmaraB> astmaraBs, string semester, string year)
        {
            object missing = Type.Missing;
            int? x = astmaraBs[0].Teacher.idSection;
            var list = (from p in context.AstmaraBs
                        select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true & t.Teacher.idSection == x).Select(t => t.Teacher).Distinct().ToList();
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DefaultSheetDirection = (int)Constants.xlRTL;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            excel.Visible = true;
            Worksheet sheet1 = workbook.ActiveSheet as Worksheet;
            for (int k = 0; k < list.Count(); k++)
            {
                v.Dispatcher.Invoke(() =>
                {

                    sheet1.Name = list[k].Name.ToString();
                    sheet1.DisplayRightToLeft = true;
                    int plusRow = 4;
                    int start = 0;

                    sheet1.Range[sheet1.Cells[1, 4], sheet1.Cells[1, 11]].Merge();
                    sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    sheet1.Cells[1, 4].Value2 = "استمارة ب د";

                    sheet1.Range[sheet1.Cells[2, 4], sheet1.Cells[2, 11]].Merge();
                    sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 10]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    sheet1.Cells[2, 4].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                    sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[4, 3]].Merge();

                    sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 10]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    sheet1.Cells[5, 11].Value2 = "ملاحظات";
                    plusRow = 4;
                    start = 0;



                    for (int j = 0; j < dataGrid.Columns.Count - start; j++)
                    {
                        v.Dispatcher.Invoke(() =>
                        {
                            Range myRange = (Range)sheet1.Cells[1 + plusRow, j + 1];
                            sheet1.Cells[1 + plusRow, j + 1].Font.Bold = true;
                            sheet1.Columns[j + 1].ColumnWidth = 15;
                            myRange.Value2 = dataGrid.Columns[j + start].Header;
                        });
                    }





                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 10];
                    //        myRange.Value2 = subjectTeachers[j].TotalSuperVision.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 11];
                    //        myRange.Value2 = subjectTeachers[j].Teacher.Name.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 12];
                    //        myRange.Value2 = subjectTeachers[j].Teacher.WorkHour.Rank.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 13];
                    //        myRange.Value2 = subjectTeachers[j].NumOfPaper.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 14];
                    //        myRange.Value2 = subjectTeachers[j].NumberOfVirtual.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 15];
                    //        myRange.Value2 = subjectTeachers[j].NumberOfExprement.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 16];
                    //        myRange.Value2 = subjectTeachers[j].NumberOfSuperVision.ToString();

                    int y = list[k].Id;
                    int? section = astmaraBs[k].Teacher.idSection;
                    var list1 = (from p in context.AstmaraBs
                                 select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == true & t.Teacher.idSection == section & t.IdDoctor == y).ToList();
                    for (int j = 0; j < list1.Count; j++)
                    {
                        Range myRange = (Range)sheet1.Cells[j + 2 + plusRow, 1];
                        myRange.Value2 = list1[j].Teacher.Name.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 2];
                        myRange.Value2 = list1[j].Teacher.WorkHour.Rank.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 3];
                        myRange.Value2 = list1[j].Teacher.WorkHour.Quorum.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 4];
                        myRange.Value2 = list1[j].Subject.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 5];
                        myRange.Value2 = list1[j].Paper.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 6];
                        myRange.Value2 = list1[j].Virtial.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 7];
                        myRange.Value2 = list1[j].Experment.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 8];
                        myRange.Value2 = list1[j].Sum.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 9];
                        myRange.Value2 = list1[j].Total.ToString();
                    }
                    for (int i = 1; i <= list1.Count(); i++)
                    {
                        Range myRange1 = (Range)sheet1.Range[sheet1.Cells[i + 2 + plusRow, 1], sheet1.Cells[i + 2 + plusRow, 3]];

                        Range myRange2 = sheet1.Range[sheet1.Cells[i + 2 + plusRow, 9], sheet1.Cells[i + 2 + plusRow, 9]];
                        myRange2.Value2 = "";
                        myRange1.Value2 = "";

                    }
                    if(k+1 != list.Count())
                         sheet1 = (Worksheet)workbook.Sheets.Add(missing, missing, 1, missing);
                });
            }
        }

        public static void data2ExelIstmaraBA(UserControl v, DataGrid dataGrid, List<AstmaraB> astmaraBs, string semester, string year)
        {
            object missing = Type.Missing;
            int? x = astmaraBs[0].Teacher.idSection;
            var list = (from p in context.AstmaraBs
                        select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false & t.Teacher.idSection == x).Select(t => t.Teacher).Distinct().ToList();
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DefaultSheetDirection = (int)Constants.xlRTL;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            excel.Visible = true;
            Worksheet sheet1 = workbook.ActiveSheet as Worksheet;
            for (int k = 0; k < list.Count(); k++)
            {
                v.Dispatcher.Invoke(() =>
                {

                    sheet1.Name = list[k].Name.ToString();
                    sheet1.DisplayRightToLeft = true;
                    int plusRow = 4;
                    int start = 0;

                    sheet1.Range[sheet1.Cells[1, 4], sheet1.Cells[1, 11]].Merge();
                    sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    sheet1.Cells[1, 4].Value2 = "استمارة ب د";

                    sheet1.Range[sheet1.Cells[2, 4], sheet1.Cells[2, 11]].Merge();
                    sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 10]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    sheet1.Cells[2, 4].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                    sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[4, 3]].Merge();

                    sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 10]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    sheet1.Cells[5, 11].Value2 = "ملاحظات";
                    plusRow = 4;
                    start = 0;



                    for (int j = 0; j < dataGrid.Columns.Count - start; j++)
                    {
                        v.Dispatcher.Invoke(() =>
                        {
                            Range myRange = (Range)sheet1.Cells[1 + plusRow, j + 1];
                            sheet1.Cells[1 + plusRow, j + 1].Font.Bold = true;
                            sheet1.Columns[j + 1].ColumnWidth = 15;
                            myRange.Value2 = dataGrid.Columns[j + start].Header;
                        });
                    }





                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 10];
                    //        myRange.Value2 = subjectTeachers[j].TotalSuperVision.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 11];
                    //        myRange.Value2 = subjectTeachers[j].Teacher.Name.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 12];
                    //        myRange.Value2 = subjectTeachers[j].Teacher.WorkHour.Rank.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 13];
                    //        myRange.Value2 = subjectTeachers[j].NumOfPaper.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 14];
                    //        myRange.Value2 = subjectTeachers[j].NumberOfVirtual.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 15];
                    //        myRange.Value2 = subjectTeachers[j].NumberOfExprement.ToString();
                    //        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 16];
                    //        myRange.Value2 = subjectTeachers[j].NumberOfSuperVision.ToString();

                    int y = list[k].Id;
                    //int? section = astmaraBs[k].Teacher.idSection;
                    var list1 = (from p in context.AstmaraBs
                                 select p).Where(t => t.Teacher.WorkHour.AcademicOrVirtual == false & t.IdDoctor == y).ToList();
                    for (int j = 0; j < list1.Count; j++)
                    {
                        Range myRange = (Range)sheet1.Cells[j + 2 + plusRow, 1];
                        myRange.Value2 = list1[j].Teacher.Name.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 2];
                        myRange.Value2 = list1[j].Teacher.WorkHour.Rank.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 3];
                        myRange.Value2 = list1[j].Teacher.WorkHour.Quorum.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 4];
                        myRange.Value2 = list1[j].Subject.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 5];
                        myRange.Value2 = list1[j].Paper.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 6];
                        myRange.Value2 = list1[j].Virtial.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 7];
                        myRange.Value2 = list1[j].Experment.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 8];
                        myRange.Value2 = list1[j].Sum.ToString();
                        myRange = (Range)sheet1.Cells[j + 2 + plusRow, 9];
                        myRange.Value2 = list1[j].Total.ToString();
                    }
                    for (int i = 1; i <= list1.Count(); i++)
                    {
                        Range myRange1 = (Range)sheet1.Range[sheet1.Cells[i + 2 + plusRow, 1], sheet1.Cells[i + 2 + plusRow, 3]];

                        Range myRange2 = sheet1.Range[sheet1.Cells[i + 2 + plusRow, 9], sheet1.Cells[i + 2 + plusRow, 9]];
                        myRange2.Value2 = "";
                        myRange1.Value2 = "";

                    }
                    if (k + 1 != list.Count())
                        sheet1 = (Worksheet)workbook.Sheets.Add(missing, missing, 1, missing);

                });
            }
        }
        public static void data2ExelPlan(UserControl v, DataGrid dataGrid, List<SubjectTeacher> subjectTeachers, string semester, string year)
        {
            int plusRow = 4;
            int start = 1;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DefaultSheetDirection = (int)Constants.xlRTL;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            excel.Visible = true;
            Worksheet sheet1 = workbook.ActiveSheet as Worksheet;
            sheet1.Range[sheet1.Cells[1, 4], sheet1.Cells[1, 11]].Merge();
            sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            sheet1.Cells[1, 4].Value2 = "الخطة";

            sheet1.Range[sheet1.Cells[2, 4], sheet1.Cells[2, 11]].Merge();
            sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 10]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            sheet1.Cells[2, 4].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

            sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[4, 3]].Merge();

            sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 5]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            for (int j = 0; j < dataGrid.Columns.Count - start; j++)
            {
                v.Dispatcher.Invoke(() =>
                {
                    Range myRange = (Range)sheet1.Cells[1 + plusRow, j + 1];
                    sheet1.Cells[1 + plusRow, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 15;
                    myRange.Value2 = dataGrid.Columns[j + start].Header;
                });
            }
            for (int j = 0; j < subjectTeachers.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[j + 2 + plusRow, 1];
                myRange.Value2 = subjectTeachers[j].Teacher.Name.ToString();
                myRange = (Range)sheet1.Cells[j + 2 + plusRow, 2];
                myRange.Value2 = subjectTeachers[j].Subject.Code.ToString();
                myRange = (Range)sheet1.Cells[j + 2 + plusRow, 3];
                myRange.Value2 = subjectTeachers[j].Subject.Name.ToString();
                myRange = (Range)sheet1.Cells[j + 2 + plusRow, 4];
                myRange.Value2 = subjectTeachers[j].Branch.Name.ToString();
                myRange = (Range)sheet1.Cells[j + 2 + plusRow, 5];
                myRange.Value2 = subjectTeachers[j].Level.Name.ToString();
                //myRange = (Range)sheet1.Cells[j + 2 + plusRow, 6];
                //myRange.Value2 = subjectTeachers[j].Virtial.ToString();
                //myRange = (Range)sheet1.Cells[j + 2 + plusRow, 7];
                //myRange.Value2 = subjectTeachers[j].Experment.ToString();
                //myRange = (Range)sheet1.Cells[j + 2 + plusRow, 8];
                //myRange.Value2 = subjectTeachers[j].Sum.ToString();
                //myRange = (Range)sheet1.Cells[j + 2 + plusRow, 9];
                //myRange.Value2 = subjectTeachers[j].Total.ToString();
            }
            
        }

            public static void data2Exel(UserControl v, DataGrid dataGrid, int IDPage, string semester, string year)
        {

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DefaultSheetDirection = (int)Constants.xlRTL;
            try
            {
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                sheet1.DisplayRightToLeft = true;
                int plusRow = 0;
                int start = 0;
                switch (IDPage)
                {
                    case 1:
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Merge();
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[1, 1].Value2 = "استمارة أ";

                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Merge();
                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[2, 1].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                        sheet1.Range[sheet1.Cells[3, 2], sheet1.Cells[3, 16]].Merge();
                        sheet1.Range[sheet1.Cells[3, 2], sheet1.Cells[3, 16]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[3, 2].Value2 = "القائمون بالتدريس من الكلية";

                        sheet1.Range[sheet1.Cells[4, 2], sheet1.Cells[4, 4]].Merge();
                        sheet1.Range[sheet1.Cells[4, 2], sheet1.Cells[4, 4]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[4, 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        sheet1.Cells[4, 2].Value2 = "عدد الساعات المقرر بالخطة";
                        sheet1.Range[sheet1.Cells[4, 7], sheet1.Cells[4, 10]].Merge();
                        sheet1.Range[sheet1.Cells[4, 7], sheet1.Cells[4, 10]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[4, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        sheet1.Cells[4, 7].Value2 = "جملة عدد الساعات";
                        sheet1.Range[sheet1.Cells[4, 13], sheet1.Cells[4, 16]].Merge();
                        sheet1.Range[sheet1.Cells[4, 13], sheet1.Cells[4, 16]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[4, 13].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        sheet1.Cells[4, 13].Value2 = "جملة عدد الساعات";

                        sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 16]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        plusRow = 4;
                        start = 1;
                        break;
                    case 2:
                        sheet1.Range[sheet1.Cells[1, 4], sheet1.Cells[1, 11]].Merge();
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[1, 4].Value2 = "استمارة ب د";

                        sheet1.Range[sheet1.Cells[2, 4], sheet1.Cells[2, 11]].Merge();
                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 10]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[2, 4].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[4, 3]].Merge();
                        //sheet1.Cells[1, 1].image = "Assets/Logo_Uni.png";

                        sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 10]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        sheet1.Cells[5, 11].Value2 = "ملاحظات";
                        plusRow = 4;
                        start = 0;
                        break;
                    case 3:
                        sheet1.Range[sheet1.Cells[1, 4], sheet1.Cells[1, 11]].Merge();
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[1, 4].Value2 = "استمارة ب د";

                        sheet1.Range[sheet1.Cells[2, 4], sheet1.Cells[2, 11]].Merge();
                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 10]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[2, 4].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[4, 3]].Merge();
                        //sheet1.Cells[1, 1].image = "Assets/Logo_Uni.png";

                        sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 10]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        sheet1.Cells[5, 11].Value2 = "ملاحظات";

                        plusRow = 4;
                        start = 0;
                        break;
                }

                for (int j = 0; j < dataGrid.Columns.Count - start; j++)
                {
                    v.Dispatcher.Invoke(() =>
                    {
                        Range myRange = (Range)sheet1.Cells[1 + plusRow, j + 1];
                        sheet1.Cells[1 + plusRow, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = dataGrid.Columns[j + start].Header;
                    });
                }

                for (int i = 0; i < dataGrid.Columns.Count - start; i++)
                {

                    for (int j = 0; j < dataGrid.Items.Count; j++)
                    {

                        TextBlock b = dataGrid.Columns[i + start].GetCellContent(dataGrid.Items[j]) as TextBlock;
                        Range myRange = (Range)sheet1.Cells[j + 2 + plusRow, i + 1];
                        if (b != null)
                            myRange.Value2 = b.Text;

                    }

                }

                switch (IDPage)
                {
                    case 1:
                        for (int i = 1; i <= dataGrid.Items.Count; i++)
                        {
                            Range myRange = (Range)sheet1.Cells[i + 2 + plusRow - 1, 1];
                            string before = myRange.Value2;
                            if (before == "" || before == null)
                                break;
                            Range myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 1];
                            string after = myRange1.Value2;
                            while (before == after)
                            {
                                Range myRange2 = sheet1.Range[sheet1.Cells[i + 2 + plusRow, 2], sheet1.Cells[i + 2 + plusRow, 9]];
                                myRange2.Value2 = "";
                                myRange1.Value2 = "";
                                i++;
                                myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 1];
                                after = myRange1.Value2;
                            }
                            sheet1.Range[sheet1.Cells[i + 1 + plusRow, 1], sheet1.Cells[i + 1 + plusRow, 16]].Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d; ;
                        }
                        break;
                    case 2:
                        for (int i = 1; i <= dataGrid.Items.Count; i++)
                        {
                            Range myRange = (Range)sheet1.Cells[i + 2 + plusRow - 1, 1];
                            string before = myRange.Value2;
                            if (before == "" || before == null)
                                break;
                            Range myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 1];
                            string after = myRange1.Value2;
                            while (before == after)
                            {
                                Range myRange2 = sheet1.Range[sheet1.Cells[i + 2 + plusRow, 1], sheet1.Cells[i + 2 + plusRow, 3]];
                                myRange2.Value2 = "";
                                sheet1.Cells[i + 2 + plusRow, 10].Value2 = "";
                                i++;
                                myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 1];
                                after = myRange1.Value2;
                            }
                            sheet1.Range[sheet1.Cells[i + 1 + plusRow, 1], sheet1.Cells[i + 1 + plusRow, 16]].Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d; ;
                        }
                        break;
                    case 3:
                        for (int i = 1; i <= dataGrid.Items.Count; i++)
                        {
                            Range myRange = (Range)sheet1.Cells[i + 2 + plusRow - 1, 1];
                            string before = myRange.Value2;
                            if (before == "" || before == null)
                                break;
                            Range myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 1];
                            string after = myRange1.Value2;
                            while (before == after)
                            {
                                Range myRange2 = sheet1.Range[sheet1.Cells[i + 2 + plusRow, 1], sheet1.Cells[i + 2 + plusRow, 3]];
                                myRange2.Value2 = "";
                                sheet1.Cells[i + 2 + plusRow, 10].Value2 = "";
                                i++;
                                myRange1 = (Range)sheet1.Cells[i + 2 + plusRow, 1];
                                after = myRange1.Value2;
                            }
                            sheet1.Range[sheet1.Cells[i + 1 + plusRow, 1], sheet1.Cells[i + 1 + plusRow, 16]].Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d; ;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
