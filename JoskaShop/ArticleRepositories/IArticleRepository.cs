// <copyright file="ArticleRepository.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.Models;

namespace JoskaShop;

/// <summary>
/// Article repository interface.
/// </summary>
public interface IArticleRepository
{
    /// <summary>
    /// Adds an article.
    /// </summary>
    /// <param name="article">Article.</param>
    /// <returns>The result of the add operation.</returns>
    Task<IResult<Article>> AddAsync(ArticleDto article);

    /// <summary>
    /// Gets an article by id.
    /// </summary>
    /// <param name="id">Id of the article.</param>
    /// <returns>The result of the get operation.</returns>
    Task<IResult<Article>> GetAsync(long id);

    /// <summary>
    /// Gets all articles.
    /// </summary>
    /// <returns>The result of the get all operation.</returns>
    Task<IResult<List<Article>>> GetAllAsync();

    /// <summary>
    /// Searches articles.
    /// </summary>
    /// <param name="nameOrDescription">Part of the name or description to search for.</param>
    /// <param name="category">Category to filter by.</param>
    /// <returns>The result of the search operation.</returns>
    Task<IResult<Article[]>> SearchAsync(string? nameOrDescription = null, string? category = null);
}
