using SQLite;
using System;
using UtilityInterface.Generic.Database;

namespace FinanceWpf.Entity.SQL
{

    public class Trade: UtilityInterface.NonGeneric.Database.IId, UtilityInterface.NonGeneric.Database.IChildRow
    {
        [PrimaryKey, NotNull]
        public Int64 Id { get; set; }
        //[ForeignKey, NotNull]
        public Int64 ParentId { get; set; }
        [NotNull]
        public Int64 Date { get; set; }
        [NotNull]
        public Int64 Price { get; set; }
        [NotNull]
        public Int64 Amount { get; set; }
        [NotNull]
        public Int64 Profit { get; set; }
    }

}
