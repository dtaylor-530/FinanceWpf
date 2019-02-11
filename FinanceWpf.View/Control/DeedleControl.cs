using Deedle;
using OxyPlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UtilityHelper;
using UtilityHelper.NonGeneric;

namespace FinanceWpf.View
{

    public class DeedleControl : Control
    {

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }


        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(DeedleControl), new PropertyMetadata(null, Changed));




        public DateTime Lower
        {
            get { return (DateTime)GetValue(LowerProperty); }
            set { SetValue(LowerProperty, value); }
        }


        public static readonly DependencyProperty LowerProperty = DependencyProperty.Register("Lower", typeof(DateTime), typeof(DeedleControl), new PropertyMetadata(default(DateTime), Changed));


        public DateTime Upper
        {
            get { return (DateTime)GetValue(UpperProperty); }
            set { SetValue(UpperProperty, value); }
        }


        public static readonly DependencyProperty UpperProperty = DependencyProperty.Register("Upper", typeof(DateTime), typeof(DeedleControl), new PropertyMetadata(default(DateTime), Changed));



        public int DiffIndex
        {
            get { return (int)GetValue(DiffIndexProperty); }
            set { SetValue(DiffIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DiffIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiffIndexProperty =
            DependencyProperty.Register("DiffIndex", typeof(int), typeof(DeedleControl), new PropertyMetadata(0,Changed));



        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(DeedleControl), new PropertyMetadata(null, Changed));


        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(string), typeof(DeedleControl), new PropertyMetadata(null, Changed));

        Dictionary<string, Subject<object>> dict = typeof(DeedleControl).GetDependencyProperties().ToDictionary(_ => _.Name.Substring(0, _.Name.Length - 8), _ => new Subject<object>());
        private Series<DateTime, double> data;

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DeedleControl).dict[e.Property.Name].OnNext(e.NewValue);
        }


        static DeedleControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeedleControl), new FrameworkPropertyMetadata(typeof(DeedleControl)));
        }


        public static readonly DependencyProperty PlotModelProperty = DependencyProperty.Register("PlotModel", typeof(PlotModel), typeof(DeedleControl), new PropertyMetadata(null));



        public DeedleControl()
        {

            Uri resourceLocater = new Uri("/FinanceWpf.View;component/Themes/Generic.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["DeedleControl"] as Style;

            var xx = dict[nameof(Data)]
        .CombineLatest(
        dict[nameof(Date)].StartWith("Date"),
        dict[nameof(Value)].StartWith("Value"),
        (data, date, value) => Task.Run(() => GetData(data, (string)date, (string)value)))
        .Subscribe(async _ =>
        {
            data = await _;
            await ChangePlotModel(data);
        });

            var xx2 = dict[nameof(Upper)].CombineLatest(dict[nameof(Lower)], (upper, lower) =>Task.Run(() => data?.Between((DateTime)lower, (DateTime)upper)))
            .Subscribe(async _ => await ChangePlotModel(await _));


            var xx3=dict[nameof(DiffIndex)].Select(_=>Task.Run(()=>data?.Diff((int)_)))
            .Subscribe(async _ => await ChangePlotModel(await _));


        }

        private async Task ChangePlotModel(Series<DateTime, double> data)
        {
            if (data != null)
                await Task.Run(() => GetPlotModel(data)).ContinueWith(__ =>
                   {
                       this.Dispatcher.InvokeAsync(async () => this.SetValue(PlotModelProperty, await __), System.Windows.Threading.DispatcherPriority.Background);
                   });


        }
        //public static Series<DateTime, double> GetData(string filePath, string column = "open")
        //{

        //    var frame = Deedle.Frame.ReadCsv(filePath);
        //    string key = frame.ColumnKeys.SingleOrDefault(_ => String.Equals(_, "date", StringComparison.CurrentCultureIgnoreCase));
        //    if (key == null)
        //        throw new Exception("no date column in data");
        //    var frameDate = frame.IndexRows<DateTime>(key).SortRowsByKey();
        //    return frameDate.GetColumn<double>(column);

        //}

        public static PlotModel GetPlotModel(Series<DateTime, double> series)
        {
            var model = new PlotModel() { LegendSymbolLength = 24 };
            var s1 = new OxyPlot.Series.LineSeries()
            {
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.Square,
                MarkerSize = 2,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Blue,
                MarkerStrokeThickness = 1.5
            };
            model.Series.Add(s1);
            
            //
            foreach (var s in series.Observations)
            {
                s1.Points.Add(new DataPoint((s.Key - default(DateTime)).TotalHours, s.Value));
            }
            return model;
        }

        public static Series<DateTime, double> GetData(object data, string dateTime, string value)

        {
            if (data is Series<DateTime, double> series)
                return series;
            else if (data is IEnumerable data_)
            {
                var type = data_.First().GetType();
                var values = data_.Cast<object>().ToList();
                return
                      new Series<DateTime, double>(
                      from dat in values select dat.GetPropValue<DateTime>(dateTime, type),
                      from dat in values select dat.GetPropValue<double>(value, type));
            }
            else
                return null;
        }





    }

}
