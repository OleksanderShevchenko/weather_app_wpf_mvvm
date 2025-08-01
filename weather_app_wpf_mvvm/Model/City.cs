using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
	public class LocalizationArea
	{
		public string ID { get; set; }
		public string LocalizedName { get; set; }
	}

	public class City
	{
		public int Version { get; set; }
		public string Key { get; set; }
		public string Type { get; set; }
		public int Rank { get; set; }
		public string LocalizedName { get; set; }
		public LocalizationArea Country { get; set; }
		public LocalizationArea AdministrativeArea { get; set; }
		public string FullName 
		{ get 
			{
				return $"{LocalizedName} ({AdministrativeArea.LocalizedName} - {Country.LocalizedName})";
			}
		}
	}
}
