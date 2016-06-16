using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace DevProtocol.Xam.RxLogDemo.Services.Logging
{
	public sealed class LogRelayService : ILogRelayService
	{
		private readonly IDictionary<string, ILogger> _loggers;
		private readonly ISubject<LogEntry> _entries;
		private readonly object _sync;
		private LogLevel _threshold;

		public LogRelayService()
		{
			_loggers = new Dictionary<string, ILogger>();
			_entries = new ReplaySubject<LogEntry>(64);
			_sync = new Object();
		}

		public LogLevel Threshold
		{
			get { return _threshold; }
			set { _threshold = value; }
		}

		public bool IsDebugEnabled => _threshold <= LogLevel.Debug;

		public bool IsInfoEnabled => _threshold <= LogLevel.Info;

		public bool IsPerfEnabled => _threshold <= LogLevel.Perf;

		public bool IsWarnEnabled => _threshold <= LogLevel.Warn;

		public bool IsTrackEnabled => _threshold <= LogLevel.Track;

		public bool IsErrorEnabled => _threshold <= LogLevel.Error;

		public IObservable<LogEntry> Entries => _entries.Where(x => x.Level >= this.Threshold);


		public ILogger GetLogger(string name)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			lock (_sync)
			{
				ILogger logger;

				if (!_loggers.TryGetValue(name, out logger))
				{
					logger = new Logger(this, name);
					_loggers.Add(name, logger);
				}
				return logger;
			}
		}

		public ILogger GetLogger(Type forType)
		{
			if (forType == null)
				throw new ArgumentNullException(nameof(forType));
			if (forType.IsConstructedGenericType)
			{
				forType = forType.GetGenericTypeDefinition();
			}
			return GetLogger(forType.FullName);
		}

		private sealed class Logger : ILogger
		{
			private readonly LogRelayService _owner;
			private readonly string _name;

			public Logger(LogRelayService owner, string name)
			{
				_owner = owner;
				_name = name;
			}

			public string Name => _name;

			public void Log(string message, LogLevel level = LogLevel.Debug)
			{
				var entry = new LogEntry(DateTime.UtcNow, _name, level, Environment.CurrentManagedThreadId, message);
				_owner._entries.OnNext(entry);
			}


			public void Log(Exception exception, LogLevel level = LogLevel.Error)
			{
				Log(exception, level);
			}

			public void Log(string message, Exception exception, LogLevel level = LogLevel.Error)
			{
				Log($"{message} : {exception}", level);
			}

			public void Track(string analytic)
			{
				Log(analytic, LogLevel.Track);
			}
		}
	}
}

