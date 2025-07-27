using System;
using System.IO;
using System.Text.Json;
using System.Reflection;

namespace weather_app_wpf_mvvm.Core
{
	/// <summary>
	/// Provides access to application configuration settings using a thread-safe singleton pattern.
	/// Reads configuration from an 'app_config.json' file located in the application's root directory.
	/// </summary>
	public sealed class AppConfig
	{
		#region Singleton Implementation

		// Static instance of the AppConfig class, initialized lazily and in a thread-safe manner.
		// The Lazy<T> class ensures that the instance is created only when it's first accessed.
		private static readonly Lazy<AppConfig> lazyInstance = new Lazy<AppConfig>(() => new AppConfig());

		/// <summary>
		/// Gets the single, thread-safe instance of the AppConfig.
		/// </summary>
		public static AppConfig Instance => lazyInstance.Value;

		#endregion

		#region Configuration Properties

		/// <summary>
		/// The application key for accessing the web service.
		/// </summary>
		public string AppKey { get; }

		/// <summary>
		/// The base URL for the web service API.
		/// </summary>
		public string ApiBaseUrl { get; }
		public string LocationUrl { get; }
		public string ConditionUrl { get; }

		#endregion

		/// <summary>
		/// Private constructor to prevent external instantiation.
		/// This is where the configuration file is loaded and parsed.
		/// </summary>
		private AppConfig()
		{
			try
			{
				// Determine the path of the executable.
				string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string configFilePath = Path.Combine(exePath, "app_config.json");

				if (!File.Exists(configFilePath))
				{
					throw new FileNotFoundException("The configuration file 'app_config.json' was not found.", configFilePath);
				}

				// Read the entire JSON file.
				string jsonContent = File.ReadAllText(configFilePath);

				// Deserialize the JSON into a temporary helper class.
				var configData = JsonSerializer.Deserialize<AppSettingsData>(jsonContent);

				// Assign the values to the public properties.
				AppKey = configData?.AppKey;
				ApiBaseUrl = configData?.ApiBaseUrl;
				LocationUrl = configData?.ApiLocationURL;
				ConditionUrl = configData?.ApiCurrentConditionURL;

				// Validate that essential settings were loaded.
				if (string.IsNullOrEmpty(AppKey))
				{
					throw new InvalidOperationException("The 'AppKey' setting is missing or empty in 'app_config.json'.");
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions during file reading or parsing.
				// You might want to log this error to a file or show a message to the user.
				throw new InvalidOperationException("Failed to load or parse application configuration.", ex);
			}
		}

		/// <summary>
		/// Private helper class used for deserializing the JSON structure.
		/// The property names here must match the keys in the JSON file.
		/// </summary>
		private class AppSettingsData
		{
			public string AppKey { get; set; }
			public string ApiBaseUrl { get; set; }
			public string ApiLocationURL { get; set; }
			public string ApiCurrentConditionURL { get; set; }
		}
	}
}

