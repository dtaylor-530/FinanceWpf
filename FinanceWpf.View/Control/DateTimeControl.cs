using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FinanceWpf.View
{
    public class DateTimeControl:Control
    {

        public DateTime Start
        {
            get { return (DateTime)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Start.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(DateTime), typeof(DateTimeControl), new PropertyMetadata(default(DateTime),StartChanged));


        ISubject<DateTime> startChanges = new Subject<DateTime>();
        private static void StartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DateTimeControl).startChanges.OnNext((DateTime)e.NewValue);
        }

        public DateTime End
        {
            get { return (DateTime)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for End.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(DateTime), typeof(DateTimeControl), new PropertyMetadata(default(DateTime),EndChanged));


        ISubject<DateTime> endChanges = new Subject<DateTime>();

        private static void EndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
         (d as DateTimeControl).  endChanges.OnNext((DateTime)e.NewValue);
        }


        public DateTimeControl()
        {
            startChanges.CombineLatest(endChanges, (s, e) => new { s, e }).Subscribe(_ =>
                   {

                   });

        }
    }
}
