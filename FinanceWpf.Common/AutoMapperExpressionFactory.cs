using AutoMapper;
using FinanceWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWpf.Map
{

    public class AutoMapperExpressionFactory
    {

        public class TicksToDateTimeConverter : ITypeConverter<long, DateTime>
        {
            public DateTime Convert(long source, DateTime destination, ResolutionContext context)
            {
                return new DateTime(source); // interpret long as Ticks
            }
        }

        public class DateTimeToTicksConverter : ITypeConverter< DateTime,long>
        {
            public long Convert(DateTime source, long destination, ResolutionContext context)
            {
                return source.Ticks; // interpret long as Ticks
            }
        }



        public class LongToDoubleConverter : ITypeConverter<Int64, double>
        {
            public double Convert(Int64 source, double destination, ResolutionContext context)
            {
                return source / 100d; // interpret long as Ticks
            }


        }

        public class LongToDoubleConverter2 : ITypeConverter<long, double>
        {
            public double Convert(long source, double destination, ResolutionContext context)
            {
                return source / 100d; // interpret long as Ticks
            }


        }

        public class DoubleToLongConverter : ITypeConverter<double, long>
        {
            public long Convert(double source, long destination, ResolutionContext context)
            {
                return (long)(source * 100d); // interpret long as Ticks
            }
        }




        public static void CreateTradeExpression(IMapperConfigurationExpression ce)
        {
            ce.CreateMap<Int64, DateTime>().ConvertUsing<TicksToDateTimeConverter>();
            //ce.CreateMap<Int64, double>().ConvertUsing<LongToDoubleConverter>();
            //ce.CreateMap<Int64, double>().ConvertUsing<LongToDoubleConverter2>();
           ce.CreateMap<Entity.SQL.Trade, Model.Trade>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src =>
                src.Price / 100d))
                //.ForMember(dest => dest.Profit, opt => opt.MapFrom(src => 
                //src.Profit / 100d))
        .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => 
        src.Amount / 100d));
            
            
        }


        public static void CreateTradeSQLExpression(IMapperConfigurationExpression ce)
        {
            ce.CreateMap< DateTime,Int64>().ConvertUsing<DateTimeToTicksConverter>();
            ce.CreateMap< Double,Int64>().ConvertUsing<DoubleToLongConverter>();
           ce.CreateMap<Entity.SQL.Trade, Model.Trade>();

  
        }



        //public static IMappingExpression<Model.XML.Event, Match> CreateMatchExpression2(IMapperConfigurationExpression ce)
        //{
        //    return ce.CreateMap<Model.XML.Event, Match>()
        //                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Name));
        //    //.ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.parent_name))
        //    //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.url));
        //}
        //public static IMappingExpression<Model.JSON.Event, Match> CreateMatchExpression3(IMapperConfigurationExpression ce)
        //{
        //    return ce.CreateMap<Model.JSON.Event, Match>()
        //                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.name))

        //                        .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parent_id))
        //                          .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.created))
        //                            .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.start_datetime));


        //}




        //public static IMappingExpression<Model.XML.Market, BettingSystem.Model.Market> CreateMarketExpression(IMapperConfigurationExpression ce)
        //{
        //    return ce.CreateMap<Model.XML.Market, BettingSystem.Model.Market>()
        //                     .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.EventId))
        //                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Name))
        //      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Slug));
        //    //.ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.parent_name))
        //    //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.url));
        //}



        //public static IMappingExpression<Model.XML.Contract, BettingSystem.Model.Contract> CreateContractExpression(IMapperConfigurationExpression ce)
        //{
        //    return ce.CreateMap<Model.XML.Contract, BettingSystem.Model.Contract>()
        //                     .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.MarketId))
        //                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Name + src.Id));

        //    //.ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.parent_name))
        //    //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.url));
        //}



        //public static IMappingExpression<Model.JSON.Competitor, SportSystem.Model.Competitor> CreateCompetitorExpression(IMapperConfigurationExpression ce)
        //{
        //    return ce.CreateMap<Model.JSON.Competitor, SportSystem.Model.Competitor>()

        //                     .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.event_id))
        //                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.name))
        //          .ForMember(dest => dest.Statistics, opt => opt.MapFrom(
        //              src => GroundToBool(src.type) != null ? new Collection<Statistic>
        //              {
        //                  new Statistic
        //                  {
        //                      Key ="Ground",
        //                      Measurements =new Collection<Measurement>
        //                      {
        //                          new Measurement
        //                      {
        //                          Time =default(DateTime),
        //                          Value =Convert.ToDouble(GroundToBool(src.type))
        //                      }
        //                  }   }
        //              } : null));



        //    //.ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.parent_name))
        //    //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.url));
        //}


        //public static Boolean? GroundToBool(string ground)
        //{

        //    switch (ground)
        //    {
        //        case ("home"):
        //            return true;
        //        case ("away"):
        //            return false;
        //        default:
        //            return null;

        //    }
        //}

    }

}
