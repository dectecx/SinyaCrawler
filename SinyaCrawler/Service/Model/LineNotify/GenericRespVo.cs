using Newtonsoft.Json;

namespace SinyaCrawler.Service.Model.LineNotify
{
    public class GenericRespVo
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
