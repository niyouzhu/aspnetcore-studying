using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            Console.WriteLine($"Inital config sources: {builder.Sources.Count()}");
            builder.AddInMemoryCollection(new Dictionary<string, string> { { "username", "Guest" } });
            Console.WriteLine($"Added memory source. Sources: {builder.Sources.Count()}");
            builder.AddCommandLine(args);
            Console.WriteLine($"Added command line source. Sources: {builder.Sources.Count()}");
            var config = builder.Build();
            string username = config.GetValue<string>("username");
            Console.WriteLine($"Hello, {username}!");
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
