using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Trady.Analysis.Extension;

namespace FinanceWpf.View
{
    /// <summary>
    /// Interaction logic for PlotView.xaml
    /// </summary>
    public partial class PlotView : UserControl
    {
        private OHLC[] data = null;

        static PlotView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlotView), new FrameworkPropertyMetadata(typeof(PlotView)));
        }

        public PlotView()
        {
        }

        public override void OnApplyTemplate()
        {
            WpfPlot = this.GetTemplateChild("PART_wpfPlot") as WpfPlot;
            if (data != null)
            {
                ds(this, data);
            }
        }

        private WpfPlot? WpfPlot { get; set; }

        public OHLC[] Data
        {
            get { return (OHLC[])GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(OHLC[]), typeof(PlotView), new PropertyMetadata(null, Changed));

        private static async void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlotView plotView && e.NewValue is OHLC[] data)
                if (plotView.WpfPlot != null)
                {
                    ds(plotView, data);
                }
                else
                {
                    plotView.data = data;
                }
        }

        static async void ds(PlotView plotView, OHLC[] data)
        {

            plotView.WpfPlot?.plt.Clear();
             _ = plotView.WpfPlot?.plt.PlotCandlestick(data);
             plotView.WpfPlot?.plt.Ticks(dateTimeX: true);
        
            await Bollinger(plotView.WpfPlot?.plt, data);
            await Atr(plotView.WpfPlot?.plt, data);
            await Adx(plotView.WpfPlot?.plt, data);
            plotView.WpfPlot?.plt.Axis(y1: 0);
            plotView.WpfPlot?.plt.Legend();
         plotView.WpfPlot?.Render();


            static Task Bollinger(Plot plt, OHLC[] arr)
            {
                if (plt != null)
                    return Task.Run(() => ScottPlot.Statistics.Finance.Bollinger(arr))
                     .ContinueWith(async a =>
                     {
                         var (sma, bolL, bolU) = (await a);
                         //double[] xs = DataGen.Consecutive(arr.Length);
                         //plt.PlotScatter(xs, bolL, color: Color.Blue, markerSize: 0);
                         //plt.PlotScatter(xs, bolU, color: Color.Blue, markerSize: 0);
                         //plt.PlotScatter(xs, sma, color: Color.Blue, markerSize: 0, lineStyle: LineStyle.Dash);

                     }, TaskScheduler.FromCurrentSynchronizationContext());
                return new Task(() => { });
            }

            static Task Atr(Plot plt, OHLC[] arr)
            {
                if (plt != null)
                    return Task.Run(() =>
                    {
                        var candles = arr.Select(a => new Trady.Core.Candle(DateTime.FromOADate(a.time), (decimal)a.open, (decimal)a.high, (decimal)a.low, (decimal)a.close, 10000)).ToArray();
                        try
                        {
                            var result = candles.Atr(20).Select(a => a.Tick).Where(a => a != null).Select(a => (double)a*10).ToArray();
                            return (arr.Select(c => c.time).ToArray(), result);
                        }
                        catch (Exception ex)
                        {
                            return (new double[0], new double[0]);
                        }
                    })
                  .ContinueWith(async a =>
                  {
                      var (x, y) = (await a);
                      try
                      {
                          plt.PlotScatter(x.Skip(x.Length-y.Length).ToArray(), y, label: nameof(OhlcvExtension.Atr));
                      }
                      catch(Exception ex)
                      {

                      }
                  }, TaskScheduler.FromCurrentSynchronizationContext());
                return new Task(() => { });
            }


            static Task Adx(Plot plt, OHLC[] arr)
            {
                if (plt != null)
                    return Task.Run(() =>
                    {
                        var candles = arr.Select(a => new Trady.Core.Candle(DateTime.FromOADate(a.time), (decimal)a.open, (decimal)a.high, (decimal)a.low, (decimal)a.close, 10000)).ToArray();
                        try
                        {
                            var result = candles.Adx(20).Select(a => a.Tick).Where(a => a != null).Select(a => (double)a).ToArray();
                            return (arr.Select(c => c.time).ToArray(), result);
                        }
                        catch (Exception ex)
                        {
                            return (new double[0], new double[0]);
                        }
                    })
                  .ContinueWith(async a =>
                  {
                      var (x, y) = (await a);
                      try
                      {
                          plt.PlotScatter(x.Skip(x.Length - y.Length).ToArray(), y, label: nameof(OhlcvExtension.Adx));
                      }
                      catch (Exception ex)
                      {

                      }
                  }, TaskScheduler.FromCurrentSynchronizationContext());
                return new Task(() => { });
            }
        }



    }
}