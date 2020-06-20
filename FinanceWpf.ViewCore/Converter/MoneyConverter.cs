using FinanceWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FinanceWpf.Terminal
{
    public class TradeConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Trade obj = value as Trade;
            return new { Amount = obj.Amount.Amount, obj.Date, obj.Key, Price = obj.Price.Amount,Profit=obj.Profit.Amount };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
