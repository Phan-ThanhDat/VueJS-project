using System;
using AutoMapper;
using System.Linq.Expressions;
using Homework.DTOs;

namespace Homework.App_Start
{
    public class AutoMapperConfig
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                #region DTOs to db

                // Nothing

                #endregion
            });


        }

        public static Expression<Func<decimal?, decimal?>> RoundDecimals = dec => dec != null ? Decimal.Round(dec.Value, 2) : dec;

        public static void MapSpecifiedFields<TSource, TDest>(IMappingExpression<TSource, TDest> mappingExpression)
        {
            throw new NotImplementedException("Not yet");
        }

    }
}