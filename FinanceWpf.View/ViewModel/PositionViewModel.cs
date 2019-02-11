using DynamicData;
using FinanceWpf.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.ViewModel
{

    public class PositionViewModel : INotifyPropertyChanged
    {
        public decimal Size { get; private set; }
        public decimal Buy { get; private set; }
        public decimal Sell { get; private set; }
        public DateTime Date { get; private set; }
        public string Key { get; private set; }
        public int Count { get; private set; }
        public decimal Profit { get; private set; }

        public PositionViewModel(IGroup<Trade, string, string> group)
        {
            group.Cache.Connect().ToCollection()
               .Subscribe(__ =>
               {
                   Sell = __.Where(_ => _.Amount < 0).Sum(tt => (decimal)tt.Amount);
                   Buy = __.Where(_ => _.Amount > 0).Sum(tt => (decimal)tt.Amount);
                   Count = __.Count;
                   Size = Buy + Sell;
                   Date = __.Last().Date;
                   Key = group.Key;
                   Profit = __.Sum(_ => (decimal)_.Profit);
                   this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
               });

        }

        public PositionViewModel(IGroup<Trade, string, string> group,IObservable<Price> prices)
        {
            

            group.Cache.Connect().ToCollection().CombineLatest(prices.ToObservableChangeSet().ToCollection(),(a,b)=>new {a,b})
               .Subscribe(ab =>
               {
                   var __ = ab.a;

                   var __b= ab.b;



                   Sell = __.Where(_ => _.Amount < 0).Sum(tt => (decimal)tt.Amount);
                   Buy = __.Where(_ => _.Amount > 0).Sum(tt => (decimal)tt.Amount);
                   Count = __.Count;
                   Size = Buy + Sell;
                   Date = __.Last().Date;
                   Key = group.Key;
                   Profit = __.Sum(_ => (decimal)_.Profit);
                   this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
               });

        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
