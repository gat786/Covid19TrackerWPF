using CovidTrackerDesktopApp.Model;
using CovidTrackerDesktopApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CovidTrackerDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        CovidAPIRepo repo = new CovidAPIRepo();
        string GlobalNewRecoveries, GlobalNewDeaths, GlobalNewInfections, GlobalTotalRecoveries, GlobalTotalDeaths, GlobalTotalInfections = 0.ToString();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var summary = repo.GetSummary();
            if (summary.Item1)
            {
                GlobalTotalDeaths = summary.Item2.Global.TotalDeaths.ToString();
                GlobalTotalRecoveries = summary.Item2.Global.TotalRecovered.ToString();
                GlobalTotalInfections = summary.Item2.Global.TotalConfirmed.ToString();

                GlobalNewDeaths = summary.Item2.Global.NewDeaths.ToString();
                GlobalNewRecoveries = summary.Item2.Global.NewRecovered.ToString();
                GlobalNewInfections = summary.Item2.Global.NewConfirmed.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
