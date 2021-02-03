using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAWebHost.Impl
{
    public interface IGeoService
    {
        Task<string> GetCountry(string ipAddress);
    }
    public class GeoService : IGeoService
    {
        public async Task<string> GetCountry(string ipAddress)
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync($"http://api.ipstack.com/${ipAddress}?access_key=<your api key>");
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                return data.country_code;
            }
        }
    }
}
