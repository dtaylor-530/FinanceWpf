using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.DAL
{
    public static class DataHelper
    {


        public static IEnumerable<double> GenerateRandomPrices()=>       UtilityMath.NoisyIterations.Brownian(
                factor: UtilityMath.RandomSingleton.Instance.Random.Next(0, 4),
                sigma: UtilityMath.RandomSingleton.Instance.Random.Next(1, 2), 
                rand: UtilityMath.RandomSingleton.Instance.Random);
        

        public static IEnumerable<DateTime> GenerateDateTimes(DateTime? dt=null)
        {
            dt =dt?? new DateTime(2000, 12, 12);
            while (true)
            {
                yield return (DateTime)dt;
                dt = dt + new TimeSpan(1, 1, 1);
            }
        }


        //http://scipy-cookbook.readthedocs.io/items/BrownianMotion.html
        public static IEnumerable<double> Brownian(double factor, double sigma, Random rand, double? value =null)
        {

            double value_ = value?? factor * MathNet.Numerics.Distributions.Normal.Sample(rand, 0, sigma);
            while (true)
            {
                yield return value_ += factor * MathNet.Numerics.Distributions.Normal.Sample(rand, 0, sigma);
            }

        }
    }
}
