
using FinanceWpf.Model;
using NodaMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FinanceWpf.DAL
{


    public class PriceFactory : IDisposable
    {

        string[] keys = new[] { "FB", "CSCO", "MSFT" };
        IEnumerator<DateTime> dts = DataHelper.GenerateDateTimes().GetEnumerator();
        Dictionary<string, IEnumerator<double>> stocks;
        Random random = new Random();

        public PriceFactory()
        {
            stocks = keys.ToDictionary(_ => _, _ => DataHelper.GenerateRandomPrices().GetEnumerator());
        }


        public Price NewPrice()
        {
            string key = keys[UtilityMath.RandomSingleton.Instance.Random.Next(0, stocks.Count)];
            stocks[key].MoveNext();
            dts.MoveNext();
            double value = stocks[key].Current*10;
            return new Price
            {
                Key = key,
                Date = dts.Current,
                Value = (Money)value,
                              //   = (Money)(value + random.Next(-1, 2) * random.NextDouble() * value / 50d);
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
