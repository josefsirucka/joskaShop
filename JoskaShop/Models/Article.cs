// <copyright file="Article.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

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
