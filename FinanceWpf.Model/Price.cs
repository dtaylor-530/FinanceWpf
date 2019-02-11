using NodaMoney;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceWpf.Model
{

    //public struct Price
    //{
    //    public TeaTime.Time Date { get; set; }
    //    public double Bid { get; set; }
    //    public double Offer { get; set; }

    //}
    public struct Price
    {
        public string Key { get; set; }
        public DateTime Date { get; set; }

        public Money Value { get; set; }


    }
}
