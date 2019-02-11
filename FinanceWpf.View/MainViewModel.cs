using FinancialDataPrediction.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using UtilityWpf.ViewModel;

namespace FinancialDataPrediction.Terminal
{
    public class MainViewModel
    {

        public CollectionViewModel<ButtonDefinition> ButtonsVM { get; set; }

        public OutputViewModel OutputVM { get; set; }


        public MainViewModel(Dispatcher dispatcher)
        {

            Controller c = new Controller();

            ButtonsVM = new CollectionViewModel<ButtonDefinition>(c.Buttons);

            OutputVM = new OutputViewModel(c.TimeValueSeriesViewModels, c.Measurements);


        }

    }




}

