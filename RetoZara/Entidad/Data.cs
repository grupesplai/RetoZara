using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoZara
{
    class Data
    {
        // Automatic properties
        public DateTime Date { get; set; }
        public decimal Closed { get; set; }
        public decimal Opened { get; set; }

        public Data() { }
        public Data(DateTime date, decimal closed, decimal opened)
        {
            Date = date;
            Closed = closed;
            Opened = opened;
        }
    }
}
