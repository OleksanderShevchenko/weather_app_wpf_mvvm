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


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Additional properties and methods for weather data can be added here.
	}
}
