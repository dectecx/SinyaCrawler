using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SinyaCrawler.Service.Model;
using SinyaCrawler.Service.Model.LineNotify;

namespace SinyaCrawler.Service
{
    public class CrawlerService
    {
        private readonly LineNotifyOption _lineNotifyOption;
        private readonly SinyaService _sinyaService;
        private readonly LineNotifyService _lineNotifyService;

        public CrawlerService(IOptions<LineNotifyOption> lineNotifyOption, SinyaService sinyaService, LineNotifyService lineNotifyService)
        {
            _lineNotifyOption = lineNotifyOption.Value;
            _sinyaService = sinyaService;
            _lineNotifyService = lineNotifyService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Start");
            // 查詢指定顯示卡類別的group_id,為提升效率,此段註解,平時以寫死group_id為主
            /*
            var allVo = new ShowOptionReqVo { id = 5 };
            var allVGAs = await _sinyaService.ShowOptionAsync(allVo);
            var rtx3060tiGroupId = allVGAs.showOption.Where(x => x.title.ToLower().Contains("3060ti "))
                                                     .Single()
                                                     .group_id;
            */
            var rtx3060tiGroupId = "2272";
            var rtx3060tiVo = new ApiProdsReqVo {
                group_id = rtx3060tiGroupId,
                prod_sub_slave_id = "5"
            };
            var rtx3060ti = await _sinyaService.ApiProdsAsync(rtx3060tiVo);
            var hasStockList = rtx3060ti.Where(x => string.IsNullOrEmpty(x.stockText))
                                        .ToList();

            var index = 0;
            var message = "\n";
            foreach (var item in hasStockList)
            {
                message += $"【{item.price}】\n{string.Join('\n', item.prod_name.Split('+'))}\n-----\n";
                index++;

                if (index == 5)
                {
                    Console.WriteLine(message);
                    await _lineNotifyService.NotifyAsync(new NotifyWithMessageReqVo
                    {
                        AccessToken = _lineNotifyOption.Token,
                        Message = message
                    }, CancellationToken.None);
                    index = 0;
                    message = "\n";
                }
            }
            if (!string.IsNullOrEmpty(message))
            {
                await _lineNotifyService.NotifyAsync(new NotifyWithMessageReqVo
                {
                    AccessToken = _lineNotifyOption.Token,
                    Message = message
                }, CancellationToken.None);
            }
            Console.WriteLine("End");
            return;
        }
    }
}
