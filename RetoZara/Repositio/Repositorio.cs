using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoZara
{
    class Repositorio : IRepositorio
    {

        public void SetList(string path)
        {
            using (var reader = new StreamReader(path))
            {
                List<Data> dateList = new List<Data>();
                DateTime tDate;
                NumberFormatInfo format = new NumberFormatInfo() { NumberDecimalSeparator = "." };
                string newDate, line;
                decimal openValue, closeValue;

                while (null != (line = reader.ReadLine()))
                {
                    var values = line.Split(';');
                    var month = values[0].Split('-');

                    month[1] = Manager.GetMonth(month[1]).ToString();
                    newDate = month[0] + "-" + month[1] + "-" + month[2];

                    openValue = Convert.ToDecimal(values[1], format);
                    closeValue = Convert.ToDecimal(values[2], format);

                    tDate = DateTime.Parse(newDate, CultureInfo.GetCultureInfo("es"));
                    dateList.Add(new Data(tDate, openValue, closeValue));
                }
            }
        }
    }
}
