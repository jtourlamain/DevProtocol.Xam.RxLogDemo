using System;
using System.Reactive.Linq;
using System.Diagnostics;
using DevProtocol.Xam.RxLogDemo.Services.Logging;

namespace DevProtocol.Xam.RxLogDemo.iOS.Logging
{
	public static class HockeyAppLogger
	{
		public static IDisposable StartListening(IObservable<LogEntry> entries)
		{
			return entries.Where(obs => obs.Level == LogLevel.Track).Subscribe(
				obs => {
					Debug.WriteLine("------> To HockeyApp ----->" + obs.Message);
			});
		}

	}
}

