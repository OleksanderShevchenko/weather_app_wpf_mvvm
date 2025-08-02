using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace weather_app_wpf_mvvm.View
{
	/// <summary>
	/// Interaction logic for WeatherWindow.xaml
	/// </summary>
	public partial class WeatherWindow : Window
	{
		public WeatherWindow()
		{
			InitializeComponent();
		}

		private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			var psi = new ProcessStartInfo
			{
				FileName = e.Uri.AbsoluteUri, // Беремо URL з самого гіперпосилання
				UseShellExecute = true        // Використовуємо системну оболонку для відкриття (тобто браузер)
			};

			// Запускаємо процес
			Process.Start(psi);

			// Позначаємо подію як оброблену
			e.Handled = true;
		}
    }
}
