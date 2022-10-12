using System;
namespace WebApi.Services
{
    public class ConsoleLogger: ILoggerService
    {
        
        public void write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}

