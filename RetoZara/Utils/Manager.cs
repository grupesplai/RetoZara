using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using RetoZara.FilesExcel;

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
            DateTime add2 = add.AddDays(1);
            return add2;
        }

        public static List<Data> GetActions(List<Data> dateList)
        {
            dateList.Reverse();
            List<Data> cotizationsDayList = new List<Data>();

            foreach (var d in dateList)
            {
                DateTime dome = GetLastFriday(d.Date);
                Data obj = dateList.Find(x => x.Date == dome );
                if (obj == null)
                {
                    int cont = 0;
                    do{
                        dome = dome.AddDays(1);
                        cont++;
                    } while (dateList.Find(x => x.Date == dome) == null && cont < 30);
                }
                obj = dateList.Find(x => x.Date == dome);
                try
                {
                    if (!cotizationsDayList.Exists(x => x.Date == obj.Date))
                       cotizationsDayList.Add(new Data(obj.Date, obj.Closed, obj.Opened));
                }
                catch{ }
            }
            //List<Data> cotization2006 = FileAccessDates.GetDataFromDate(cotizationsDayList,"01/01/06","01/01/07");
            //List<Data> cotization2007 = FileAccessDates.GetDataFromDate(cotizationsDayList, "01/01/07", "01/01/08");
            //List<Data> cotization2011 = FileAccessDates.GetDataFromDate(cotizationsDayList, "01/01/11", "01/01/12");
            return cotizationsDayList;
        }

        public static decimal GetTotal(List<Data> cotizationsDayList, decimal exactDay)
        {
            decimal total = 0;
            foreach (Data ld in cotizationsDayList)
            {
                total = Decimal.Round(total + ((50-(50*2/100)) / ld.Opened), 3);
               // Console.WriteLine(ld.Date +"\t"+ld.Closed + "\t"+ld.Opened);
            }
            total = exactDay * total;
            CreateExcel(cotizationsDayList, exactDay);
            return total;
        }
        public static void CreateExcel(List<Data> dateList, decimal exactDay)
        {
            string path = @"C:\Users\G1\source\repos\RetoZara\RetoZara\DatosFechas.xls";
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
                    CultureInfo ci = new CultureInfo("Es-Es");
                    foreach (Data row in dateList)
                    {
                        excel.Cells[rowIndex, 1] = ci.DateTimeFormat.GetDayName(row.Date.DayOfWeek)
                            + " " + row.Date.Day + " " + ci.DateTimeFormat.GetMonthName(row.Date.Month) + " " + row.Date.Year;
                        excel.Cells[rowIndex, 2] = row.Closed;
                        excel.Cells[rowIndex, 3] = row.Opened;
                        total = Decimal.Round(total + ((50 - (50 * 2 / 100)) / row.Opened), 3); ;
                        rowIndex++;
                    }
                    excel.Cells[2, 6] = "Total";
                    excel.Cells[3, 6] = Decimal.Round((total * exactDay), 3);
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
