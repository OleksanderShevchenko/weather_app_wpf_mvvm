using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace weather_app_wpf_mvvm.ViewModel
{
    public class WeatherVM: INotifyPropertyChanged
    {
		private string _city;

		public string City
		{
			get { return _city; }
			set
			{
				if (_city != value)
				{
					_city = value;
					OnPropertyChanged(nameof(City));
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
					Link = "https://www.example.com/weather/12345",
				};
				SelectedCity = new City()
				{
					LocalizedName = "Uzhgorod"
				};
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
