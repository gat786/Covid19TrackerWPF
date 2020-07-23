using CovidTrackerDesktopApp.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CovidTrackerDesktopApp.Properties;

namespace CovidTrackerDesktopApp.Repository
{
    class CountryListRepo
    {
        List<CountryDetail> GetListOfCountries()
        {
            var countryModel = JsonSerializer.Deserialize<CountryModel>(Resources.countries_json);
            return countryModel.list;
        }

        public List<string> GetCountryNames()
        {
            var countryList = GetListOfCountries();
            List<string> countryNames = new List<string>();
            foreach(CountryDetail country in countryList)
                countryNames.Add(country.CountryName);
            return countryNames;
        }
    }
}
