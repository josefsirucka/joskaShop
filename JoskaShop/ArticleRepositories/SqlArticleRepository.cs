// <copyright file="SqlArticleRepository.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.Models;
using JoskaShop.Services;
using Microsoft.EntityFrameworkCore;

namespace JoskaShop.ArticleRepositories;

/// <summary>
/// SQL Article repository.
/// </summary>
public class SqlArticleRepository : IArticleRepository
{
    private readonly DatabaseService _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlArticleRepository"/> class.
    /// </summary>
    /// <param name="db"></param>
    public SqlArticleRepository(DatabaseService db)
    {
        _db = db;
    }

    /// <inheritdoc/>
    public async Task<IResult<Article>> AddAsync(ArticleDto articleDto)
    {
        Article article = new()
        {
            Name = articleDto.Name,
            Description = articleDto.Description,
            Category = articleDto.Category,
            Price = articleDto.Price,
            Currency = articleDto.Currency,
        };

        _db.Articles.Add(article);
        await _db.SaveChangesAsync();
        return IResult.SuccessResult(article);
    }

    /// <inheritdoc/>
    public async Task<IResult<Article>> GetAsync(long id)
    {
        Article? article = await _db.Articles.SingleOrDefaultAsync(a => a.ArticleId == id);
        if (article == null)
        {
            return IResult.FailureResult<Article>($"Article with id {id} not found.");
        }

        return IResult.SuccessResult(article);
    }

    /// <inheritdoc/>
    public async Task<IResult<List<Article>>> GetAllAsync()
    {
        return IResult.SuccessResult(await _db.Articles.ToListAsync());
    }

    /// <inheritdoc/>
    public async Task<IResult<Article[]>> SearchAsync(string? nameOrDescription = null, string? category = null)
    {
        IQueryable<Article> query = _db.Articles;

        if (!string.IsNullOrWhiteSpace(nameOrDescription))
        {
            query = query.Where(a => EF.Functions.Like(a.Name, $"%{nameOrDescription}%") ||
                                     EF.Functions.Like(a.Description, $"%{nameOrDescription}%"));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(a => a.Category == category);
        }

        Article[] results = await query.ToArrayAsync();
        return IResult.SuccessResult(results);
    }
}
