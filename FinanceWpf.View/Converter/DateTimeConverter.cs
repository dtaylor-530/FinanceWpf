using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UtilityHelper;
using UtilityHelper.NonGeneric;

namespace FinanceWpf.Terminal
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lst = (value as IEnumerable).Cast<object>();
           var first= lst.First().GetPropertyValue<DateTime>(parameter.ToString());
            var last=lst.First().GetPropertyValue<DateTime>(parameter.ToString());
            return new Tuple<DateTime, DateTime>(first, last);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse(value.ToString());
        }
    }
}
