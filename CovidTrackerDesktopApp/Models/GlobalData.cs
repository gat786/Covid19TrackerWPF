using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTrackerDesktopApp.Model
{
    class GlobalData
    {
        public int NewRecoveries { get; set; }
        public int TotalRecoveries { get; set; }
        public int NewInfections { get; set; }
        public int TotalInfections { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
    }
}
