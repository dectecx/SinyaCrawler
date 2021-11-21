using System.ComponentModel.DataAnnotations;

namespace SinyaCrawler.Service.Model.LineNotify
{
    public class NotifyWithMessageReqVo
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string AccessToken { get; set; }
    }
}
