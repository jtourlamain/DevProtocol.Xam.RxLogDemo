using System;
using System.Reactive.Disposables;
using Xamarin.Forms;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using DevProtocol.Xam.RxLogDemo.Services.Logging;


namespace DevProtocol.Xam.RxLogDemo.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		private readonly CompositeDisposable _subscriptionDisposables = new CompositeDisposable();
		private readonly ILogger _logger;

		public MainPageViewModel(ILogRelayService logService)
		{
			DoSomethingCommand = new DelegateCommand(DoSomething).ObservesCanExecute((p) => IsActive);
			_logger = logService.GetLogger(this.GetType());

		}

		#region Bindable properties
		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		private string _myOutput;
		public string MyOutput
		{
			get { return _myOutput; }
			set { SetProperty(ref _myOutput, value); }
		}
		private bool _isActive;
		public bool IsActive
		{
			get { return _isActive; }
			set { SetProperty(ref _isActive, value);
				_logger.Log($"{nameof(IsActive)} changed");
			}
		}
		#endregion

		#region Commands
		public DelegateCommand DoSomethingCommand { get; private set;}
		#endregion

		private void DoSomething()
		{
			MyOutput += DateTime.Now + "\n";
			_logger.Track(DateTime.Now.ToString());
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			_subscriptionDisposables.Clear();
		}


		public void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("title"))
				Title = (string)parameters["title"] + " and Prism";
}

	}
}


