
using  FinanceWpf.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;



namespace  FinanceWpf.Service
{


    //class DataHelper
    //{


    //    public static IEnumerable<KeyValuePair<DateTime, double>> GetDataBySymbol(string symbol)
    //    {


    //        var x = DataSingleton.Instance.Data.Keys.SingleOrDefault(_ => _.Contains(symbol));
    //        var y = (DataSingleton.Instance.Data[x]);

    //        if (x != null)
    //            return y.ToKVPairs();
    //        else
    //            throw new Exception($"unable to find {symbol} Data in Resources folder");
    //    }
    //}




    //[DisplayName("Stock Ticker")]
    //[Description("Defines a grammar that analyzes an observable sequence of stock ticker values over time "
    //           + "and yields alerts for reversals beyond certain thresholds, following 2 or more consecutive "
    //           + "ticks in the same direction.")]
    //public sealed class StockTickerLab //: StockTickerBaseLab
    //{
    //    protected  void Main()
    //    {
    //        //TraceLine(Instructions.PressAnyKeyToCancel);

    //        var random = new Random();

    //        IObservable<int> values = Observable
    //          .Interval(TimeSpan.FromSeconds(1))
    //          .Select(_ => random.Next(1, 50));

    //        IConnectableObservable<StockTick> ticks = values
    //          .Scan(StockTick.Empty, (acc, cur) => new StockTick(cur, cur - acc.Value))
    //          .Publish();

    //        IObservable<StockAlert> alerts = ticks.Parse(parser =>
    //          from next in parser
    //          let ups = next.Where(tick => tick.Change > 0)
    //          let downs = next.Where(tick => tick.Change < 0)
    //          let downAlert = from manyUps in ups.AtLeast(2).ToList()
    //                          from reversalDown in downs.NonGreedy()
    //                          where reversalDown.Change <= -11
    //                          select new StockAlert(manyUps, reversalDown)
    //          let upAlert = from manyDowns in downs.AtLeast(2).ToList()
    //                        from reversalUp in ups.NonGreedy()
    //                        where reversalUp.Change >= 21
    //                        select new StockAlert(manyDowns, reversalUp)
    //          select downAlert.Or(upAlert).Ambiguous(untilCount: 1));

    //        using (ticks.Subscribe(WriteTick))
    //        using (alerts.Subscribe(WriteAlert))
    //        using (ticks.Connect())
    //        {
    //            WaitForKey();
    //        }
    //    }


    //}



}