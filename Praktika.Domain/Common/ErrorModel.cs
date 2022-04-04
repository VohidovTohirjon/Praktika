using Newtonsoft.Json;

namespace Praktika.Domain.Common
{
    public class ErrorModel
    {
        [JsonIgnore]
        public int Code { get; set; }

        public string Message { get; set; }

        public ErrorModel(int Code, string Message)
        {
            this.Code = Code;
            this.Message = Message;
        }

    }
}
