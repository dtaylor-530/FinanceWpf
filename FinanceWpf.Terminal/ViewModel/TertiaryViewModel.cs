﻿
using FinanceWpf.Common;
using FinanceWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceWpf.Map;
using DynamicData;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;

namespace FinanceWpf.Terminal
{
    public class TertiaryViewModel : ReactiveUI.ReactiveObject
    {

        //  private ReadOnlyObservableCollection<KeyValuePair<(DateTime, DateTime), List<Trade>>> _data;

        public ObservableCollection<KeyValuePair<(DateTime, DateTime), List<Trade>>> Data { get; set; }

        private ReadOnlyObservableCollection<Trade> _trades;
        public ReadOnlyObservableCollection<Trade> Trades => _trades;


        private ReadOnlyObservableCollection<Price> _prices;
        public ReadOnlyObservableCollection<Price> Prices => _prices;


        public TertiaryViewModel()
        {

            var prices = new DAL.PriceRepository().GetAllData(100, false).GetEnumerator();
            Random rnd = new Random();
            var tf = new DAL.TradeFactory();


            var oprices = Observable.Generate(prices.Current, value => prices.MoveNext(), value => value, value => prices.Current, value => TimeSpan.FromMilliseconds(500)).Publish().RefCount();

            var randomprices = from op in oprices
                               where rnd.NextDouble() > 0.5
                               select op;

            randomprices.ToObservableChangeSet()
            .ObserveOnDispatcher()
            .Bind(out _prices)
            .Subscribe();


            var randomtrades = from op in oprices
                               where rnd.NextDouble() > 0.5
                               select tf.NewTrade(op);


            randomtrades
                .ToObservableChangeSet()
                .ObserveOnDispatcher()
                .Bind(out _trades)
                .Subscribe();


            var randomprices2 = randomprices.ToObservableChangeSet(_ => _.Date + _.Key);


            randomtrades.ToObservableChangeSet(_ => _.Date + _.Key).ToCollection()
             .CombineLatest(randomprices2.ToCollection(), (a, b) => new { a, b }).Select(_ =>
                  {
                      var x = Combine(_.a, _.b);
                      return x;

                  }).ObserveOnDispatcher().Where(_ => _ != null).Subscribe(_ =>
                      {
                          Data = new ObservableCollection<KeyValuePair<(DateTime, DateTime), List<Trade>>>(_);
                          this.RaisePropertyChanged(nameof(Data));
                      });

        }



        private IEnumerable<KeyValuePair<(DateTime, DateTime), List<Trade>>> Combine(IReadOnlyCollection<Trade> collection, IReadOnlyCollection<Price> collection2)
        {
            var combined = collection2.Take(collection2.Count - 1).Zip(collection2.Skip(1), (a, b) => new { a, b });

            var dxx = from dp in collection
                      let catIds =
                      from xx in from ty in
                                     from dk in combined
                                     where predicate(dp.Date, dk.a.Date, dk.b.Date)
                                     select new { key = (dk.a.Date, dk.b.Date), dp }
                                 group ty by ty.key
                      select new KeyValuePair<(DateTime, DateTime), List<Trade>>(xx.Key, xx.Select(_ => _.dp).ToList())
                      select catIds.FirstOrDefault();
            return dxx;

        }


        private bool predicate(DateTime date, DateTime date1, DateTime date2) => date >= date1 && date < date2;
    }
}

