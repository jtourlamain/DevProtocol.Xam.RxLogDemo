using System;
using System.Reactive;
using Xamarin.Forms;
using System.Reactive.Disposables;
using Prism.Unity;
using Microsoft.Practices.Unity;
using DevProtocol.Xam.RxLogDemo.Views;
using DevProtocol.Xam.RxLogDemo.Services.Logging;


namespace DevProtocol.Xam.RxLogDemo
{
	public partial class App : PrismApplication
	{
		private Action<ILogRelayService> _bootstrapServices;
		public App(Action<ILogRelayService> bootstrapServices)
		{
			_bootstrapServices = bootstrapServices;
		}

		protected override void OnInitialized()
		{
			InitializeComponent();
			NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<MainPage>();
			Container.RegisterType<ILogRelayService, LogRelayService>(new ContainerControlledLifetimeManager());
		}

		protected override void OnStart()
		{
			_bootstrapServices(Container.Resolve<ILogRelayService>());
			base.OnStart();
		}

		protected override void OnSleep()
		{
			base.OnSleep();
		}

		protected override void OnResume()
		{
			base.OnResume();
		}


	}
}

