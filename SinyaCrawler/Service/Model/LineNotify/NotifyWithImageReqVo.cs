using System.ComponentModel.DataAnnotations;

namespace SinyaCrawler.Service.Model.LineNotify
{
    public class NotifyWithImageReqVo
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string AccessToken { get; set; }

        public string FilePath { get; set; }

        public byte[] FileBytes { get; set; }
    }
}
