using SinyaCrawler.Service.Model;

namespace SinyaCrawler.Service
{
    public class CrawlerService
    {
        private readonly SinyaService _sinyaService;

        public CrawlerService(SinyaService sinyaService)
        {
            _sinyaService = sinyaService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Start");
            // 查詢指定顯示卡類別的group_id,為提升效率,此段註解,平時以寫死group_id為主
            var allVo = new ShowOptionReqVo { id = 5 };
            var allVGAs = await _sinyaService.ShowOptionAsync(allVo);
            var rtx3060tiGroupId = allVGAs.showOption.Where(x => x.title.ToLower().Contains("3060ti "))
                                                     .Single()
                                                     .group_id;

            var rtx3060tiVo = new ApiProdsReqVo {
                group_id = rtx3060tiGroupId,
                prod_sub_slave_id = "5"
            };
            var rtx3060ti = await _sinyaService.ApiProdsAsync(rtx3060tiVo);
            var g = rtx3060ti.Where(x => x.barcode == null).ToList();
            var hasStockList = rtx3060ti.Where(x => string.IsNullOrEmpty(x.stockText))
                                        .ToList();
            var n = string.Join(",", g.Select(x => x.prod_name).ToArray());

            hasStockList.ForEach(x =>
            {
                Console.WriteLine($"{x.price}\t{x.prod_name}");
            });
            Console.WriteLine("End");
            return;
        }
    }
}
