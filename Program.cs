using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using SmartProfiler.CLI.Presentation.Commands;
using SmartProfiler.CLI.Core.Interfaces;
using SmartProfiler.CLI.Infrastructure.DependencyInjection;
using SmartProfiler.Profilers;
using SmartProfiler.CLI.Infrastructure.Diagnostics;
using SmartProfiler.CLI.Application.Profilers.Common;
using Spectre.Console;

class Program
{
    public static int Main(string[] args)
    {
        // Register services
        var services = new ServiceCollection();
        services.AddSingleton<IProfileEnricher, MethodProfilerEngine>();
        services.AddSingleton<IProfilerEngine, ProcessProfilerEngine>();
        services.AddSingleton<ICpuDiagnostics, CpuDiagnostics>();

        // Register CLI dependencies
        var registrar = new TypeRegistrar(services);

        // Setup command app with DI
        var app = new CommandApp<ProfileCommand>(registrar);
        app.Configure(config =>
        {
            config.SetApplicationName("SmartProfiler.CLI");
            config.AddCommand<ProfileCommand>("profile")
                  .WithDescription("Profile a .NET application and show method-level CPU usage.");
        });

        app.Configure(config =>
        {
            config.AddCommand<ProfileCommand>("profile")
                  .WithDescription("Profiles a method in the specified assembly")
                  .WithExample(new[] {
                      "profile",
                      "DummyTestApp",
                      "--method", "DummyTestApp.MemoryCruncher.SimulateHeavyMemoryAndCPU",
                      "--assembly", "./DummyTestApp/bin/Debug/net8.0/DummyTestApp.dll",
                      "--detailed-memory",
                      "--memory-log", "./memory-log.txt"
                  });
        });


        return app.Run(args);
    }
}