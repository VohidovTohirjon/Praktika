using Newtonsoft.Json;
using Praktika.Domain.Configurations;
using Praktika.Service.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Praktika.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, PaginationParams @params)
        {
            var metaData = new PaginationMetaData(source.Count(), @params);
            var json = JsonConvert.SerializeObject(metaData);

            if (HttpContextHelper.ResponseHeader.Keys.Contains("X-Pagination"))
                HttpContextHelper.ResponseHeader.Remove("X-Pagination");

            HttpContextHelper.Context.Response.Headers.Add("X-Pagination", json);

            return @params.PageSize > 0 && @params.PageIndex >= 0
                ? source.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize) : source;
        }
    }
}
