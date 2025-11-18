// <copyright file="DatabaseService.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JoskaShop.Services;

/// <summary>
/// Main Database service.
/// </summary>
public class DatabaseService : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseService"/> class.
    /// </summary>
    /// <param name="options">Options for configuring the database service.</param>
    public DatabaseService(DbContextOptions<DatabaseService> options)
    : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the table with all Articles.
    /// </summary>
    public DbSet<Article> Articles { get; set; }
}