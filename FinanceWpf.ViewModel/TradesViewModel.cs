using FinanceSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;


using System.Reactive.Linq;

namespace FinanceSystem.ViewModel
{
    //public class TradesViewModel //: INPCBase
    //{
    //    public TradeViewModel Current { get; }
    //    public TradeViewModel Past { get; }

    //    //public CollectionViewModel<Trade> Past { get; private set; }
    //    //public double Price { get; set; }
    //    //private List<double> prices=new List<double>();
    //   // public ChartLine ChartLine { get; set; }


    //    public TradesViewModel(IObservable<Trade> current, IObservable<Trade> past, IScheduler ui)
    //    {

    //        //RecommendationsVM = new CollectionViewModel<Recommendation>(recs, ui);

    //        Current = new TradeViewModel(current, ui);

    //        Past = new TradeViewModel(past, ui);

    //        //current.Subscribe(_ => { prices.Add(_.Price); Price = prices.Average(); NotifyChanged(nameof(Price)); });

    //        //var avs= current.Average(_ => _.Price).Zip(current, (a, b) => new KeyValuePair<DateTime, double>(b.Date, a));

    //        // ChartLine = new ChartLine(current.Select(_ => new KeyValuePair<DateTime, double>(_.Date, _.Price)), avs, true,ui);


    //    }



    //}





    //public class TradeViewModel
    //{
    //    public CollectionViewModel<Trade> Items { get; private set; }
    //    public ChartLine ChartLine { get; set; }
    //    public ValueViewModel<double> SellPosition { get; private set; }
    //    public ValueViewModel<double> BuyPosition { get; private set; }
    //    public ValueViewModel<double> AverageSellPosition { get; private set; }
    //    public ValueViewModel<double> AverageBuyPosition { get; private set; }


    //    public TradeViewModel(IObservable<Trade> trades, IScheduler ui)
    //    {

    //        Items = new CollectionViewModel<Trade>(trades, ui);

    //        fsas(trades, ui);


    //    }


    //    private void fsas(IObservable<Trade> trades, IScheduler ui)
    //    {
    //        //var buy = trades.Where(_ => _.Amount > 0);
    //        //var sell = trades.Where(_ => _.Amount < 0);


    //        //AverageSellAmount = new ValueViewModel<double>(UtilityReactive.Statistic.Average(sell, _ => _.Amount), ui);
    //        //AverageBuyAmount = new ValueViewModel<double>(UtilityReactive.Statistic.Average(buy, _ => _.Amount), ui);
    //        //UtilityReactive.Statistic.Average(trades.Where(_ => _.Amount > 0), _ => _.Amount).Subscribe(_ =>  
    //        //Console.WriteLine());
    //        //UtilityReactive.Statistic.Average(trades.Where(_ => _.Amount < 0), _ => _.Amount).Subscribe(_ => 
    //        //Console.WriteLine());



    //        AverageSellPosition = new ValueViewModel<double>(trades.Where(_ => _.Amount < 0).WeightedAverage(_ => _.Price, _ => _.Amount), ui);
    //        AverageBuyPosition = new ValueViewModel<double>(trades.Where(_ => _.Amount > 0).WeightedAverage(_ => _.Price, _ => _.Amount), ui);


    //        SellPosition = new ValueViewModel<double>(trades.Where(_ => _.Amount < 0).Select(v => v.Amount * v.Price).Scan(0d, (a, b) => a + b), ui);
    //        BuyPosition = new ValueViewModel<double>(trades.Where(_ => _.Amount > 0).Select(v => v.Amount * v.Price).Scan(0d, (a, b) => a + b), ui);

    //    }
    //}

}
