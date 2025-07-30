using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Additional properties and methods for weather data can be added here.
	}
}
