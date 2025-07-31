using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather_app_wpf_mvvm.Core;
using weather_app_wpf_mvvm.ViewModel.Commands;
using WeatherApp.Model;
using WeatherApp.ViewModel.AccuWeatherHelpers;

namespace weather_app_wpf_mvvm.ViewModel
{
    public class WeatherVM: INotifyPropertyChanged
    {
		private string _cityQuery;
		private AppConfig _appConfig = AppConfig.Instance;
		private AccuWeatherHelper _helper;

		public string CityQuery
		{
			get { return _cityQuery; }
			set
			{
				if (_cityQuery != value)
				{
					_cityQuery = value;
					OnPropertyChanged(nameof(CityQuery));
				}
			}
		}

		private WeatherCondition _weatherConditions;

		public WeatherCondition WeatherConditions
		{
			get { return _weatherConditions; }
			set 
			{ 
				_weatherConditions = value;
				OnPropertyChanged(nameof(WeatherConditions));
			}
		}

		private City _selectedCity;

		public City SelectedCity
		{
			get { return _selectedCity; }
			set 
			{ 
				_selectedCity = value;
				OnPropertyChanged(nameof(SelectedCity));
			}
		}

		private SearchComand _searchCity;

		public SearchComand SearchForCity
		{
			get { return _searchCity; }
			set { _searchCity = value; }
		}


		public WeatherVM()
		{
			if(DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
			{
				// Initialize properties if necessary
				WeatherConditions = new WeatherCondition()
				{
					WeatherText = "Partially cloudy",
					Temperature = new Temperature()
					{
						Metric = new UnitValue() { Value = 20, Unit = "C", UnitType = 1 },
						Imperial = new UnitValue() { Value = 68, Unit = "F", UnitType = 2 }
					},
					Link = "http://www.accuweather.com/en/ua/uzhhorod/326310/current-weather/326310?lang=en-us",
				};
				SelectedCity = new City()
				{
					LocalizedName = "Uzhgorod"
				};
			}
			// Initialize the AccuWeatherHelper with the app configuration
			_helper = new AccuWeatherHelper(_appConfig.AppKey, _appConfig.ApiBaseUrl, _appConfig.LocationUrl, _appConfig.ConditionUrl);
			_searchCity = new SearchComand(this);
		}

		public async Task FetchCitiesAsync()
		{
			// This method can be used to make a query to the weather service.
			// It can be implemented to fetch list of cities using user input in the text box.
			var cities = await _helper.GetCities(CityQuery);
			foreach (var city in cities) 
			{
				;
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Additional properties and methods for weather data can be added here.
	}
}
