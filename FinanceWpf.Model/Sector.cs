
using System;
using System.Collections.Generic;

namespace FinanceWpf.Model
{
    public class Sector
    {
        public string Key { get; set; }
        public ICollection<Stock> Stocks { get; set; }

    }
}