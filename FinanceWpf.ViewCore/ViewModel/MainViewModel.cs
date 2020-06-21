using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using FinanceWpf.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using DynamicData;
using ScottPlot;
using UtilityModel;

namespace FinanceWpf.Terminal
{
    public class MainViewModel : ReactiveUI.ReactiveObject, IDisposable
    {

        ReadOnlyObservableCollection<Sector> sectors;
        private Stock stock;
        private DateTime date1;
        private DateTime date2;
        readonly ObservableAsPropertyHelper<DayMovement[]> dayMovements;
        readonly ObservableAsPropertyHelper<OHLC[]> prices;
        private CompositeDisposable compositeDisposable;


        public DateTime Date1
        {
            get => date1;
            set => this.RaiseAndSetIfChanged(ref date1, value);
        }

        public DateTime Date2
        {
            get => date2;
            set => this.RaiseAndSetIfChanged(ref date2, value);
        }

        public ReadOnlyObservableCollection<Sector> Sectors => sectors;

        public Stock Stock
        {
            get => stock;
            set => this.RaiseAndSetIfChanged(ref stock, value);
        }

        public OHLC[] Prices => prices.Value;
        public DayMovement[] DayMovements => dayMovements.Value;


        public MainViewModel()
        {
            var repo = new DAL.DataRepository();
            var s = this.WhenAnyValue(_ => _.Stock).Where(_ => _ != null);


            var p =
                 this.WhenAnyValue(_ => _.Date1).Where(_ => _ != default).CombineLatest(
                 this.WhenAnyValue(_ => _.Date2).Where(_ => _ != default), (a, b) =>
                      new DateRange(a, b)).WithLatestFrom(s, (a, b) => new { a, b })
                  .Select(_ => dayMovements.Value.Where(pr => pr.Date >= _.a.Start && pr.Date <= _.a.End).ToArray())
                  .Where(a => a.Length > 0);

            var p2 = s.Select(a => repo.GetStock(a.Key).Prices.ToArray()).Where(_ => _ != null);
            var p3 = p2.Merge(p).Select(a => a.ToArray());
            prices = p3.Select(a => a.Select(Map).ToArray()).ToProperty(this, a => a.Prices);

            dayMovements = p3.ToProperty(this, a => a.DayMovements);

            var dis2 = p3
                .Select(_ => new DateRange(_.First().Date, _.Last().Date)).DistinctUntilChanged()
                .Subscribe(_ => { Date1 = _.Start; Date2 = _.End; });

            var dis1 =
               repo.GetSectors()
                .ToObservable(RxApp.TaskpoolScheduler)
                .ToObservableChangeSet()
                .ObserveOnDispatcher()
                   .DisposeMany()
                .Bind(out sectors)
                .Subscribe(_ => Stock = sectors.First().Stocks.First());


            compositeDisposable = new CompositeDisposable(dis1, dis2);
        }

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }



        public OHLC Map(DayMovement dayMovement)
        {
            return new OHLC(dayMovement.Open, dayMovement.High, dayMovement.Low, dayMovement.Close, dayMovement.Date);

        }
    }
}






