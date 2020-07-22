using CovidTrackerDesktopApp.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTrackerDesktopApp.Model
{
    class CountriesModel
    {
        public void GetCountriesNamesList()
        {
            
            var jsonObject = JObject.Parse(Resources.countries_json);
            Console.WriteLine(jsonObject);
        }
    }
}
