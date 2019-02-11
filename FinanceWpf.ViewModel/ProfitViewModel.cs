
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
//using UtilityWpf.ViewModel;

namespace FinanceSystem.ViewModel
{

    //public class ProfitViewModel : INPCBase
    //{

    //    public CollectionViewModel<KeyValuePair<DateTime, double>> RealisedProfits { get; private set; }
    //    public CollectionViewModel<KeyValuePair<DateTime, double>> UnRealisedProfits { get; private set; }
  
    //    public CollectionViewModel<KeyValuePair<DateTime, double>> CumulativeRealisedProfits { get; private set; }


    //    public ProfitViewModel( IObservable<KeyValuePair<DateTime, double>> realisedProfits, IObservable<KeyValuePair<DateTime, double>> unrealisedProfits, IScheduler ui )
    //    {

    //        RealisedProfits = new CollectionViewModel<KeyValuePair<DateTime, double>>(realisedProfits, ui);

    //        UnRealisedProfits = new CollectionViewModel<KeyValuePair<DateTime, double>>(unrealisedProfits, ui);

    //        var cumulativeProfits = realisedProfits.Scan(new KeyValuePair<DateTime, double>(default(DateTime), 0d), (a, b) => new KeyValuePair<DateTime, double>(b.Key, b.Value + a.Value));
    //        CumulativeRealisedProfits = new CollectionViewModel<KeyValuePair<DateTime, double>>(cumulativeProfits, ui);
    //    }
    //}




}
