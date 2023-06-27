using ChessRules.Core.Interfaces;

namespace ChessRules.Infrastructure.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string text) => Console.WriteLine(text);
    }
}
