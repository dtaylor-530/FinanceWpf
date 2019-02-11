
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deedle;


namespace  FinanceWpf.DAL
{




   //public class DataSingleton
   // {
   //     private static DataSingleton instance;

   //     private DataSingleton()
   //     {
   //         data = GetData();
   //     }

   //     private Dictionary<string,Series<DateTime, double>> data;

   //     public Dictionary<string, Series<DateTime, double>> Data { get { return data; } }




   //     public static DataSingleton Instance
   //     {
   //         get
   //         {
   //             if (instance == null)
   //             {
   //                 instance = new DataSingleton();
   //             }
   //             return instance;
   //         }
   //     }


   //     private static Dictionary<string,Series<DateTime, double>> GetData()
   //     {
   //         Dictionary<string, Series<DateTime, double>> x = null;
   //         try
   //         {
   //           x= DataHelper.GetAllDataInResources().ToDictionary(_ => _.Key, _ => _.Value);
   //         }
   //         catch(IOException e)
   //         {
   //             Console.WriteLine("file is probably in use " + e.Message);
   //         }
   //         return x;
   //     }

   // }



}
