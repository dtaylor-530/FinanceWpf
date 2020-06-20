using FinanceWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace FinanceWpf.Terminal
{
    public class TertiaryViewModel : ReactiveUI.ReactiveObject
    {


        public IEnumerable<KeyValuePair<(DateTime, DateTime), List<Trade>>> Data => data.Value;
        private ObservableAsPropertyHelper<IEnumerable<KeyValuePair<(DateTime, DateTime), List<Trade>>>> data;

        private ReadOnlyObservableCollection<Trade> _trades;
        public ReadOnlyObservableCollection<Trade> Trades => _trades;


        private ReadOnlyObservableCollection<Price> _prices;
        public ReadOnlyObservableCollection<Price> Prices => _prices;



        public TertiaryViewModel()
        {

            var prices = new DAL.PriceRepository().GetAllData(100, false).GetEnumerator();
            Random rnd = new Random();
            var tf = new DAL.TradeFactory();


            var oprices = Observable.Generate(prices.Current, value => prices.MoveNext(), value => value, value => prices.Current, value => TimeSpan.FromMilliseconds(1500)).Publish().RefCount();

            var randomprices = (from op in oprices
                               where rnd.NextDouble() > 0.5
                               select op).ToObservableChangeSet(_ => _.Date + _.Key);

            randomprices
            .ObserveOnDispatcher()
            .Bind(out _prices)
            .Subscribe();


            var randomtrades = (from op in oprices
                               where rnd.NextDouble() > 0.5
                               select tf.NewTrade(op)).ToObservableChangeSet(_ => _.Date + _.Key);

            randomtrades
                .ObserveOnDispatcher()
                .Bind(out _trades)
                .Subscribe();

            data= randomtrades.ToCollection()
             .CombineLatest(randomprices.ToCollection(), (a, b) => new { a, b })
             .Select(_ => Combine(_.a, _.b))
             .ObserveOnDispatcher().Where(_ => _ != null)
             .ToProperty(this, _ => _.Data);


        }



        private IEnumerable<KeyValuePair<(DateTime, DateTime), List<Trade>>> Combine(IReadOnlyCollection<Trade> collection, IReadOnlyCollection<Price> collection2)
        {
            var groupcombined = collection2.ToLookup(_ => _.Key);
            var combined = groupcombined.ToDictionary(_ => _.Key, _ => _.Take(groupcombined[_.Key].Count() - 1).Zip(groupcombined[_.Key].Skip(1), (a, b) => new { a, b }).ToArray());
            var group = collection.ToLookup(_ => _.Key);

            return group.SelectMany(_ =>
            {
                if (combined.ContainsKey(_.Key) && combined[_.Key].Any())
                {
                    var dxx = 
                              from dp in _.ToList()
                              let catIds =
                              from xx in from ty in
                                             from dk in combined[_.Key]
                                             where predicate(dp.Date, dk.a.Date, dk.b.Date)
                                             select new { key = (dk.a.Date, dk.b.Date), dp }
                                         group ty by ty.key
                              select new KeyValuePair<(DateTime, DateTime), List<Trade>>(xx.Key, xx.Select(__ => __.dp).ToList())
                              select catIds.FirstOrDefault();
                       
                    return dxx.Where(ac=>ac.Value?.Any()!=null? ac.Value.Any():false);
                }
                return new List<KeyValuePair<(DateTime, DateTime), List<Trade>>>();
            });
        }


        private bool predicate(DateTime date, DateTime date1, DateTime date2) => date >= date1 && date < date2;
    }
}

