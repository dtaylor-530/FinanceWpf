

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Deedle;
using System.Reflection;

namespace  FinanceWpf.Common
{

    public static class PathHelper
    {
        public static string GetProjectPath()
        {

            return Directory.GetParent(
                     Directory.GetCurrentDirectory()).Parent.FullName;
        }


        public static string GetSolutionPath()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        }

        public static string GetCurrentExecutingDirectory()
        {
            string filePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
    }







    public static class TimeHelper
    {
        //    //const double scalefactor = 0.00002;


        //    public static IEnumerable<KeyValuePair<DateTime, double>> Reschedule(this IEnumerable<KeyValuePair<DateTime, double>> _)
        //    {
        //        var avdiff = _.Select(ac => ac.Key).ToList().SelectDifferences().Average();
        //        var scalefactor = ((double)TimeSpan.TicksPerSecond) / ((double)avdiff.Ticks * 10);
        //        var x = _.Select(s => new KeyValuePair<DateTime, double>(s.Key.Scale(scalefactor), s.Value));
        //        var y = x.Min(ad => ad.Key);
        //        var z = DateTime.Now - y;
        //        var ax = x.Select(dd => new KeyValuePair<DateTime, double>(dd.Key + z, dd.Value));

        //        return ax;
        //    }


        public static IEnumerable<KeyValuePair<DateTime, double>> ToDeviationFromMean(this IEnumerable<KeyValuePair<DateTime, double>> c_)
    {
        var x = c_.Select(d => d.Value).Average();

        return c_.Select(_ => new KeyValuePair<DateTime, double>(_.Key, _.Value - x));
    }

    //    public static IEnumerable<KeyValuePair<DateTime, double>> Add(this IEnumerable<KeyValuePair<DateTime, double>> c_, double d)
    //    {

    //        return c_.Select(_ => new KeyValuePair<DateTime, double>(_.Key, _.Value +d));
    //    }


    //    public static IEnumerable<TimeSpan> SelectDifferences(this IEnumerable<DateTime> sequence)
    //    {
    //        using (var e = sequence.GetEnumerator())
    //        {
    //            e.MoveNext();
    //            DateTime last = e.Current;
    //            while (e.MoveNext())
    //            {
    //                yield return e.Current - last;
    //                last = e.Current;
    //            }
    //        }
    //    }


    //    public static TimeSpan Average(this IEnumerable<TimeSpan> sourceList)
    //    {
    //        double doubleAverageTicks = sourceList.Average(timeSpan => timeSpan.Ticks);

    //        return new TimeSpan(Convert.ToInt64(doubleAverageTicks / 3));
    //    }




    //    public static DateTime Scale(this DateTime dt, double number)
    //    {
    //        return new DateTime((long)(dt.Ticks * number));
    //    }

    }


   
}
