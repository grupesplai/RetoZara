using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace RetoZara
{
    internal class Manager
    {
        public static int GetMonth(string month)
        {
            string[] monthsArray = {"ene","feb","mar","abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
            int index = Array.IndexOf(monthsArray, month);
            return index + 1;
        }

        public static DateTime GetLastFriday(DateTime date)
        {
            var dateFirts = new DateTime(date.Year, date.Month, 1).AddMonths(1);
            int vector = (((int)dateFirts.DayOfWeek + 2) % 7) + 1;

            DateTime add = dateFirts.AddDays(-vector);
            return add;
        }

        public static List<Data> GetActions(List<Data> dateList)
        {
            List<Data> cotizationsDayList = new List<Data>();

            foreach (var d in dateList)
            {
                DateTime lastCotizationDay = new DateTime();
                if (GetLastFriday(d.Date).Equals(d.Date)) lastCotizationDay = d.Date;

                else
                {
                    while (!GetLastFriday(d.Date).Equals(d.Date))
                        d.Date = d.Date.AddDays(1);

                    lastCotizationDay = d.Date;
                }
                if (!(cotizationsDayList.Exists(x => x.Date == lastCotizationDay)))
                {
                    cotizationsDayList.Add(new Data(d.Date, d.Closed, d.Opened));
                    
                }
            }
            return cotizationsDayList;
        }

        public static decimal GetTotal(List<Data> cotizationsDayList, decimal exactDay)
        {
            cotizationsDayList.Reverse();
            decimal total = 0;
            foreach (Data ld in cotizationsDayList)
            {
                total = Decimal.Round(total + ((50-(50*2/100)) / ld.Opened), 3);
                Console.WriteLine(ld.Date +"\t"+ld.Closed + "\t"+ld.Opened);
            }
            total = exactDay * total;
            CreateExcel(cotizationsDayList, exactDay);
            return total;
        }
        public static void CreateExcel(List<Data> dateList, decimal exactDay)
        {
            string path = @"C:\Users\G1\source\repos\RetoZara\RetoZara\DatosFechas.csv";
            if (!File.Exists(path))
            {
                _Application excel = new Application();
                try
                {
                    Workbook excelWorkBook = excel.Application.Workbooks.Add(true);
                    excel.Cells[1, 1] = "Fecha de cotización";
                    excel.Cells[1, 2] = "Precio cierre";
                    excel.Cells[1, 3] = "Precio apertura";
                    int rowIndex = 2;
                    decimal total = 0;
                    foreach (Data row in dateList)
                    {
                        excel.Cells[rowIndex, 1] = row.Date;
                        excel.Cells[rowIndex, 2] = row.Closed;
                        excel.Cells[rowIndex, 3] = row.Opened;
                        total = Decimal.Round(total + ((50 - (50 * 2 / 100)) / row.Opened), 3); ;
                        rowIndex++;
                    }
                    excel.Cells[2, 5] = "Total";
                    excel.Cells[3, 5] = Decimal.Round((total * exactDay), 3);
                    excel.Visible = false;

                    Worksheet worksheet = (Worksheet)excel.ActiveSheet;
                    worksheet.Activate();
                    excelWorkBook.SaveAs(path, XlFileFormat.xlWorkbookNormal, Missing.Value,
                        Missing.Value, false, false, XlSaveAsAccessMode.xlShared, false, false,
                        Missing.Value, Missing.Value, Missing.Value);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }
        }
    }
}
