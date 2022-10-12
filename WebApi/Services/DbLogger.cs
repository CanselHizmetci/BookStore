using System;
namespace WebApi.Services
{
    public class DbLogger : ILoggerService
    {
        public void write(string message)
        {
            Console.WriteLine("[DbLogger] - " + message);
        }
    }
}

