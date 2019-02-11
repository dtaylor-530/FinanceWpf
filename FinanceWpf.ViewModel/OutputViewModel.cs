using System;
using System.Collections.Generic;

//using UtilityWpf.ViewModel;

using System.Reactive.Linq;


using System.Windows;
using FinanceSystem.Model;


namespace FinanceSystem.ViewModel
{
    public class OutputViewModel //: INPCBase
    {
        //    public ProfitViewModel ProfitVM { get; private set; }
        //    public TradesViewModel TradesVM { get; private set; }

        //    public OutputViewModel(IObservable<Prediction> prediction, IObservable<KeyValuePair<DateTime, double>> series, DispatcherService dispatcherservice)
        //    {

        //        var recs = RecommendationService.Link(series, prediction, dispatcherservice.Background);

        //        var ts = new TradeService3<Recommendation>(recs, a => a.Value, a => a.Weight, a => a.PriceAsDouble, a => a.PriceDateAsDateTime);

        //        ts.Trades.Subscribe(async _ => await FinanceSystem.DAL.Sqlite.ToDbAsync(_));

        //        TradesVM = new TradesViewModel(ts.Trades, FinanceSystem.DAL.Sqlite.SelectAllTrades().ToObservable(), dispatcherservice.UI);

        //        var cumulativeProfits = ts.RealisedProfits.Scan(new KeyValuePair<DateTime, double>(default(DateTime), 0d), (a, b) => new KeyValuePair<DateTime, double>(b.Key, b.Value + a.Value));

        //        ProfitVM = new ProfitViewModel(ts.RealisedProfits, ts.UnRealisedProfits, cumulativeProfits , dispatcherservice.UI);


        //    }














        //public OutputViewModel(IObservable<Tuple<KeyValuePair<DateTime, double>[], IEnumerable<KeyValuePair<DateTime, Tuple<double, double>[]>>>> series, DispatcherService dispatcherservice)
        //{


        //    series.SubscribeOn(dispatcherservice.New).ObserveOn(dispatcherservice.New).Subscribe(_d =>
        //    {
        //        var pss = new RecommendationService(_d.Item2, _d.Item1);

        //        var ts = new TradeService(pss.Recommendations);

        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            TradesVM = new CollectionViewModel<Trade>(ts.Trades, dispatcherservice.UI);
        //            NotifyChanged(nameof(TradesVM));
        //        });

        //        var prs = new ProfitService(ts.Trades);

        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            ProfitVM = new ProfitViewModel(prs.RealisedProfits, prs.UnRealisedProfits, prs.UnRealisedProfits, dispatcherservice.UI);
        //            NotifyChanged(nameof(ProfitVM));
        //        });


        //    },
        //      e => Console.WriteLine("error in observable"), () => Console.WriteLine("finished")
        //    );


        //}


        //public OutputViewModel(IEnumerable<KeyValuePair<DateTime, double>> o, IEnumerable<KeyValuePair<DateTime, Tuple<double, double>[]>> series, DispatcherService dispatcherservice)
        //{


        //    var pss = new RecommendationService(series,o);

        //    var ts = new TradeService(pss.Recommendations);

        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        TradesVM = new CollectionViewModel<Trade>(ts.Trades, dispatcherservice.UI);
        //        NotifyChanged(nameof(TradesVM));
        //    });

        //    var prs = new ProfitService(ts.Trades);

        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        ProfitVM = new ProfitViewModel(prs.RealisedProfits, prs.UnRealisedProfits, prs.CumulativeRealisedProfits, dispatcherservice.UI);
        //        NotifyChanged(nameof(ProfitVM));
        //    });


        //}




    }





    //static class Helper
    //{


    //    public static IEnumerable<KeyValuePair<DateTime, Tuple<double, double, double>>> GetPositions(this IEnumerable<KeyValuePair<DateTime, Tuple<double, double>[]>> x)
    //    {
    //        var positions = x
    //        .Select(g => new KeyValuePair<DateTime, Tuple<double, double, double>>(g.Key,
    //        Tuple.Create(g.Value[0].Item1, g.Value[0].Item1 - g.Value[0].Item2, g.Value[0].Item1 + g.Value[0].Item2)));

    //        return positions;
    //    }


    //    public static IEnumerable<KeyValuePair<DateTime, Tuple<double, double, double>>> GetVelocities(this IEnumerable<KeyValuePair<DateTime, Tuple<double, double>[]>> x)
    //    {
    //        var vels = x.Select(g =>
    //       new KeyValuePair<DateTime, Tuple<double, double, double>>(g.Key,
    //        Tuple.Create(g.Value[1].Item1, g.Value[0].Item1 - g.Value[0].Item2, g.Value[1].Item1 + g.Value[1].Item2)));

    //        return vels;
    //    }

    //    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int n)
    //    {
    //      return  source.Skip(Math.Max(0, source.Count() - n));
    //    }

    //}

}
