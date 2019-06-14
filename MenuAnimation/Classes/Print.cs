using System;
using System.Windows;
using System.Windows.Controls;
using Astmara6.Controls.Print_Data.Child;
using Microsoft.Office.Interop.Excel;
namespace Astmara6.Classes
{
   public static class  Print
    {
        public static void data2Exel(UserControl v, DataGrid dataGrid,int IDPage, string semester, string year)
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
                        sheet1.Cells[2, 1].Value2 = year+" :للعام الدراسي"+"                       "+semester+ " :الفصل الدراسي";

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
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Merge();
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[1, 1].Value2 = "استمارة ب د";

                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Merge();
                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[2, 1].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                        sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 16]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        plusRow = 3;
                        start = 0;
                        break;
                    case 3:
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Merge();
                        sheet1.Range[sheet1.Cells[1, 1], sheet1.Cells[1, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[1, 1].Value2 = "استمارة ب م";

                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Merge();
                        sheet1.Range[sheet1.Cells[2, 1], sheet1.Cells[2, 17]].Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        sheet1.Cells[2, 1].Value2 = year + " :للعام الدراسي" + "                       " + semester + " :الفصل الدراسي";

                        sheet1.Range[sheet1.Cells[5, 1], sheet1.Cells[5, 16]].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        plusRow = 3;
                        start = 0;
                        break;
                }

                for (int j = 0; j < dataGrid.Columns.Count-start; j++)
                {
                        v.Dispatcher.Invoke(() =>
                        {
                        Range myRange = (Range)sheet1.Cells[1+ plusRow, j + 1];
                        sheet1.Cells[1+ plusRow, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = dataGrid.Columns[j+start].Header;
                        });
                }
               
                for (int i = 0; i < dataGrid.Columns.Count-start; i++)
                {
                    v.Dispatcher.Invoke(() =>
                    {
                        for (int j = 0; j < dataGrid.Items.Count; j++)
                        {
                            v.Dispatcher.Invoke(() =>
                            {
                                TextBlock b = dataGrid.Columns[i+start].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                Range myRange = (Range)sheet1.Cells[j + 2+ plusRow, i + 1];
                                if (b != null)
                                    myRange.Value2 = b.Text;
                            });

                        }
                    });

                    }

                 switch (IDPage)
                {
                    case 1:
                        for (int i=1;i<= dataGrid.Items.Count;i++)
                        {
                            Range myRange = (Range)sheet1.Cells[i + 2 + plusRow-1, 1];
                            string before = myRange.Value2;
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
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
