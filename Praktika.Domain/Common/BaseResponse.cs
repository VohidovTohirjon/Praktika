﻿using Newtonsoft.Json;

namespace Praktika.Domain.Common
{
    public class BaseResponse<T>
    {
        [JsonIgnore]
        public int Code { get; set; } = 200;
        public T Data { get; set; }

        public ErrorModel Error { get; set; }
    }
}
