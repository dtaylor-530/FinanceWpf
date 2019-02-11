using AutoMapper;
using FinanceWpf.Model;
using FinanceWpf.Entity.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityHelper;

namespace FinanceWpf.Map
{


    public static class ModelMap
    {

        static IMapper mapper;

        static ModelMap()
        {
            mapper = new MapperConfiguration(AutoMapperExpressionFactory.CreateTradeExpression).CreateMapper();
        }

        public static IEnumerable<Model.Trade> ToTrades(this IEnumerable<Entity.SQL.Trade> trades)
        {
            return mapper.Map<IEnumerable<Entity.SQL.Trade>, IEnumerable<Model.Trade>>(trades);
        }

        public static Model.Trade ToTrade(this Entity.SQL.Trade trade)
        {
            return mapper.Map<Entity.SQL.Trade, Model.Trade>(trade);
        }

    }


    public static class EntityMap
    {

        static IMapper mapper;

        static EntityMap()
        {

            mapper = new MapperConfiguration(AutoMapperExpressionFactory.CreateTradeSQLExpression).CreateMapper(); ;
        }


        public static Entity.SQL.Trade ToTradeSQL(this Model.Trade trade)
        {
            return mapper.Map<Model.Trade, Entity.SQL.Trade>(trade);
        }
    }
}
