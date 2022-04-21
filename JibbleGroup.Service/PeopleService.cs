using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JibbleGroup.Model;
using JibbleGroup.Service.Helper;

namespace JibbleGroup.Service
{
    /// <summary>
    /// People Service using APIs from
    /// https://services.odata.org/TripPinRESTierService/People
    /// </summary>
    public class PeopleService : IPeopleService
    {
        #region Fields
        private const string Uri = "https://services.odata.org/TripPinRESTierService/People";
        private readonly IHttpClientFactory _httpClient;
        #endregion

        #region Ctor
        public PeopleService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion


        /// <summary>
        /// Fetch People without filter or filter
        /// </summary>
        /// <param name="filter">$filter=FirstName eq 'Scott'</param>
        /// <returns>People List</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<People>> GetAsync(string filter)
        {
            var result =await ServiceHttpClientExtension.GetDataAsync<PeopleList>(_httpClient, string.IsNullOrEmpty(filter) ? Uri : $"{Uri}?$filter={filter}");
            switch (result.Status)
            {
                case HttpStatusCode.OK:
                    return result.Data.Value;
                case HttpStatusCode.NotFound:
                    return new List<People>();
                default:
                    throw new ArgumentException("Oops Something went wrong");
            }
        }

        /// <summary>
        /// Get Specific person by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A Person</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<People> GetOneAsync(string username)
        {
            var result = await ServiceHttpClientExtension.GetDataAsync<People>(_httpClient, $"{Uri}('{username.Trim()}')");
            switch (result.Status)
            {
                case HttpStatusCode.OK:
                    return result.Data;
                case HttpStatusCode.NotFound:
                    return null;
                default:
                    throw new ArgumentException("Oops Something went wrong");
            }
            
        }
    }
}
