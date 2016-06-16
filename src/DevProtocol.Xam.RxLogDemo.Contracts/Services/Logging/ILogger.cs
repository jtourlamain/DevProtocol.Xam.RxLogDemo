using System;
namespace DevProtocol.Xam.RxLogDemo.Services.Logging
{
	public interface ILogger
	{
		string Name { get; }

		void Log(string message, LogLevel level = LogLevel.Info);
		void Log(Exception exception, LogLevel level = LogLevel.Error);
		void Log(string message, Exception exception, LogLevel level = LogLevel.Error);
		void Track(string analytic);

	}
}

