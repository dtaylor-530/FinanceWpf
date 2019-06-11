using FinanceWpf.Model;
using NodaMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.DAL
{


    public class TradeRepository : IDisposable
    {

        private readonly IDisposable _cleanUp;
        private readonly object _locker = new object();
        private TradeFactory tradeFactory;

        public TradeRepository()
        {
            tradeFactory = new TradeFactory();
        }

        public IEnumerable<Trade> GetAllData(int numberToGenerate, bool initialLoad = false)
        {
            IEnumerable<Trade> result;
            lock (_locker)
            {
                result = Enumerable.Range(1, numberToGenerate).Select(_ => tradeFactory.NewTrade());
            }
            return result;
        }



        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }

}
