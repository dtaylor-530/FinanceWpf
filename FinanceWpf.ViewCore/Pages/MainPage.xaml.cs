using FinanceWpf.Common;
//using FinanceWpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinanceWpf.Terminal
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainViewModel MainVM;
        public MainPage()
        {
            //var x = new Subject<IEnumerable<KeyValuePair<DateTime, double>>>();
            //Action<IEnumerable<KeyValuePair<DateTime, double>>> av = (a) => x.OnNext(a);
            //var xx = typeof(FinanceWpf.Service.EnumerableDataService).LoadMethods<IEnumerable<KeyValuePair<DateTime, double>>>().ToActions(av);
            //var x = new Subject<IEnumerable<KeyValuePair<DateTime, double>>>();
            //Action<IEnumerable<KeyValuePair<DateTime, double>>> av = (a) => x.OnNext(a);
            //var xx = typeof(FinanceWpf.Service.EnumerableDataService).LoadMethods<IEnumerable<KeyValuePair<DateTime, double>>>();
            //var d = new DispatcherService(Application.Current.Dispatcher);

            //InitializeComponent();


            //MainVM = new MainViewModel(/*xx,*/ d);


            //this.DataContext = MainVM;

        }
    }
}
