using System;
using System.Configuration;
using System.Data;
using ClosedXML.Excel;

namespace MyTelegramBot
{
    public class SSS
    {
        public static DataTable ImportExcel()
        {

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(ConfigurationManager.AppSettings["excelPath"]))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                    
                }
                return dt;
            }
        }

    }
}