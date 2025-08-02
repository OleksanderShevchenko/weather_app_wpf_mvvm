using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace weather_app_wpf_mvvm.ViewModel.ValueConverters
{
    public class BoolToRainConvertor : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool rainStatus = (bool)value;
			if (rainStatus)
				return "It is raining";
			return "It is not raining";
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string rainStatus = (string)value;
			if (rainStatus == "It is raining")
				return true;
			return false;
		}
		
	}
   
}
