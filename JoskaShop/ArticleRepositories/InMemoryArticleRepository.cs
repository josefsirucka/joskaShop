// <copyright file="InMemoryArticleRepository.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.Models;

namespace JoskaShop.ArticleRepositories;

/// <summary>
/// In-memory Article repository.
/// </summary>
public class InMemoryArticleRepository : IArticleRepository
{
    private readonly List<Article> _articles = [];

    /// <inheritdoc/>
    public Task<IResult<Article>> AddAsync(ArticleDto articleDto)
    {
        Article article = new()
        {
            ArticleId = _articles.Count + 1,
            Name = articleDto.Name,
            Description = articleDto.Description,
            Category = articleDto.Category,
            Price = articleDto.Price,
            Currency = articleDto.Currency,
        };

        _articles.Add(article);
        return Task.FromResult(IResult.SuccessResult(article));
    }

    /// <inheritdoc/>
    public Task<IResult<Article>> GetAsync(long id)
    {
        Article? article = _articles.SingleOrDefault(a => a.ArticleId == id);
        if (article == null)
        {
            return Task.FromResult(
                IResult.FailureResult<Article>($"Article with id {id} not found.")
            );
        }

        return Task.FromResult(IResult.SuccessResult(article));
    }

    /// <inheritdoc/>
    public Task<IResult<List<Article>>> GetAllAsync()
    {
        return Task.FromResult(IResult.SuccessResult(_articles.ToList()));
    }

    /// <inheritdoc/>
    public Task<IResult<Article[]>> SearchAsync(
        string? nameOrDescription = null,
        string? category = null
    )
    {
        IEnumerable<Article> query = _articles;

        if (!string.IsNullOrWhiteSpace(nameOrDescription))
        {
            query = query.Where(a =>
                a.Name.Contains(nameOrDescription, StringComparison.OrdinalIgnoreCase)
                || a.Description.Contains(nameOrDescription, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(a =>
                a.Category != null
                && a.Category.Equals(category, StringComparison.OrdinalIgnoreCase)
            );
        }

        return Task.FromResult(IResult.SuccessResult(query.ToArray()));
    }
}
