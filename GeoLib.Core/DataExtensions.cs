﻿using System.Collections.Generic;
using System.Linq;

namespace GeoLib.Core
{
    public static class DataExtensions
    {
        // todo: use ToList()?
        public static IEnumerable<T> ToFullyLoaded<T>(this IQueryable<T> query)
        {
            return query.ToArray().ToList();
        }

        public static IEnumerable<T> ToFullyLoaded<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }
    }
}
