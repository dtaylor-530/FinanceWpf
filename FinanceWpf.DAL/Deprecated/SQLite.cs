//using FinanceWpf.Map;
//using FinanceWpf.Model;
//using FinanceWpf.Entity.SQL;
//using ProfitSystem.Model;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FinanceWpf.DAL
//{
//    public static class Sqlite
//    {

//        static readonly string dbName;


//        static Sqlite()
//        {
//            dbName = GetConnectionString();

//        }

//        static string GetConnectionString()
//        {

//            foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
//            {
//                //Console.WriteLine(c.ConnectionString);

//                if (c.ProviderName == "SQLite") ;
//                return c.ConnectionString;
//            }
//            throw new Exception("Need to add connection string with a provider name of 'SQLite' to app config file since it seems to be missing");


//        }

//        public static async Task<bool> ToDb(IEnumerable<Model.Trade> Trades)
//        {
//            try
//            {
//                var db = MakeAsyncConnection();
//                int success = 0;
//                foreach (Model.Trade Trade in Trades)
//                {
//                    try
//                    {
//                        //int rowsAffected = await db.UpdateAsync(Trade);
//                        //if (rowsAffected == 0)
//                        //{
//                            // The item does not exists in the database so lets insert it
//                            var rowsAffected = await db.InsertAsync(Trade);

//                        //}
//                        success += Convert.ToInt32(rowsAffected > 0);
//                    }
//                    catch
//                    {

//                    }


//                }
//                if (success == Trades.Count()) return true; else return false;
//            }
//            catch
//            {
//                return false;
//            }

//        }
//        public static async Task<bool> ToDbAsync(Model.Trade trade)
//        {
//            try
//            {

//                Entity.SQL.Trade tradeSQL = Map3.ToTradeSQL(trade);

//                var db = MakeAsyncConnection();

//                try
//                {
//                    //int rowsAffected = await db.UpdateAsync(tradeSQL);
//                    //if (rowsAffected == 0)
//                    //{
//                    // The item does not exists in the database so lets insert it
//                    await db.Table<Entity.SQL.Trade>().CountAsync().ContinueWith(async _ =>
//                    {
//                        tradeSQL.Id = _.Result;
//                        var rowsAffected = await db.InsertAsync(tradeSQL);
//                    });

             

//                    //}

//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e.Message);
//                    return false;
//                }

//                return true;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//                return false;
//            }

//        }


//        public static void ToDb(IEnumerable<Recommendation> recommendations)
//        {
//            using (var db = MakeConnection())
//            {
//                int success = 0;
//                foreach (var rec in recommendations)
//                {

//                    try
//                    {
//                        int rowsAffected = db.Update(rec);
//                        if (rowsAffected == 0)
//                        {
//                            // The item does not exists in the database so lets insert it
//                            rowsAffected = db.Insert(rec);

//                        }
//                        success += Convert.ToInt32(rowsAffected > 0);
//                    }
//                    catch
//                    {

//                    }

//                }
//            }


//        }

//        public static SQLite.SQLiteConnection MakeConnection()
//        {

//            return new SQLite.SQLiteConnection(dbName);


//        }

//        public static SQLite.SQLiteAsyncConnection MakeAsyncConnection()
//        {
//            return new SQLite.SQLiteAsyncConnection(dbName);


//        }
//        //public static bool MatchTradeByContractId(long id)
//        //{
//        //    var db = MakeConnection();

//        //    var x = db.Table<Trade>().Any(_ => _.ContractId == id);

//        //    db.Dispose();

//        //    return x;

//        //}


//        public static List<Model.Trade> SelectAllTrades()
//        {
//            using (var db = MakeConnection())
//                return db.Table<Entity.SQL.Trade>().ToList().Select(_ => _.ToTrade()).ToList();

//        }

//        public static List<Model.Trade> SelectTradesByParent(long id)
//        {
//            using (var db = MakeConnection())
//                return db.Table<Entity.SQL.Trade>().ToList().Select(_ => _.ToTrade()).ToList().Where(_=>_.ParentId==id).ToList();

//        }

//    }
//}
