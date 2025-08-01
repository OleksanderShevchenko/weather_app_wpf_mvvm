using System.Collections.ObjectModel;
using System.ComponentModel;
using weather_app_wpf_mvvm.Core;
using weather_app_wpf_mvvm.ViewModel.Commands;
using WeatherApp.Model;
using WeatherApp.ViewModel.AccuWeatherHelpers;

namespace weather_app_wpf_mvvm.ViewModel
{
	public class WeatherVM : INotifyPropertyChanged
	{
		private string _cityQuery;
		private AccuWeatherHelper _helper;
		private WeatherCondition _weatherConditions;
		private ObservableCollection<City> _cities;

		public string CityQuery
		{
			get { return _cityQuery; }
			set
			{
				_cityQuery = value;
				OnPropertyChanged(nameof(CityQuery));
			}
		}

		public WeatherCondition WeatherConditions
		{
			get { return _weatherConditions; }
			set
			{
				_weatherConditions = value;
				OnPropertyChanged(nameof(WeatherConditions));
			}
		}

		private async void getCurrentCondition()
		{
			WeatherConditions = await _helper.GetWeatherCondition(SelectedCity.Key);
			// clear after we've got new current weather conditions
			CityQuery = string.Empty;
			_cities.Clear();

		}

		public ObservableCollection<City> FoundCities
		{
			get { return _cities; }
			set { _cities = value; }
		}


		private City _selectedCity;

		public City SelectedCity
		{
			get { return _selectedCity; }
			set
			{
				_selectedCity = value;
				if (_selectedCity != null)
				{
					OnPropertyChanged(nameof(SelectedCity));
					getCurrentCondition();
				}
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
			if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
			{
				// Initialize properties for design mode
				CityQuery = "Search for a city...";
				WeatherConditions = new WeatherCondition()
				{
					WeatherText = "Partially cloudy",
					Temperature = new Temperature()
					{
						Metric = new UnitValue() { Value = "20", Unit = "C", UnitType = 1 },
					},
					Link = "http://www.accuweather.com/en/ua/uzhhorod/326310/current-weather/326310?lang=en-us",
				};
				SelectedCity = new City()
				{
					LocalizedName = "Uzhgorod"
				};
				FoundCities = new ObservableCollection<City>
				{
					SelectedCity,
					new City { LocalizedName = "Mykolaiv" }
				};
			}
			else
			{
				CityQuery = "";
				// Initialize the AccuWeatherHelper with the app configuration
				var _appConfig = AppConfig.Instance;
				FoundCities = new ObservableCollection<City>();
				_helper = new AccuWeatherHelper(_appConfig.AppKey, _appConfig.ApiBaseUrl, _appConfig.LocationUrl, _appConfig.ConditionUrl);
				SearchForCity = new SearchComand(this);
			}

		}

		public async void FetchCitiesAsync()
		{
			// This method can be used to make a query to the weather service.
			// It can be implemented to fetch list of cities using user input in the text box.
			var cities = await _helper.GetCities(CityQuery);
			FoundCities.Clear();
			foreach (var city in cities)
			{
				_cities.Add(city);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}




	}
}
