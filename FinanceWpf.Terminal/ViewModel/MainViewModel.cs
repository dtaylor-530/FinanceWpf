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
    public class MainViewModel : ReactiveUI.ReactiveObject
    {

        ReadOnlyObservableCollection<Sector> sectors;
        public ReadOnlyObservableCollection<Sector> Sectors => sectors;

        private FinanceWpf.DAL.Column column = DAL.Column.Open;

        public FinanceWpf.DAL.Column Column
        {
            get => column;
            set => this.RaiseAndSetIfChanged(ref column, value);
        }

        private Stock stock;

        public Stock Stock
        {
            get => stock;
            set => this.RaiseAndSetIfChanged(ref stock, value);
        }
        private DateTime date1;

        public DateTime Date1
        {
            get => date1;
            set => this.RaiseAndSetIfChanged(ref date1, value);
        }

        private DateTime date2;

        public DateTime Date2
        {
            get => date2;
            set => this.RaiseAndSetIfChanged(ref date2, value);


        }
        readonly ObservableAsPropertyHelper<IEnumerable<DayMovement>> prices;

        public IEnumerable<DayMovement> Prices => prices.Value?.ToList();



        public MainViewModel()
        {
            var repo = new DAL.DataRepository();

            prices = this.WhenAnyValue(_ => _.Stock)
                .Where(_=>_!=null )
                .CombineLatest(this.WhenAnyValue(_ => _.Date1), this.WhenAnyValue(_ => _.Date2),(a,b,c)=>
                
               repo.GetStock(a.Key).Prices.ToList()/*.Where(__=>__.Date>b && __.Date<c)*/)
                .ToProperty(this, _ => _.Prices);

            this.WhenAnyValue(_ => _.Prices)
                   .Where(_ => _ != null)
                .Select(_ => new { first = _.First().Date, second = _.Last().Date })
                .Subscribe(_ => { Date1 = _.first; Date2 = _.second; });
            //    .SubscribeOn(Scheduler.Default)
            //    .Select(_ => new FinanceWpf.DAL.Repository().GetStock(_ + ".csv"))
            //    .ToProperty(this, _ => _.Stock);

            var obs1 =
               repo.GetSectors()
                .ToObservable(Scheduler.Default)
                .ToObservableChangeSet()
                .ObserveOnDispatcher()
                   .DisposeMany()
                .Bind(out sectors);
   
                 var dis1=obs1    .Subscribe();
            obs1.Take(1).Subscribe(_ =>
               Stock = sectors.First().Stocks.First());
        }

    }
}





