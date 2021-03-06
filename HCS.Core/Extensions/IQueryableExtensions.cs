﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HCS.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnsMap.ContainsKey(queryObject.SortBy))
                return query;
            if (queryObject.IsSortAscending)
                return query.OrderBy(columnsMap[queryObject.SortBy]);
            return query.OrderByDescending(columnsMap[queryObject.SortBy]);
        }
    }
}
