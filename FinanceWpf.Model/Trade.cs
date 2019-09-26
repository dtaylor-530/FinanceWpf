using NodaMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinanceWpf.Model
{
    public class Trade//:IDbRow
    {
        public string Key { get; set; }
        //public Int64 Id { get; set; }
        //public Int64 ParentId { get; set; }
        public DateTime Date { get; set; }
        public Money Price { get; set; }
        public Money Amount { get; set; }
        public Money Profit { get; set; }
    }
}
