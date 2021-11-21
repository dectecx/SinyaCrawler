using Newtonsoft.Json;

namespace SinyaCrawler.Service.Model.LineNotify
{
    public class TokenRespVo : GenericRespVo
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
