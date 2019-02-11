//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TeaTime;


//namespace FinanceWpf.DAL
//{
//    public class TeaFile
//    {

//        //struct Tick
//        //{
//        //    public Time Time;
//        //    public double Price;
//        //    public int Volume;
//        //}
//        static readonly string dbName;

//        static readonly string providerName = "TeaFiles";

//        static TeaFile()
//        {
//            dbName = GetConnectionString();

//        }

//        static string GetConnectionString()
//        {

//            foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
//            {
//                //Console.WriteLine(c.ConnectionString);

//                if (c.ProviderName == providerName) 
//                return c.ConnectionString;
//            }
//            // defaults to bin directory
//            return "";
//            //throw new Exception($"Need to add connection string with a provider name of '{providerName}' to the app config file since it (seems to be) missing");
         
//        }


//        public static void ToDb(List<Model.Price> prices,string id)
//        {

//            if (File.Exists(dbName + "/" + id + ".tea"))
//                try
//                {
//                    using (var tf = TeaFile<Model.Price>.Append(id + ".tea"))
//                    {
//                        foreach (var x in prices)
//                            tf.Write(x);
//                    }
//                }
//                catch(Exception ex)
//                {
//                    Console.WriteLine("Error Writing file " +ex.Message);
//                }
//            else
//                // create file and write values
//                using (var tf = TeaFile<Model.Price>.Create(dbName + "/" + id + ".tea"))
//                {
//                    foreach (var x in prices)
//                        tf.Write(x);

//                }
//        }



//        public static List<Model.Price> FromDb(string id)
//        {

//            if (File.Exists(id + ".tea"))
//                try
//                {
//                    using (var tf = TeaFile<Model.Price>.OpenRead(id + ".tea"))
//                    {

//                        return tf.Items.ToList();
//                    }
//                }catch(Exception ex)
//                {
//                    Console.WriteLine("Error reading file "+ ex.Message);
//                    return null;
//                }
//            else
//                return null;
    
//        }




//        //// sum the prices of all items in the file
//        //using (var tf = TeaFile<Tick>.OpenRead("acme.tea"))
//        //{
//        //    return tf.Items.Sum(item => item.Price);
//        //}
//    }
//}
