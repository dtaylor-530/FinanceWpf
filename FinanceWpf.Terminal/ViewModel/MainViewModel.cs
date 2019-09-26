using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Reactive.Linq;
using FinanceWpf.Common;

using UtilityHelper;
using System.Reactive.Disposables;
using System.Windows;
using FinanceWpf.Model;
using FinanceWpf.Map;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using DynamicData;

namespace FinanceWpf.Terminal
{
    public class MainViewModel : ReactiveUI.ReactiveObject, IDisposable
    {



        ReadOnlyObservableCollection<Sector> sectors;
        private Stock stock;
        private DateTime date1;
        private DateTime date2;
        readonly ObservableAsPropertyHelper<IEnumerable<DayMovement>> prices;
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

        public IEnumerable<DayMovement> Prices => prices.Value?.ToList();


        public MainViewModel()
        {
            var repo = new DAL.DataRepository();
            var s = this.WhenAnyValue(_ => _.Stock).Where(_ => _ != null);

    
            var p =
                 this.WhenAnyValue(_ => _.Date1).Where(_ => _ != default(DateTime)).CombineLatest(
                 this.WhenAnyValue(_ => _.Date2).Where(_=>_!=default(DateTime)), (a, b) =>
                  new DateRange { Start = a, End = b }).WithLatestFrom(s, (a, b) => new { a, b })
                  .Select(_ => Prices.Where(pr => pr.Date >= _.a.Start && pr.Date <= _.a.End));
                
            var p2 = s.Select(a=>  repo.GetStock(a.Key).Prices.ToList()).Where(_ => _ != null);
            var p3 = p2.Merge(p);
            prices = p3.ToProperty(this, _ => 
            _.Prices);

            var dis2 = p3
                .Select(_ => new DateRange { Start = _.First().Date, End = _.Last().Date }).DistinctUntilChanged()
                .Subscribe(_ => { Date1 = _.Start; Date2 = _.End; });

            var dis1 =
               repo.GetSectors()
                .ToObservable(Scheduler.Default)
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


        class DateRange:IEquatable<DateRange>
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }

            public bool Equals(DateRange other)
            {
               return Start == other.Start && End == other.End;
            }
        }
    }
}





