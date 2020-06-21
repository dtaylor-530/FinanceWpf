using SQLite;
using System;
using UtilityInterface.Generic.Database;

namespace FinanceWpf.Entity.SQL
{

    public class Trade: UtilityInterface.NonGeneric.Database.IId, UtilityInterface.NonGeneric.Database.IChildRow
    {
        [PrimaryKey, NotNull]
        public long Id { get; set; }
        //[ForeignKey, NotNull]
        public long ParentId { get; set; }
        [NotNull]
        public long Date { get; set; }
        [NotNull]
        public long Price { get; set; }
        [NotNull]
        public long Amount { get; set; }
        [NotNull]
        public long Profit { get; set; }
    }

}
