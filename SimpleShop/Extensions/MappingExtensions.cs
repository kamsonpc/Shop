using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SimpleShop.Extensions
{
	public static class MappingExtentions
	{
		public static TDest MapTo<TDest>(this object src)
		{
			return (TDest)Mapper.Map(src, src.GetType(), typeof(TDest));
		}

		public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
		{
			expression.ForAllMembers(opt => opt.Ignore());
			return expression;
		}
	}
}