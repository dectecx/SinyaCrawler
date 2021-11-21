using System.ComponentModel.DataAnnotations;

namespace SinyaCrawler.Service.Model.LineNotify
{
    public class NotifyWithStickerReqVo
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string AccessToken { get; set; }

        //https://developers.line.biz/en/docs/messaging-api/sticker-list/#sticker-definitions
        public string StickerPackageId { get; set; }

        public string StickerId { get; set; }
    }
}
