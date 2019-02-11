
using System.Collections.Generic;
using System.Linq;


namespace  FinanceWpf.Service
{
    public class ConnectableDataService
    {


        //public static IConnectableObservable<KeyValuePair<DateTime, double>> GetAppleData()
        //{

        //    return DataHelper.GetDataBySymbol("MSFT")
        //        .Select(_ => new KeyValuePair<DateTime, double>(_.Key.Scale(0.000002), _.Value))
        //        .ByTimeStamp()
        //        .Publish();

        //}

        //public static IObservable<KeyValuePair<DateTime, double>> GetFaceBookData()
        //{
        //    return DataHelper.GetDataBySymbol("FB")
        //         .Select(_ => new KeyValuePair<DateTime, double>(_.Key.Scale(0.000002), _.Value))
        //         .ByTimeStamp()
        //         .Publish();


        //}



        //public static IObservable<KeyValuePair<DateTime, double>> GetCiscoData()
        //{
        //    return DataHelper.GetDataBySymbol("CSCO")
        //     .Select(_ => new KeyValuePair<DateTime, double>(_.Key.Scale(0.000002), _.Value))
        //     .ByTimeStamp()
        //     .Publish();


        //}



    }


}
