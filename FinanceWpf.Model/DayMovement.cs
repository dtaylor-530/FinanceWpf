using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceWpf.Model
{
    public class DayMovement
    {
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Adj_Close { get; set; }
        public double Volume { get; set; }
 
    }
}
