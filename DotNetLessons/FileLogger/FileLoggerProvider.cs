namespace DotNetLessons.FileLogger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        string _path;
        public FileLoggerProvider(string path)
        {
            _path = path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName);
        }

        public void Dispose() { }
    }
}
