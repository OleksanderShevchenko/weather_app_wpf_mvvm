using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.AccuWeatherHelpers
{
	public class AccuWeatherHelper
	{
		private string baseURL = "";
		private string appKey = "";
		private string locationURL = "";
		private string curentConditionURL = "";

		public AccuWeatherHelper(string appKey = "", 
			string baseUrl = "http://dataservice.accuweather.com", 
			string locationUrl= "locations/v1/cities/autocomplete?apikey={0}&q={1}", 
			string curentConditionUrl = "currentconditions/v1/{0}?apikey={1}")
		{
			this.appKey = appKey;
			baseURL = baseUrl;
			locationURL = locationUrl;
			curentConditionURL = curentConditionUrl;
		}

		public async Task<List<City>> GetCities(string query)
		{
			List<City> cities = new List<City>();
			if (query != null && this.appKey != "" && query != "")
			{
				string url = this.baseURL + "/" + string.Format(this.locationURL, this.appKey, query);

				using (HttpClient client = new HttpClient())
				{
					var response = await client.GetAsync(url);

					string json = await response.Content.ReadAsStringAsync();
					if (!string.IsNullOrEmpty(json))
					{
						List<City>  deserializedResult = JsonConvert.DeserializeObject<List<City>>(json);
						if(deserializedResult != null && deserializedResult.Count > 0)
						{
							cities = deserializedResult;
						}
					}
					
				}
			}
			return cities;
		}

		public async Task<WeatherCondition> GetWeatherCondition(string cityKey)
		{
			WeatherCondition currentCondition = new WeatherCondition();
			if (cityKey != null && this.appKey != "" && cityKey != "")
			{
				string url = this.baseURL + "/" + string.Format(this.curentConditionURL, cityKey, this.appKey);

				using (HttpClient client = new HttpClient())
				{
					var response = await client.GetAsync(url);

					string json = await response.Content.ReadAsStringAsync();
					if (!string.IsNullOrEmpty(json))
					{
						List<WeatherCondition> deserializedResult = JsonConvert.DeserializeObject<List<WeatherCondition>>(json);
						if (deserializedResult != null && deserializedResult.Count > 0)
						{
							currentCondition = deserializedResult[0];
						}
					}

				}
			}
			return currentCondition;
		}
	}
}
