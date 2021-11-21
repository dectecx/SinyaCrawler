using SinyaCrawler.Service.Interface;
using SinyaCrawler.Utility;

namespace SinyaCrawler.Service
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<KeyValuePair<string, string>> GetFormData<T>(T query, bool useDescription = false)
        {
            var formData = new List<KeyValuePair<string, string>>();
            if (query != null)
            {
                query.GetType().GetProperties().ToList().ForEach(p =>
                {
                    if (useDescription)
                    {
                        var keyPair = new KeyValuePair<string, string>(Util.GetDescription(query.GetType(), p.Name), p.GetValue(query)?.ToString());
                        formData.Add(keyPair);
                    }
                    else
                    {
                        var keyPair = new KeyValuePair<string, string>(p.Name, p.GetValue(query)?.ToString());
                        formData.Add(keyPair);
                    }
                });
            }
            return formData;
        }

        public async Task<string> DoGetAsync(string url)
        {
            try
            {
                var result = _httpClient.GetAsync(url);
                string resultContent = result.Result.Content.ReadAsStringAsync().Result;
                return resultContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> DoPostAsync(string url, IEnumerable<KeyValuePair<string, string>>? formData = null)
        {
            try
            {
                var content = formData != null ? new FormUrlEncodedContent(formData.ToArray()) : null;
                var result = _httpClient.PostAsync(url, content);
                string resultContent = result.Result.Content.ReadAsStringAsync().Result;
                return resultContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
