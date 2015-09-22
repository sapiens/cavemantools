using System;

namespace CavemanTools.Logging
{
    public class ConsoleLogger:DeveloperLogger
    {
        public ConsoleLogger() : base(Console.WriteLine)
        {
        }
    }
}