using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

using AvaiabiltyChecker.DataObjects;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1
{
    //BBMP district id = 294
    //BLR Urban = 265
    class Program
    {
#if Windows
        static System.Media.SoundPlayer player = new System.Media.SoundPlayer(Path.GetFullPath(@".\Resources\button-2.wav"));
#endif
        static HttpClient httpClient = new HttpClient();

        static void HandleCenters (Centers centerInformation )
        {
            int flag = 0;
            if (centerInformation == null)
            {
                Console.WriteLine("Center Information came back as null");
                return;
            }
            centerInformation.centers.ForEach(x => {
                if (x.AvailableSessions())
                    flag++;
                });

#if Windows
            if (flag > 0)
            {
                Task.Run(() => player.Play());
                Console.WriteLine("\n\n\n\n\n");
            }
#endif
            //Console.WriteLine("Refreshed Serach"); UnComment if you get anxious if it is actually working
        }

        static async Task<Centers> GetCenters(DistrictModel districtModel)
        {
            string getPath = QueryMaker(districtModel);
            Centers centerInformation = null;
            HttpResponseMessage responseMessage = await httpClient.GetAsync(getPath);
            if (responseMessage.IsSuccessStatusCode)
            {
                centerInformation = await responseMessage.Content.ReadAsAsync<Centers>();
                //centerInformation = JsonConvert.DeserializeObject<Centers>(centerInformationAsString);
            }
            responseMessage.Content = null;
            return centerInformation;
        }

        private static string QueryMaker(DistrictModel districtModel)
        {
            return "https://cdn-api.co-vin.in/api/v2/appointment/sessions/calendarByDistrict?district_id=" + districtModel.district_id + "&date=" + districtModel.date; 
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            var districts = new List<DistrictModel>
            {
                 new DistrictModel
                 {
                      district_id = "265",
                      date = System.DateTime.Now.ToString("dd-MM-yyyy")
                 },
                 new DistrictModel
                 {
                     district_id = "294",
                     date = System.DateTime.Now.ToString("dd-MM-yyyy")
                 }
            };
            int i = 0;
            int length = districts.Count;
            while (true)
            {
                try
                {
                    HandleCenters(await GetCenters(districts[i%length]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occured: " + ex);
                }
                Thread.Sleep(4000);
            }
            
        }
           
    }
}
