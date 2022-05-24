using Microsoft.AspNetCore.Hosting;
using PumoxRESTful.Controllers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using PumoxRESTful;

string url = "https://localhost:12345";

var commandLoopTask = Task.Run(() => CommandLoop(url));

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseKestrel()
        .UseStartup<Startup>()
        .UseUrls(url)
        .ConfigureLogging(loggingBuilder => loggingBuilder.ClearProviders());
    });


await Task.WhenAny(builder.RunConsoleAsync(), commandLoopTask);


static void CommandLoop(string url)
{
	Console.WriteLine($"Stubbed endpoint: GET {url}");

	while (true)
	{
        var args = Console.ReadLine().Split();
        
	}
}
