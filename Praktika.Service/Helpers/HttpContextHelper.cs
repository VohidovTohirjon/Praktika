using Microsoft.AspNetCore.Http;

namespace Praktika.Service.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor;

        public static HttpContext Context => Accessor?.HttpContext;

        public static IHeaderDictionary ResponseHeader => Context?.Response?.Headers;
    }
}
