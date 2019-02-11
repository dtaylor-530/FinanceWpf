//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Threading;
//using UtilityWpf.ViewModel;
//using System.Reactive.Linq;
//using System.Reactive.Concurrency;
//using UtilityEnum;
//using UtilityReactive;
//using UtilityHelper;

//namespace FinanceSystem.ViewModel
//{
//    public class InputViewModel:IOutputViewModel<IObservable<KeyValuePair<DateTime, double>>>
//    {

//        public NumberBoxViewModel IterationsVM { get; private set; }
//        public ToggleViewModel<Flux> FluxVM { get; private set; }
//        public ButtonDefinitionsViewModel<IEnumerable<KeyValuePair<DateTime, double>>> ButtonsVM { get; set; }

//        public IObservable<IObservable<KeyValuePair<DateTime, double>>> Output { get; }

//        public InputViewModel(IScheduler background, Dispatcher  dispatcher)
//        {
//            var tsbs = ButtonDefinitionHelper.GetCommandOutput<IEnumerable<KeyValuePair<DateTime, double>>>(typeof(FinanceSystem.Service.EnumerableDataService));

//            ButtonsVM = new ButtonDefinitionsViewModel<IEnumerable<KeyValuePair<DateTime, double>>> (tsbs,dispatcher);

//            IterationsVM = new NumberBoxViewModel("Iterations", 20);
//            //FluxVM = new ToggleViewModel<Flux>();

//            Output = ButtonsVM.Output.Limit(IterationsVM.Output)
//                 .SubscribeOn(background).Select(c =>
//                c.Reschedule().ByTimeStamp());
       
//        }


//    }
//}


