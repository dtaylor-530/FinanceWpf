using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinanceWpf.Service
{
    using System.ComponentModel;
    using FinanceWpf.DAL;
    using FinanceWpf.Model;

    public class BasicService
    {
        private DataRepository repo;

        public BasicService()
        {

           repo = new DAL.DataRepository();
        }

        //[Description("Microsoft"), Category("Stock Data")]
        //public  IEnumerable<KeyValuePair<DateTime, double>> GetMicrosoftData()
        //{

        //    return repo.GetData(DAL.Column.Open)
        //           .SingleOrDefault(_ => _.Key == "Technology")
        //           .Stocks
        //           .SingleOrDefault(_ => _.Key == "MSFT").Prices
        //           .ObservationsAll
        //           .Where(_ => _.Value.HasValue)
        //           .Select(_ => new KeyValuePair<DateTime, double>(_.Key, _.Value.Value));

        //}
        //[Description("Facebook"), Category("Stock Data")]
        //public  IEnumerable<KeyValuePair<DateTime, double>> GetFaceBookData()
        //{
        //    repo.GetData(DAL.Column.Open)
        //                       .SingleOrDefault(_ => _.Key == "Technology")
        //                       .Stocks
        //                       .SingleOrDefault(_ => _.Key == "MSFT").Prices
        //                       .ObservationsAll
        //                       .Where(_ => _.Value.HasValue)
        //                       .Select(_ => new KeyValuePair<DateTime, double>(_.Key, _.Value.Value));


        //}
        //[Description("Cisco"), Category("Stock Data")]
        //public  IEnumerable<KeyValuePair<DateTime, double>> GetCiscoData()
        //{
        //    repo.GetData(DAL.Column.Open)
        //                      .SingleOrDefault(_ => _.Key == "Technology")
        //                      .Stocks
        //                      .SingleOrDefault(_ => _.Key == "MSFT").Prices
        //                      .ObservationsAll
        //                      .Where(_ => _.Value.HasValue)
        //                      .Select(_ => new KeyValuePair<DateTime, double>(_.Key, _.Value.Value));


        //}

        //[Description("Canadian Overseas Petroleum Limited"), Category("Stock Data")]
        //public IEnumerable<KeyValuePair<DateTime, double>> GetCanadianOverseasPetroleumLimitedData()
        //{
        //    repo.GetData(DAL.Column.Open)
        //                    .SingleOrDefault(_ => _.Key == "Technology")
        //                    .Stocks
        //                    .SingleOrDefault(_ => _.Key == "MSFT").Prices
        //                    .ObservationsAll
        //                    .Where(_ => _.Value.HasValue)
        //                    .Select(_ => new KeyValuePair<DateTime, double>(_.Key, _.Value.Value));

        //}
  
    }


}