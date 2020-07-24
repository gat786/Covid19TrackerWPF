using CovidTrackerDesktopApp.Repositories;
using CovidTrackerDesktopApp.Repository;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CovidTrackerDesktopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        CovidAPIRepo repo = new CovidAPIRepo();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CountryNamesPopUp.IsOpen = (CountryNamesPopUp.IsOpen) ? false : true;
        }

        List<TextBlock> WorstHitCountryName;
        List<TextBlock> WorstHitCountryCount;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WorstHitCountryName = new List<TextBlock>()
            { 
                most_affected_1_name, 
                most_affected_2_name, 
                most_affected_3_name, 
                most_affected_4_name, 
                most_affected_5_name, 
                most_affected_6_name 
            };

            WorstHitCountryCount = new List<TextBlock>()
            {
                most_affected_1_count,
                most_affected_2_count,
                most_affected_3_count,
                most_affected_4_count,
                most_affected_5_count,
                most_affected_6_count
            };

            var countryListRepo = new CountryListRepo();
            var listCountries = countryListRepo.GetCountryNames();

            foreach (string name in listCountries)
            {
                var tb = new TextBlock();
                tb.Text = name;
                NamesOfCountriesSP.Children.Add(tb);
            }

            var summary = repo.GetSummary();
            if (summary.Item1)
            {
                progress_bar.Value = 50;
                progress_text.Content = "Loaded";
                var globalData = summary.Item2.Global;
                global_new_deaths.Text = globalData.NewDeaths.ToString();
                global_total_deaths.Text = globalData.TotalDeaths.ToString();
                global_new_infections.Text = globalData.NewConfirmed.ToString();
                global_total_infections.Text = globalData.TotalConfirmed.ToString();
                global_new_recoveries.Text = globalData.NewRecovered.ToString();
                global_total_recoveries.Text = globalData.TotalRecovered.ToString();

            }
            else
            {
                progress_bar.Value = 50;
                progress_text.Content = "Error";
            }

            var worstAffectedData = repo.GetWorstHitCountries();
            if (worstAffectedData != null) {
                progress_bar.Value = 100;
                progress_text.Content = "Loaded";
                for (int i = 0; i < WorstHitCountryName.Count; i++) {
                    WorstHitCountryName[i].Text = worstAffectedData[i].Slug.ToUpperInvariant();
                    WorstHitCountryCount[i].Text = worstAffectedData[i].TotalConfirmed.ToString();
                }
                progress_bar.Visibility = Visibility.Collapsed;
            }
            else
            {
                progress_bar.Value = 100;
                progress_text.Content = "Error";
                progress_bar.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var queryString = SearchField.Text;
            if (queryString.Length > 2) {
                var countryRepo = new CountryListRepo();
                var countryNames = countryRepo.GetCountryNames();
                if (countryNames.Contains(queryString)) {
                    ErrorLogo.Visibility = Visibility.Collapsed;
                    Console.WriteLine("Country Name Found");
                    GoBackButton.Visibility = Visibility.Visible;
                    SearchResultsGrid.Visibility = Visibility.Visible;
                    var dataForCountry = repo.GetCountryWiseData(queryString);
                    if (dataForCountry.Item1)
                    {
                        var CountryData = dataForCountry.Item2;
                        CountryData.Reverse();


                    }
                    Console.WriteLine(dataForCountry.Item2[0].Date);
                }
                else
                {

                    ErrorLogo.Visibility = Visibility.Visible;
                    Console.WriteLine("Not able to find Correct Country Name");
                }
            }
            else
            {
                Console.WriteLine("Less than 2 charachters written");
                ErrorLogo.Visibility = Visibility.Visible;
            }
        }
    }
}
