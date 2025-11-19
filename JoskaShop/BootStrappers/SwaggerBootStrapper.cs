// <copyright file="SwaggerBootStrapper.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JoskaShop.BootStrappers;

/// <summary>
/// Swagger configuration.
/// </summary>
public static partial class SwaggerBootStrapper
{
    /// <summary>
    /// Applies the configuration to Swagger options.
    /// </summary>
    /// <param name="options">Swagger options.</param>
    public static void ApplyConfiguration(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", GetSwaggerInfo());

        // Set the comments path for the Swagger JSON and UI.
        string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
        options.CustomSchemaIds(GetSwaggerCustomSchemeIds);
        options.SchemaFilter<SwaggerEnumSchemeFilter>();

        // Configure Enums
        options.UseAllOfForInheritance();
        options.UseInlineDefinitionsForEnums();
    }

    private static OpenApiInfo GetSwaggerInfo()
    {
        return new OpenApiInfo
        {
            Title = "Joska Shop Api Demo",
            Version = "v1",
            Description = "I want to be the best developer.",
            Contact = new OpenApiContact() { Name = "Josef Širůčka", Email = "joska@papirfly.com" },
        };
    }

    private static string GetSwaggerCustomSchemeIds(Type type)
    {
        string fullName = type.FullName ?? "<MissingName>";

        // Remove the top-level namespace
        const string TOP_LEVEL_NAMESPACE = "PFCZH.";
        if (fullName.StartsWith(TOP_LEVEL_NAMESPACE))
        {
            fullName = fullName[TOP_LEVEL_NAMESPACE.Length..];
        }

        Regex regex = MyRegex();
        return regex.Replace(fullName, ".");
    }

    [GeneratedRegex("\\W+")]
    private static partial Regex MyRegex();

    private class SwaggerEnumSchemeFilter : ISchemaFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();

                foreach (object? value in Enum.GetValues(context.Type))
                {
                    schema.Enum.Add(new OpenApiString($"{(int)value} - {value}"));
                }
            }
        }
    }
}
