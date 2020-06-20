using DynamicData;
using FinanceWpf.Model;

using FinanceWpf.ViewModel;
using NodaMoney;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UniRx;
using UtilityHelper;
using UtilityHelper.NonGeneric;




namespace FinanceWpf.View
{

    public class PositionTracker : Control
    {
        public static readonly DependencyProperty PricesProperty = DependencyProperty.Register("Prices", typeof(IEnumerable), typeof(PositionTracker), new PropertyMetadata(null, Changed));

        public static readonly DependencyProperty TradesProperty = DependencyProperty.Register("Trades", typeof(IEnumerable), typeof(PositionTracker), new PropertyMetadata(null, Changed));

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(string), typeof(PositionTracker), new PropertyMetadata("Date", Changed));

        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(string), typeof(PositionTracker), new PropertyMetadata("Price", Changed));

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(string), typeof(PositionTracker), new PropertyMetadata("key", Changed));

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(string), typeof(PositionTracker), new PropertyMetadata("Position", Changed));

        public static readonly DependencyProperty ProfitProperty = DependencyProperty.Register("Profit", typeof(string), typeof(PositionTracker), new PropertyMetadata("Profit", Changed));

        public static readonly DependencyProperty StartProperty = DependencyProperty.Register("Start", typeof(double), typeof(PositionTracker), new PropertyMetadata(100d, Changed));


        public IEnumerable Prices
        {
            get { return (IEnumerable)GetValue(PricesProperty); }
            set { SetValue(PricesProperty, value); }
        }

        public IEnumerable Trades
        {
            get { return (IEnumerable)GetValue(TradesProperty); }
            set { SetValue(TradesProperty, value); }
        }

        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        public string Position
        {
            get { return (string)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public string Profit
        {
            get { return (string)GetValue(ProfitProperty); }
            set { SetValue(ProfitProperty, value); }
        }


          Dictionary<string, Subject<object>> dict = typeof(PositionTracker).GetDependencyProperties().ToDictionary(_ => _.Name.Substring(0, _.Name.Length - 8), _ => new Subject<object>());
        private ReadOnlyObservableCollection<PositionViewModel> _data;

        public static readonly DependencyProperty PositionsProperty =
            DependencyProperty.Register("Positions", typeof(IEnumerable), typeof(PositionTracker), new PropertyMetadata(null));



        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PositionTracker).dict[e.Property.Name].OnNext(e.NewValue);
        }


        static PositionTracker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PositionTracker), new FrameworkPropertyMetadata(typeof(PositionTracker)));
        }

        ListBox ListBox = null;
        public override void OnApplyTemplate()
        {
            ListBox = this.GetTemplateChild("ListBox") as ListBox;
            ListBox.ItemsSource = _data;
            //_data.CollectionChangedAsObservable().Subscribe(_ =>
            //{

            //});
        }

        public PositionTracker()
        {

            dict[nameof(Trades)].Subscribe(_ =>
            {
            });
            var dispatcher = new DispatcherScheduler(this.Dispatcher);
            dict[nameof(Trades)].Where(_ => _ != null)
                .CombineLatest(dict[nameof(Date)].StartWith("Date"),
                dict[nameof(Key)].Where(_ => _ != null).StartWith("Key"),
                dict[nameof(Position)].Where(_ => _ != null).StartWith("Position"),
                   dict[nameof(Price)].Where(_ => _ != null).StartWith("Price"),
                      dict[nameof(Profit)].Where(_ => _ != null).StartWith("Profit"),
                (positions, date, key, position, price, profit) => new { positions, date, key, position, price, profit })
               //
               .Subscribe(_ =>
               GetChanges((IEnumerable)_.positions, (string)_.date, (string)_.key, (string)_.position, (string)_.price, (string)_.profit).
               Select(t => new Trade { Key = t.Key, Amount = (Money)t.Value.Value["Amount"], Price = (Money)t.Value.Value["Price"], Profit = (Money)t.Value.Value["Profit"], Date = t.Value.Key })
               .ToObservableChangeSet(c => c.Date + c.Key)
               .Group(g => g.Key)
               .Transform(t => new PositionViewModel(t))
              .ObserveOn(dispatcher)
               .Bind(out _data)
                .DisposeMany()
                .Subscribe(v =>
                {
                    //this.Dispatcher.InvokeAsync(() => ListBox.ItemsSource = _data,DispatcherPriority.Background);
                }));

        }





        private static IObservable<KeyValuePair<string, KeyValuePair<DateTime, Dictionary<string, decimal>>>> GetChanges(IEnumerable data, string date, string key, string value, string price, string profit)
        {
            Type type = null;
            PropertyInfo Date_ = null;
            PropertyInfo Key_ = null;
            PropertyInfo Value = null;
            PropertyInfo Price = null;
            PropertyInfo Profit = null;
            object lck = new object();

            return Observable.Create<KeyValuePair<string, KeyValuePair<DateTime, Dictionary<string, decimal>>>>(observer =>
            ((data as INotifyCollectionChanged)?.CollectionChangedAsObservable() ?? Observable.Empty<NotifyCollectionChangedEventArgs>())
            .ObserveOn(Scheduler.Default)
            .StartWith(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, data)).Where(_ => _.NewItems != null).Subscribe(_ =>
                             {

                                 if (data.Count() > 0)
                                 {
                                     if (type == null)
                                     {
                            
                                         type = data.First().GetType();
                                         var properties = type.GetProperties();
                                         Date_ = type.GetProperty(date);
                                         Key_ = type.GetProperty(key);
                                         Value = properties.FirstOrDefault(_a => _a.Name == value);
                                         Price = properties.FirstOrDefault(_a => _a.Name == price);
                                         Profit = properties.FirstOrDefault(_a => _a.Name == profit);
                                     }
                                     lock (lck)
                                     {
                                         foreach (var newitems in _.NewItems)
                                         {
                                             if (newitems is IEnumerable enumerable)
                                                 foreach (var __ in (enumerable).Cast<object>().ToArray())
                                                 {
                                                     observer.OnNext(new KeyValuePair<string, KeyValuePair<DateTime, Dictionary<string, decimal>>>(__.GetPropertyValue<string>(Key_),
                                                         new KeyValuePair<DateTime, Dictionary<string, decimal>>(newitems.GetPropertyValue<DateTime>(Date_),
                                                    new Dictionary<string, decimal>
                                                    {
                                                        { Value?.Name, Value!=null?newitems.GetPropertyValue<decimal>(Value):0 },
                                                                     { Price?.Name, Price!=null?newitems.GetPropertyValue<decimal>(Price):0 },
                                                                { Profit?.Name, Profit!=null?newitems.GetPropertyValue<decimal>(Profit):0 }

                                                    })));
                                                 }
                                             else
                                                 observer.OnNext(new KeyValuePair<string, KeyValuePair<DateTime, Dictionary<string, decimal>>>(newitems.GetPropertyValue<string>(Key_),
                                                    new KeyValuePair<DateTime, Dictionary<string, decimal>>(newitems.GetPropertyValue<DateTime>(Date_),
                                                    new Dictionary<string, decimal>
                                                    {
                                                        { Value?.Name, Value!=null?newitems.GetPropertyValue<decimal>(Value):0 },
                                                                     { Price?.Name, Price!=null?newitems.GetPropertyValue<decimal>(Price):0 },
                                                                { Profit?.Name, Profit!=null?newitems.GetPropertyValue<decimal>(Profit):0 }
                                                    })));
                                         }
                                     }
                                 }

                             }));
        }



        //private static IObservable<KeyValuePair<string, KeyValuePair<DateTime, Tuple<double, double>>>> GetChanges(IEnumerable data, string date, string key, string value, string value2)
        //{
        //    Type type = null;
        //    PropertyInfo Date_ = null;
        //    PropertyInfo Key_ = null;
        //    PropertyInfo Value = null;
        //    PropertyInfo Value2 = null;

        //    return Observable.Create<KeyValuePair<string, KeyValuePair<DateTime, Tuple<double, double>>>>(observer => ((data as INotifyCollectionChanged)?.CollectionChangedAsObservable() ?? Observable.Empty<NotifyCollectionChangedEventArgs>())
        //                  .StartWith(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, data)).Subscribe(_ =>
        //                  {

        //                      if (data.Count() > 0)
        //                      {
        //                          if (type == null)
        //                          {
        //                              type = data.First().GetType();
        //                              Date_ = type.GetProperty(date);
        //                              Key_ = type.GetProperty(key);
        //                              Value = type.GetProperty(value);
        //                              Value2 = type.GetProperty(value2);
        //                          }
        //                          foreach (var __ in _.NewItems)
        //                          {
        //                              observer.OnNext(new KeyValuePair<string, KeyValuePair<DateTime, Tuple<double, double>>>(__.GetPropertyValue<string>(Key_),
        //                                  new KeyValuePair<DateTime, Tuple<double, double>>(__.GetPropertyValue<DateTime>(Date_), Tuple.Create(__.GetPropertyValue<double>(Value), __.GetPropertyValue<double>(Value2)))));
        //                          }

        //                      }

        //                  }));
        //}


    }



    //public class TradesPosition
    //{
    //    private readonly int _count;

    //    public TradesPosition(decimal buy, decimal sell, int count)
    //    {
    //        Buy = buy;
    //        Sell = sell;
    //        _count = count;
    //        Position = Buy - Sell;
    //    }

    //    public bool Negative => Position < 0;


    //    public decimal Position { get; }
    //    public decimal Buy { get; }
    //    public decimal Sell { get; }
    //    public string CountText => "Order".Pluralise(_count);
    //}
}