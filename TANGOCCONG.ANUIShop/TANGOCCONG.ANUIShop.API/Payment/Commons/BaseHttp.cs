using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Payment.Commons
{
    public class BaseHttp
    {
        private static int _TIMEOUT = 15;
        public async Task<string> Get(string url, string tokenAuth = "")
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(_TIMEOUT);
                    if (!string.IsNullOrEmpty(tokenAuth))
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenAuth);
                    using (HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false))
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> Post(string url, object param, string tokenAuth = "")
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(_TIMEOUT);
                    if (!string.IsNullOrEmpty(tokenAuth))
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenAuth);
                    using (HttpResponseMessage response = await httpClient.PostAsync(url, httpContent).ConfigureAwait(false))
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
