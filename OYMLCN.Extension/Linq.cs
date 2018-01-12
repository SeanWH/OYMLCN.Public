using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN
{
    /// <summary>
    /// LinqExtensions
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// 获取分页页数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static IQueryable<TSource> TakePage<TSource>(this IQueryable<TSource> source, int page, int limit = 10)
            => source.Skip((page - 1) * limit).Take(limit);
    }
}
