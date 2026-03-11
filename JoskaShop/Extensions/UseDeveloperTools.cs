// <copyright file="GlobalUsings.cs" company="Josef Širůčka">
// Copyright (c) Josef Širůčka. All rights reserved.
// </copyright>
// <summary>Created on: 11.03 2026</summary>

namespace JoskaShop.Extensions;

/// <summary>
/// Developer tools extension.
/// </summary>
public static class UseDeveloperTools
{
    /// <summary>
    /// Use developer tools.
    /// </summary>
    /// <param name="app">Web application.</param>
    public static void UseDeveloperToolsExtension(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
    }
}
