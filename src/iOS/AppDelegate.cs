using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using DevProtocol.Xam.RxLogDemo.Services.Logging;
using DevProtocol.Xam.RxLogDemo.iOS.Logging;

namespace DevProtocol.Xam.RxLogDemo.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App(BootstrapServices));

			//DevProtocol.Xam.RxLogDemo.iOS.Logging.HockeyAppLogger.StartListening()
			return base.FinishedLaunching(app, options);
		}

		private void BootstrapServices(ILogRelayService logRelayService)
		{
			HockeyAppLogger.StartListening(logRelayService.Entries);
			AzureLogger.StartListening(logRelayService.Entries);
		}
	}
}

