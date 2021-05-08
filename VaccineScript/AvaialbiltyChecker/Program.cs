using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using AvaiabiltyChecker.DataObjects;

namespace ConsoleApp1
{
    //BBMP district id = 2294
    //
    class Program
    {
        static HttpClient httpClient = new HttpClient();

        static void HandleCenters (IEnumerable<CenterInformation> centerInformation )
        {
            centerInformation.Where(x => x.min_age_limit == 18)
                             .Where(x => x.available_capacity > 0)
                             .ToList().ForEach(x => x.CentreInformation());
        }

        static async Task<IEnumerable<CenterInformation>> GetCenters(findByDistrictModel districtModel)
        {
            string getPath = QueryMaker(districtModel);
            List<CenterInformation> centerInformation = null;
            HttpResponseMessage responseMessage = await httpClient.GetAsync(getPath);
            if (responseMessage.IsSuccessStatusCode)
            {
                centerInformation = (await responseMessage.Content.ReadAsAsync<IEnumerable<CenterInformation>>()).ToList();
            }
            return centerInformation;
        }

        private static string QueryMaker(findByDistrictModel districtModel)
        {
            return "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/findByDistrict?district_id=" + districtModel.district_id + "&date=" + districtModel.date; 
        }

        static void Main(string[] args)
        {
            
        }
    }
}
