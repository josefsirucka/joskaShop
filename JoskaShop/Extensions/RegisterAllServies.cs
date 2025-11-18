// <copyright file="RegisterAllServies.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.ArticleRepositories;
using JoskaShop.BootStrappers;
using JoskaShop.Domains;
using JoskaShop.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace JoskaShop;

/// <summary>
/// Service registrator extension.
/// </summary>
public static class ServicesRegistrator
{
    /// <summary>
    /// Register all services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <param name="isDevelopment">Is development environment.</param>
    public static void RegisterAllServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o => SwaggerBootStrapper.ApplyConfiguration(o));
        services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.DefaultIgnoreCondition = System
                .Text
                .Json
                .Serialization
                .JsonIgnoreCondition
                .WhenWritingNull
            );
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(Log.Logger));

        if (isDevelopment)
        {
            services.AddSingleton<IArticleRepository, InMemoryArticleRepository>();
        }
        else
        {
            services.AddDbContext<DatabaseService>(options =>
                 options.UseSqlite(configuration.GetValue<string>("DefaultConnection"),
                options => options.CommandTimeout(10)
            ));

            services.AddScoped<IArticleRepository, SqlArticleRepository>();
        }

        services.AddScoped<IArticleDomain, ArticleDomain>();
        services.AddScoped<CurrencyService>();
        
    }
}
