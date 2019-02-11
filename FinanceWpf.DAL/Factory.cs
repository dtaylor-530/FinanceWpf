
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
            Profit = (Money)UtilityMath.RandomSingleton.Instance.Random.NextDouble() * 10
        };

    }


    public class PriceFactory : IDisposable
    {

        string[] keys = new[] { "FB", "CSCO", "MSFT" };
        IEnumerator<DateTime> dts = DataHelper.GenerateDateTimes().GetEnumerator();
        Dictionary<string, IEnumerator<double>> stocks;

        public PriceFactory()
        {
            stocks = keys.ToDictionary(_ => _, _ => DataHelper.GenerateRandomPrices().GetEnumerator());
        }



        public Price NewPrice()
        {
            string key = keys[UtilityMath.RandomSingleton.Instance.Random.Next(0, stocks.Count)];
            stocks[key].MoveNext();
            dts.MoveNext();
            return new Price
            {
                Key = key,
                Date = dts.Current,
                Value = (Money)stocks[key].Current,

            };
        }

        public void Dispose()
        {
            foreach (var s in stocks)
                s.Value.Dispose();

            dts.Dispose();
        }
    }
}
