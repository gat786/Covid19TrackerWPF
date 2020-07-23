using CovidTrackerDesktopApp.Model;
using CovidTrackerDesktopApp.Properties;
using CovidTrackerDesktopApp.Repository;
using RestSharp;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace CovidTrackerDesktopApp.Repositories
{
    public class CovidAPIRepo
    {
        const string Covid19APIBaseUrl = "https://api.covid19api.com";
        private RestClient _restclient = new RestClient(Covid19APIBaseUrl);
       
        public Tuple<bool, object> GetSummary()
        {
            var restRequest = new RestRequest("summary");
            var response = _restclient.Get(restRequest);
            if (response.IsSuccessful) {
                var json = JsonSerializer.Deserialize<SummaryModel>(response.Content);
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
            var listOfCountries = new CountryListRepo().GetCountryNames();
            if (listOfCountries.Contains(countryName))
            {
                // name was correctly sent
                var restRequest = new RestRequest($"country/{countryName}");
                var response = _restclient.Get(restRequest);
                if (response.IsSuccessful)
                {
                    var json = JsonSerializer.Deserialize<CountryCovidDataModel>(response.Content);
                    return new Tuple<bool, object>(true, json);
                }
                else
                {
                    Console.WriteLine("Request wasnt successful try again");
                    Console.WriteLine($"StatusCode returned was {response.StatusCode}");
                    return new Tuple<bool, object>(false, null);
                }
                
            }
            else
            {
                // name of country wasnt correct need to check again
                return new Tuple<bool, object>(false, null);
            }
            
        }
    }
}
