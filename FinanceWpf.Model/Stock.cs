using Deedle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.Model
{

    public class Stock
    {
        public string Name { get; set; }
        public string Key { get; set; }
        //public Series<DateTime, double> Prices { get; set; }
        public IEnumerable<DayMovement> Prices { get; set; }

    }

}

