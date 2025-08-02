using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace weather_app_wpf_mvvm.ViewModel.Commands
{
	public class OpenURL : ICommand
	{
		public WeatherVM VM { get; set; }

		public OpenURL(WeatherVM vm)
		{
			this.VM = vm;
		}
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			if (parameter is string url && !string.IsNullOrWhiteSpace(url))
			{ 
				return true;
			}
			return false;
		}
		public void Execute(object parameter)
		{
			VM.OpenURL();

		}
	}
}
