using SinyaCrawler.Extension;
using SinyaCrawler.Service.Interface;
using SinyaCrawler.Service.Model;

namespace SinyaCrawler.Service
{
    public class SinyaService
    {
        private readonly string _listUrl = "https://www.sinya.com.tw/diy/show_option";
        private readonly string _detailUrl = "https://www.sinya.com.tw/diy/api_prods";
        private readonly IHttpService _httpService;

        public SinyaService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ShowOptionRespVo> ShowOptionAsync(ShowOptionReqVo vo)
        {
            var formData = _httpService.GetFormData(vo);
            var resp = await _httpService.DoPostAsync(_listUrl, formData);
            return resp.ToObject<ShowOptionRespVo>();
        }

        public async Task<ApiProdsRespVo[]> ApiProdsAsync(ApiProdsReqVo vo)
        {
            var formData = _httpService.GetFormData(vo, true);
            var resp = await _httpService.DoPostAsync(_detailUrl, formData);
            return resp.ToObject<ApiProdsRespVo[]>();
        }
    }
}
