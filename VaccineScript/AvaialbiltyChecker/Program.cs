﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;

using AvaiabiltyChecker.DataObjects;

namespace ConsoleApp1
{
    //BBMP district id = 2294
    //
    class Program
    {
        static HttpClient httpClient = new HttpClient();

        static void HandleCenters (Centers centerInformation )
        {
            if (centerInformation == null)
            {
                Console.WriteLine("Center Information came back as null");
                return;
            }
            centerInformation.sessions
                             .Where(x => x.min_age_limit == 18)
                             .Where(x => x.available_capacity > 0)
                             .ToList().ForEach(x => x.CentreInformation());
        }

        static async Task<Centers> GetCenters(DistrictModel districtModel)
        {
            string getPath = QueryMaker(districtModel);
            Centers centerInformation = null;
            HttpResponseMessage responseMessage = await httpClient.GetAsync(getPath);
            if (responseMessage.IsSuccessStatusCode)
            {
                centerInformation = (await responseMessage.Content.ReadAsAsync<Centers>());
            }
            return centerInformation;
        }

        private static string QueryMaker(DistrictModel districtModel)
        {
            return "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/findByDistrict?district_id=" + districtModel.district_id + "&date=" + districtModel.date; 
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            var district = new DistrictModel
            {
                district_id = "294",
                date = System.DateTime.Now.ToString("dd-MM-yyyy")
            };
            while (true)
            {
                try
                {
                    var centers = await GetCenters(district);

                    HandleCenters(await GetCenters(district));
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
