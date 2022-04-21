using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using JibbleGroup.Model;
using Newtonsoft.Json;

namespace JibbleGroup.Service.Helper
{
    /// <summary>
    /// HttpClient Extention for calling APIs
    /// </summary>
    public static class ServiceHttpClientExtension
    {
        /// <summary>
        /// Use for GET action verb
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="httpClientFactory"></param>
        /// <param name="uri">API Address</param>
        /// <returns></returns>
        public static async Task<ApiResultModel<T>> GetDataAsync<T>(IHttpClientFactory httpClientFactory,string uri)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetAsync(uri);
            //if the status code is OK Deserialize JSON
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data =  JsonConvert.DeserializeObject<T>(result);
                return new ApiResultModel<T> {Status = HttpStatusCode.OK,Data = data};
            }

            return new ApiResultModel<T> {Status = response.StatusCode};


        }
    }
}
