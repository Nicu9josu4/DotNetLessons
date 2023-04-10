namespace DotNetLessons.FileLogger
{
    public class FileLogger : ILogger, IDisposable
    {
        string _filePath;
        static object _lock = new object();
        public FileLogger(string path)
        {
            _filePath = path;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            /// returneaza acest obiect care reprezinta un camp de vedere.
            return this;
        }

        public void Dispose(){ }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, 
            TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                /// Locul unde va fi efectuat logarea
                File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
            }
        }


    }
}
