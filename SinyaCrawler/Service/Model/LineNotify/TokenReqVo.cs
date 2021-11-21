using System.ComponentModel.DataAnnotations;

namespace SinyaCrawler.Service.Model.LineNotify
{
    public class TokenReqVo
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CallbackUrl { get; set; }
    }
}
