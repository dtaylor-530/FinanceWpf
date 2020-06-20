
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deedle;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Data;
using FinanceWpf.Common;
using FinanceWpf.Model;
using Net.Code.Csv;

namespace FinanceWpf.DAL
{
    public class DataRepository
    {
        const string raw = @"..\..\..\..\stocknet-dataset-master\price\raw";

        const string stockTable = @"..\..\..\..\stocknet-dataset-master\StockTable.csv";



        public IEnumerable<Sector> GetAllData(Column column) =>

             from x in
                 from s in GetSectors()
                 from st1 in s.Stocks
                 join st2
                 in GetStocks() on st1.Key equals st2.Key
                 select new { s.Key, stock = new Stock { Key = st1.Key, Name = st1.Name, Prices = st2.Prices } }
             group x by x.Key into g
             select new Sector { Key = g.Key, Stocks = g.Select(_ => _.stock).ToList() };




        private IEnumerable<Stock> GetStocks() => from x in Directory.EnumerateFiles(raw) select GetStock(x);

        public Stock GetStock(string fileName) => new Stock
        {
            Key = Path.GetFileNameWithoutExtension(raw + "\\" + fileName + ".csv"),
            Prices = ParseDayMovement(raw + "\\" + fileName + ".csv")
        };

        public Stock GetStock(string fileName, DateTime start, DateTime end) => new Stock
        {
            Key = Path.GetFileNameWithoutExtension(raw + "\\" + fileName + ".csv"),
            Prices = ParseDayMovement(raw + "\\" + fileName + ".csv").Where(_ => _.Date >= start && _.Date <= end)
        };


        public IEnumerable<Model.Sector> GetSectors() =>

                  from myRow in Frame.ReadCsv(stockTable).ToDataTable(new[] { "" }).AsEnumerable()
                  group myRow by myRow.Field<string>("Sector") into g
                  select new Model.Sector
                  {
                      Key = g.Key,
                      Stocks = g.Select(__ => new Model.Stock
                      {
                          Key = __.Field<string>("Symbol").Remove(0, 1),
                          Name = __.Field<string>("Company")
                      }).ToList()
                  };



        private static IEnumerable<DayMovement> ParseDayMovement(string fileName) =>
            from line in Csv.CsvReader.ReadFromText(File.ReadAllText(fileName))
            let daymovement = ReadLine(line)
            where daymovement != null
            select daymovement;



        private static DayMovement ReadLine(Csv.ICsvLine line)
        {
            try
            {
                return new DayMovement
                {
                    Open = System.Convert.ToDouble(line["Open"]),
                    Close = System.Convert.ToDouble(line["Close"]),
                    Adj_Close = System.Convert.ToDouble(line["Adj Close"]),
                    High = System.Convert.ToDouble(line["High"]),
                    Volume = System.Convert.ToDouble(line["Volume"]),
                    Low = System.Convert.ToDouble(line["Low"]),
                    Date = DateTime.Parse(line["Date"].ToString()),
                };
            }
            catch
            {
                return null;
            }

        }

    }
}

