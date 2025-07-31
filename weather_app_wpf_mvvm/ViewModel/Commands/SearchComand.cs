using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace weather_app_wpf_mvvm.ViewModel.Commands
{
    public class SearchComand: ICommand
	{
		public WeatherVM VM { get; set; }

		public event EventHandler CanExecuteChanged;

		public SearchComand(WeatherVM vm)
		{
			this.VM = vm;
			//// Subscribe to the PropertyChanged event of the VM to update CanExecute state
			//this.VM.PropertyChanged += (s, e) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}
		public async void Execute(object parameter)
		{
			VM.FetchCitiesAsync();
		}
	}
    
}
