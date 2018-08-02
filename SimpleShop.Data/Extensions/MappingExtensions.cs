using System;
using System.Linq.Expressions;
using AutoMapper;

namespace SimpleShop.Data.Extensions
{
	public static class MappingExtentions
	{
		public static TDest MapTo<TDest>(this object src)
		{
			return (TDest)Mapper.Map(src, src.GetType(), typeof(TDest));
		}

		public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
			this IMappingExpression<TSource, TDestination> map,
			Expression<Func<TDestination, object>> selector)
		{
			map.ForMember(selector, config => config.Ignore());
			return map;
		}
	}
}