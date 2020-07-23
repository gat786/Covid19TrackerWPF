using System.Collections.Generic;

namespace CovidTrackerDesktopApp.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class CountryDetail
    {
        public string CountryName { get; set; }
        public string Slug { get; set; }
        public string ISO2 { get; set; }

    }

    public class CountryModel
    {
        public List<CountryDetail> list { get; set; }

    }
}
