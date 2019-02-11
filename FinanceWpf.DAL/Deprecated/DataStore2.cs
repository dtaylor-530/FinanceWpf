using Deedle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  FinanceWpf.DAL
{


    //public static class DataAccess
    //{
    //    public static IEnumerable<Model.Sector> GetAllSectors()
    //    {

    //        foreach (var sctr in DataSingleton3.Instance.Data)
    //        {
    //            foreach(var stk in sctr.Stocks)
    //            {
    //                var x = DataSingleton2.Instance.Data.FirstOrDefault(_ => stk.Key== _.Key);
    //                 stk.Prices = x.Value.Keys.Zip(x.Value.Values,(a,b)=>new KeyValuePair<DateTime, double>(a,b)).ToList();
    //            }
    //            yield return sctr;
    //        }
    //    }



    //}










    //internal class DataSingleton2
    //{
    //    private static DataSingleton2 instance;

    //    private DataSingleton2()
    //    {
    //        data = GetData();
    //    }

    //    private Dictionary<string, Series<DateTime, double>> data;

    //    public Dictionary<string, Series<DateTime, double>> Data { get { return data; } }




    //    public static DataSingleton2 Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new DataSingleton2();
    //            }
    //            return instance;
    //        }
    //    }


    //    private static Dictionary<string, Series<DateTime, double>> GetData()
    //    {
    //        Dictionary<string, Series<DateTime, double>> x = null;
    //        try
    //        {
    //            x = DataHelper.GetAllDataInResources2().ToDictionary(_ => _.Key, _ => _.Value);
    //        }
    //        catch (IOException e)
    //        {
    //            Console.WriteLine("file is probably in use " + e.Message);
    //        }
    //        return x;
    //    }

    //}



    //internal class DataSingleton3
    //{
    //    private static DataSingleton3 instance;

    //    private DataSingleton3()
    //    {
    //        data = GetData();
    //    }

    //    private ICollection<Model.Sector> data;

    //    public ICollection<Model.Sector> Data { get { return data; } }




    //    public static DataSingleton3 Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new DataSingleton3();
    //            }
    //            return instance;
    //        }
    //    }


    //    private static ICollection<Model.Sector> GetData()
    //    {
    //        ICollection<Model.Sector> x = null;
    //        try
    //        {
    //            x = DataHelper.GetAllDataInResources3().ToList();
    //        }
    //        catch (IOException e)
    //        {
    //            Console.WriteLine("file is probably in use " + e.Message);
    //        }
    //        return x;
    //    }

    //}

}
