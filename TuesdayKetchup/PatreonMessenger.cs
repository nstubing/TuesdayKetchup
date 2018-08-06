using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace TuesdayKetchup
{
    public class PatreonMessenger
    {
        private string url;
        private string accessToken;
        
        public PatreonMessenger()
        {
            url = "https://www.patreon.com/api/oauth2/api/campaigns/1719233/pledges?include=patron.null";
            accessToken = "Bearer " + MyKeys.PatreonAccessToken;
        }

        private async Task<JObject> GetPledgeJSONAsync()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", accessToken);
            HttpResponseMessage response =await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            return json;
            
        }

        public List<string> GetPatrons()
        {
            JObject json = GetPledgeJSONAsync().Result;

            List<string> patronList = new List<string>();
            int patronCount = json["included"].Count();
            for (int i = 0; i < patronCount; i++)
            {
                patronList.Add((string)json["included"][i]["attributes"]["full_name"]);
            }
            return patronList;
        }
    }
}