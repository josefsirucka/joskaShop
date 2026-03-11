// <copyright file="GlobalUsings.cs" company="Josef Širůčka">
// Copyright (c) Josef Širůčka. All rights reserved.
// </copyright>
// <summary>Created on: 11.03 2026</summary>

using Serilog;
using ILogger = Serilog.ILogger;

namespace JoskaShop.BootStrappers;

/// <summary>
/// Serial logging configuration.
/// </summary>
public class Logging
{
    /// <summary>
    /// Factory method to create a  logger.
    /// </summary>
    /// <param name="configuration">Configuration instance.</param>
    /// <returns>New instance of Serilog logger.</returns>
    public static ILogger CreateBootstrapLogger(IConfiguration configuration)
    {
        return new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
    }
}
