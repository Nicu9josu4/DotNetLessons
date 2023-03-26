using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILogger
    {
        void Log(string message);
    }
    internal class Message : ILogger
    {
        public void Log(string message) => Console.WriteLine(message + " From Message");
    }
    internal class MailMessage : ILogger
    {
        public void Log(string message) => Console.WriteLine(message + " From MailMessage");
    }
    internal class Logger
    {
        ILogger logger;
        public string Text { get; set; }
        public Logger(ILogger logger)
        {
            this.logger = logger;
        }
        public void Print() => logger.Log(Text);
    }
}
