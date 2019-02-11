using FinanceWpf.DAL;
using FinanceWpf.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.Service
{
    public class Service
    {
        private DataRepository repo;

        public Service()
        {
            repo = new DAL.DataRepository();
        }


        public IEnumerable<Sector> GetOpenData()
        {

            return repo.GetAllData(DAL.Column.Open);


        }

        public IEnumerable<Sector> GetCloseData()
        {

            return repo.GetAllData(DAL.Column.Close);


        }
    }
}