using DynamicData;
using FinanceWpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.Terminal
{
    public class SecondaryViewModel
    {

        private ReadOnlyObservableCollection<object> trades;
        public ReadOnlyObservableCollection<object> Trades => trades;
        TradeConverter converter = new TradeConverter();

        public SecondaryViewModel()
        {
            var data = GetDataEnumerator();
           
            Observable.Generate(data.Current, value => data.MoveNext(), value => value, value => data.Current, value => TimeSpan.FromMilliseconds(1000))
            .ToObservableChangeSet()
            .ObserveOnDispatcher()
            .Bind(out trades)
            .Subscribe();
        }

        private IEnumerator<object> GetDataEnumerator() =>  new DAL.TradeRepository().GetAllData(100, false).Select(_ => converter.Convert(_, null, null, null)).GetEnumerator();
    }
}
