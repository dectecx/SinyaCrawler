namespace SinyaCrawler.Service.Interface
{
    public interface IHttpService
    {
        public IEnumerable<KeyValuePair<string, string>> GetFormData<T>(T query, bool useDescription = false);

        public Task<string> DoGetAsync(string url);

        public Task<string> DoPostAsync(string url, IEnumerable<KeyValuePair<string, string>>? formData = null);
    }
}
