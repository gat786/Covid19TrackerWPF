using CovidTrackerDesktopApp.Model;
using CovidTrackerDesktopApp.Properties;
using CovidTrackerDesktopApp.Repository;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace CovidTrackerDesktopApp.Repositories
{
    public class CovidAPIRepo
    {
        const string Covid19APIBaseUrl = "https://api.covid19api.com";
        private RestClient _restclient = new RestClient(Covid19APIBaseUrl);
        private JsonDeserializer _deserializer = new JsonDeserializer();

        public Tuple<bool, SummaryModel> GetSummary()
        {
            var restRequest = new RestRequest("summary");
            var response = _restclient.Get(restRequest);
            if (response.IsSuccessful) {
                var responseJsonDeserialized = _deserializer.Deserialize<SummaryModel>(response);
                return new Tuple<bool, SummaryModel>(true, responseJsonDeserialized);
            }
            else
            {
                Console.WriteLine($"Status code for the request resulted as " +
                    $"{response.StatusCode}");
                return new Tuple<bool, SummaryModel>(false, null);
            }
            
        }

        public IEnumerable<Country> GetWorstHitCountries()
        {
            var summary = GetSummary();
            if (summary.Item1)
            {
                var sorted = (summary.Item2 as SummaryModel).Countries.OrderByDescending(x => x.TotalConfirmed);
                var top = sorted.Take(6);
                return top;
            }
            return null;
        }

        public Tuple<bool,object> GetCountryWiseData(string countryName)
        {
            var listOfCountries = new CountryListRepo().GetCountryNames();
            Console.WriteLine(listOfCountries.Contains("india"));
            if (listOfCountries.Contains(countryName))
            {
                Console.WriteLine("Going on with the request");
                // name was correctly sent
                var restRequest = new RestRequest($"country/{countryName}");
                var responseParsed = _restclient.Execute<List<CountryPerDaySummary>>(restRequest);
                
                if (responseParsed.IsSuccessful)
                {
                    //successfully done with the request
                    Console.WriteLine("request went through successfully");
                    try
                    {
                        return new Tuple<bool, object>(true, responseParsed.Data);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"{e.Message} happened");
                    }
                    return new Tuple<bool, object>(false, null);
                }
                else
                {   
                    //successfully done with the request
                    Console.WriteLine("Request wasnt successful try again");
                    Console.WriteLine($"StatusCode returned was {responseParsed.StatusCode} {responseParsed.Content}");
                    return new Tuple<bool, object>(false, null);
                }
                
            }
            else
            {
                // name of country wasnt correct need to check again
                Console.WriteLine("Name not in the country list");
                return new Tuple<bool, object>(false, null);
            }
            
        }
    }
}
