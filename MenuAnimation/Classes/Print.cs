using System;
using System.Windows;
using System.Windows.Controls;
using Astmara6.Controls.Print_Data.Child;
using Microsoft.Office.Interop.Excel;
namespace Astmara6.Classes
{
   public static class  Print
    {
        public static void data2Exel(UserControl v, DataGrid dataGrid)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DefaultSheetDirection = (int)Constants.xlRTL;
            try
            {
                excel.Visible = true; 
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                sheet1.DisplayRightToLeft = true;
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        v.Dispatcher.Invoke(() =>
                        {
                        Range myRange = (Range)sheet1.Cells[1, j + 1];
                        sheet1.Cells[1, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = dataGrid.Columns[j].Header;
                        });
                    }
               
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        v.Dispatcher.Invoke(() =>
                        {
                            for (int j = 0; j < dataGrid.Items.Count; j++)
                            {
                                v.Dispatcher.Invoke(() =>
                                {
                                    TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                                Range myRange = (Range)sheet1.Cells[j + 2, i + 1];
                                if (b != null)
                                    myRange.Value2 = b.Text;
                                });

                            }
                        });

                      }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
