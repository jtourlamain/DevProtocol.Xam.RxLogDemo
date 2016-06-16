using System;

namespace DevProtocol.Xam.RxLogDemo.Services.Logging
{
	public class LogEntry
	{
		private readonly DateTime _timestamp;
		private readonly string _name;
		private readonly LogLevel _level;
		private readonly int _threadId;
		private readonly string _message;

		public LogEntry(DateTime timestamp, string name, LogLevel level, int threadId, string message)
		{
			_timestamp = timestamp;
			_name = name;
			_level = level;
			_threadId = threadId;
			_message = message;
		}

		public DateTime Timestamp => _timestamp;
		public string Name => _name;
		public LogLevel Level => _level;
		public int ThreadId => _threadId;
		public string Message => _message;
	}
}

