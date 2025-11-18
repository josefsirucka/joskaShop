// <copyright file="ArticleDto.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace JoskaShop.Models;

/// <summary>
/// Article model.
/// </summary>
public record ArticleDto
{
    /// <summary>
    /// Gets or sets the name of the article.
    /// </summary>
    [Required]
    public required string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the article.
    /// </summary>
    [Required]
    public required string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the article.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the price of the article.
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the currency of the article.
    /// </summary>
    [StringLength(3, MinimumLength = 3)]
    public string? Currency { get; set; }
}
