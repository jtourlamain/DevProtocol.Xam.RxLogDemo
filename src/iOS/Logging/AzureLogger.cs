using System;
using System.Reactive.Linq;
using System.Diagnostics;
using DevProtocol.Xam.RxLogDemo.Services.Logging;

namespace DevProtocol.Xam.RxLogDemo.iOS.Logging
{
	public static class AzureLogger
	{
		public static IDisposable StartListening(IObservable<LogEntry> entries)
		{
			return entries.Where(obs => obs.Level != LogLevel.Track).Subscribe(
				obs =>
				{
					Debug.WriteLine("------> To Azure ----->" + obs.Message);
				});
		}
	}
}

