﻿using Serilog;
using Serilog.Events;

namespace MediaInAction.Shared.Hosting.AspNetCore;

public static class SerilogConfigurationHelper
{
    public static void Configure(string applicationName)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", $"{applicationName}")
            .Enrich.WithThreadId()
            .WriteTo.Async(c => c.File("Logs/logs-.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();
    }
}