// <copyright file="IArticleDomain.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.Models;

namespace JoskaShop.Domains;

/// <summary>
/// Contract for article domain.
/// </summary>
public interface IArticleDomain
{
    /// <summary>
    /// Creates the article.
    /// </summary>
    /// <param name="dto">Article dto.</param>
    /// <returns>Result from domain.</returns>
    Task<IResult<Article>> CreateAsync(ArticleDto dto);

    /// <summary>
    /// Gets the article by id.
    /// </summary>
    /// <param name="id">Article id.</param>
    /// <returns>Result from domain.</returns>
    Task<IResult<Article>> GetAsync(long id);

    /// <summary>
    /// Searches articles.
    /// </summary>
    /// <param name="nameOrDescription">Part of the name or description to search for.</param>
    /// <param name="category">Category to filter by.</param>
    /// <returns>Result from domain.</returns>
    Task<IResult<Article[]>> SearchAsync(string? nameOrDescription = null, string? category = null);

    /// <summary>
    /// Gets all articles.
    /// </summary>
    /// <returns>Result from domain.</returns>
    Task<IResult<IEnumerable<Article>>> GetAllArticlesAsync();
}