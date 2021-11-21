using SinyaCrawler.Extension;
using SinyaCrawler.Service.Model.LineNotify;
using SinyaCrawler.Utility;
using System.Net;
using System.Net.Http.Headers;

namespace SinyaCrawler.Service
{
    public class LineNotifyService
    {
        private static readonly string ApiEndpoint = "https://notify-api.line.me/";
        private readonly HttpClient _httpClient;

        public LineNotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(ApiEndpoint);
        }

        public bool IsThrowInternalError { get; set; } = false;
        public async Task<GenericRespVo> NotifyAsync(NotifyWithMessageReqVo request, CancellationToken cancelToken)
        {
            Util.Validate(request);

            var url = "api/notify";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken) },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "message", request.Message }
                }),
            };

            var response = _httpClient.SendAsync(httpRequest, cancelToken).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = await response.Content.ReadAsStringAsync(cancelToken);
                if (this.IsThrowInternalError)
                {
                    throw new Exception(error);
                }

                return new GenericRespVo
                {
                    Message = error,
                };
            }
            return response.Content.ReadAsStringAsync(cancelToken).Result.ToObject<GenericRespVo>();
        }

        public async Task<GenericRespVo> NotifyAsync(NotifyWithImageReqVo request, CancellationToken cancelToken)
        {
            Util.Validate(request);
            var url = $"api/notify?message={request.Message}";
            using var formDataContent = new MultipartFormDataContent();

            var imageName = Path.GetFileName(request.FilePath);
            var mimeType = MimeTypeMapping.GetMimeType(imageName);
            var imageContent = new ByteArrayContent(request.FileBytes);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            formDataContent.Add(imageContent, "imageFile", imageName);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken) },
                Content = formDataContent
            };

            var response = await _httpClient.SendAsync(httpRequest, cancelToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (this.IsThrowInternalError)
                {
                    var error = await response.Content.ReadAsStringAsync(cancelToken);
                    throw new Exception(error);
                }
            }
            return response.Content.ReadAsStringAsync(cancelToken).Result.ToObject<GenericRespVo>();
        }

        public async Task<GenericRespVo> NotifyAsync(NotifyWithStickerReqVo request, CancellationToken cancelToken)
        {
            Util.Validate(request);

            var url = "api/notify";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken) },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "message", request.Message },
                    { "stickerPackageId", request.StickerPackageId },
                    { "stickerId", request.StickerId },
                }),
            };

            var response = _httpClient.SendAsync(httpRequest, cancelToken).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = await response.Content.ReadAsStringAsync(cancelToken);
                if (this.IsThrowInternalError)
                {
                    throw new Exception(error);
                }

                return new GenericRespVo
                {
                    Message = error,
                };
            }
            return response.Content.ReadAsStringAsync(cancelToken).Result.ToObject<GenericRespVo>();
        }

        public async Task<TokenInfoRespVo> GetAccessTokenInfoAsync(string accessToken, CancellationToken cancelToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var url = "api/status";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", accessToken) },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>()),
            };

            var response = await _httpClient.SendAsync(httpRequest, cancelToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (this.IsThrowInternalError)
                {
                    var error = await response.Content.ReadAsStringAsync(cancelToken);
                    throw new Exception(error);
                }
            }

            var tokenInfo = response.Content.ReadAsStringAsync(cancelToken).Result.ToObject<TokenInfoRespVo>();
            tokenInfo.Limit = GetValue<int>(response, "X-RateLimit-Limit");
            tokenInfo.ImageLimit = GetValue<int>(response, "X-RateLimit-ImageLimit");
            tokenInfo.Remaining = GetValue<int>(response, "X-RateLimit-Remaining");
            tokenInfo.ImageRemaining = GetValue<int>(response, "X-RateLimit-ImageRemaining");
            tokenInfo.Reset = GetValue<int>(response, "X-RateLimit-Reset");
            tokenInfo.ResetLocalTime = ToLocalTime(tokenInfo.Reset);
            return tokenInfo;
        }

        public async Task<GenericRespVo> RevokeAsync(string accessToken, CancellationToken cancelToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var url = "api/revoke";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", accessToken) },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>()),
            };

            var response = await _httpClient.SendAsync(httpRequest, cancelToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (this.IsThrowInternalError)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            return response.Content.ReadAsStringAsync(cancelToken).Result.ToObject<GenericRespVo>();
        }

        private static T GetValue<T>(HttpResponseMessage response, string key)
        {
            var result = default(T);
            response.Headers.TryGetValues(key, out var values);
            if (values == null)
            {
                return result;
            }

            var content = values.FirstOrDefault();
            return (T)Convert.ChangeType(content, typeof(T));
        }

        private static DateTime ToLocalTime(long source)
        {
            var timeOffset = DateTimeOffset.FromUnixTimeSeconds(source);
            return timeOffset.DateTime.ToUniversalTime();
        }
    }
}
