using Newtonsoft.Json;

namespace SinyaCrawler.Service.Model.LineNotify
{
    public class TokenInfoRespVo : GenericRespVo
    {
        [JsonProperty("targetType")]
        public string TargetType { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        public int Limit { get; set; }

        public int ImageLimit { get; set; }

        public int Remaining { get; set; }

        public int ImageRemaining { get; set; }

        public int Reset { get; set; }

        public DateTime ResetLocalTime { get; set; }
    }
}
