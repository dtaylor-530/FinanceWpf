using FinanceWpf.Model;
using NodaMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.DAL
{
    public class TradeFactory
    {
        private PriceFactory priceFactory;

        public TradeFactory()
        {
            priceFactory = new PriceFactory();
        }

        public Trade NewTrade() => NewTrade(priceFactory.NewPrice());


        public Trade NewTrade(Price price) => new Trade
        {
            Key = price.Key,
            Date = price.Date,
            Price = price.Value,
            Amount = (UtilityMath.RandomSingleton.Instance.Random.Next(1, 2000) / 2) * (10 ^ UtilityMath.RandomSingleton.Instance.Random.Next(-5, 5)),
            Profit = (Money)(UtilityMath.RandomSingleton.Instance.Random.NextDouble() * 10)
        };

    }

}
