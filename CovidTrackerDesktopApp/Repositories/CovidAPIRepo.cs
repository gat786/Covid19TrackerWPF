using CovidTrackerDesktopApp.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTrackerDesktopApp.Repositories
{
    public class CovidAPIRepo
    {
        const string Covid19APIBaseUrl = "https://api.covid19api.com";
        private RestClient _restclient;
        public CovidAPIRepo() {
            _restclient = new RestClient(Covid19APIBaseUrl);
        }

        public Tuple<bool, object> GetSummary()
        {
            var restRequest = new RestRequest("summary");
            var response = _restclient.Get(restRequest);
            if (response.IsSuccessful) {
                var json = JsonConvert.DeserializeObject(response.Content);
                Console.WriteLine(json);
                return new Tuple<bool, object>(true, json);
            }
            else
            {
                Console.WriteLine($"Status code for the request resulted as " +
                    $"{response.StatusCode}");
                return new Tuple<bool, object>(false, null);
            }
            
        }

        public Tuple<bool,object> GetCountryWiseData(string countryName)
        {
            var restRequest = new RestRequest("");
            var model = new CountriesModel();
            model.GetCountriesNamesList();
            return new Tuple<bool, object>(true, new object());
        }
    }
}
