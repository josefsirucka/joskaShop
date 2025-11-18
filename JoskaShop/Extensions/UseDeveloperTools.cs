// <copyright file="UseDeveloperTools.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

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