// <copyright file="GlobalUsings.cs" company="Josef Širůčka">
// Copyright (c) Josef Širůčka. All rights reserved.
// </copyright>
// <summary>Created on: 11.03 2026</summary>

namespace JoskaShop.Models;

/// <summary>
/// Article model.
/// </summary>
public record Article : ArticleDto
{
    /// <summary>
    /// Gets or sets the article identifier.
    /// </summary>
    public long ArticleId { get; set; }
}
