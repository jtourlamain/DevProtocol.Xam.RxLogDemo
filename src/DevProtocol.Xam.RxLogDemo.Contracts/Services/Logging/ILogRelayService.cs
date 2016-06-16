using System;
namespace DevProtocol.Xam.RxLogDemo.Services.Logging
{
	public interface ILogRelayService
	{
		LogLevel Threshold
		{
			get;
			set;
		}

		bool IsDebugEnabled
		{
			get;
		}

		bool IsInfoEnabled
		{
			get;
		}

		bool IsPerfEnabled
		{
			get;
		}

		bool IsWarnEnabled
		{
			get;
		}

		bool IsTrackEnabled
		{
			get;
		}

		bool IsErrorEnabled
		{
			get;
		}

		IObservable<LogEntry> Entries
		{
			get;
		}

		ILogger GetLogger(Type forType);

		ILogger GetLogger(string name);
	}
}

