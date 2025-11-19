// <copyright file="JoskaShopApp.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.BootStrappers;
using JoskaShop.Extensions;
using JoskaShop.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace JoskaShop;

/// <summary>
/// Main application class.
/// </summary>
public class JoskaShopApp
{
    private const int APPLICATION_EXIT = 0;
    private const int APPLICATION_ERROR = 1;

    /// <summary>
    /// Application entry point.
    /// </summary>
    /// <param name="args">Arguments.</param>
    public async Task<int> Main(string[] args)
    {
        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            Log.Logger = Logging.CreateBootstrapLogger(builder.Configuration);
            builder.Services.RegisterAllServices(
                builder.Configuration,
                builder.Environment.IsDevelopment()
            );

            WebApplication app = builder.Build();
            app.UseDeveloperToolsExtension();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllers();

            app.Run();
            return APPLICATION_EXIT;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            return APPLICATION_ERROR;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
