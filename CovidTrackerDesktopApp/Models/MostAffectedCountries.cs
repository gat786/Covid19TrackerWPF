using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTrackerDesktopApp.Model
{
    class CountryData {
        public int CountryName { get; set; }
        public int ActiveInfectionsCount { get; set; }
    }

    class MostAffectedCountries
    {
        public List<CountryData> countryData { get; set; }
    }
}

