using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.Common
{

    public static class DateTimeHelper
    {

        public static DateTime Scale(this DateTime dt, double number)
        {
            return new DateTime((long)(dt.Ticks * number));
        }

    }


}

