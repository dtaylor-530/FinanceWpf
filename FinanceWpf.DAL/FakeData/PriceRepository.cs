using FinanceWpf.Model;
using NodaMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.DAL
{


    public class PriceRepository
    {
        private readonly object _locker = new object();
        private readonly PriceFactory priceFactory;

        public PriceRepository()
        {
            priceFactory = new PriceFactory();
        }

        public IEnumerable<Price> GetAllData(int numberToGenerate, bool initialLoad = false)
        {
            IEnumerable<Price> result;
            lock (_locker)
            {
                result = Enumerable.Range(1, numberToGenerate).Select(_ => priceFactory.NewPrice());
            }
            return result;
        }



   
    }

}
