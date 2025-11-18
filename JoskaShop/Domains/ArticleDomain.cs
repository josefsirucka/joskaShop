// <copyright file="ArticleDomain.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.Models;
using JoskaShop.Services;

namespace JoskaShop.Domains;

/// <summary>
/// Article domain implementation.
/// </summary>
public class ArticleDomain : IArticleDomain
{
    private readonly IArticleRepository _articleRepository;
    private readonly ILogger<ArticleDomain> _logger;
    private readonly CurrencyService _currencyService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleDomain"/> class.
    /// </summary>
    /// <param name="articleRepository">The article repository.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="currencyService">The currency service.</param>
    public ArticleDomain(IArticleRepository articleRepository, ILogger<ArticleDomain> logger, CurrencyService currencyService)
    {
        _articleRepository = articleRepository;
        _logger = logger;
        _currencyService = currencyService;
    }

    /// <inheritdoc/>
    public async Task<IResult<Article>> CreateAsync(ArticleDto dto)
    {
        if (dto.Name.Length > 64)
        {
            _logger.LogDebug("Article name too long: {Name}", dto.Name);
            return IResult.FailureResult<Article>("Article name cannot be longer than 64 characters.");
        }

        if (dto.Description.Length > 2048)
        {
            _logger.LogDebug("Article description too long: {Description}", dto.Description);
            return IResult.FailureResult<Article>("Article description cannot be longer than 2048 characters.");
        }

        if (dto.Category?.Length > 64)
        {
            _logger.LogDebug("Article category too long: {Category}", dto.Category);
            return IResult.FailureResult<Article>("Article category cannot be longer than 64 characters.");
        }

        if (dto.Price < 0)
        {
            _logger.LogDebug("Article price negative: {Price}", dto.Price);
            return IResult.FailureResult<Article>("Article price cannot be negative.");
        }

        if (dto.Price > 0 && string.IsNullOrWhiteSpace(dto.Currency))
        {
            _logger.LogDebug("Article currency missing for price: {Price}", dto.Price);
            return IResult.FailureResult<Article>("Article currency must be specified when price is greater than zero.");
        }

        if (dto.Price > 0 && !_currencyService.IsIso4217Code(dto.Currency))
        {
            _logger.LogDebug("Article currency invalid: {Currency}", dto.Currency);
            return IResult.FailureResult<Article>("Article currency must be a valid ISO 4217 currency code.");
        }

        IResult<Article> result = await _articleRepository.AddAsync(dto);

        if (!result.Success)
        {
            _logger.LogDebug("Failed to add article: {Message}", result.Message);
            return IResult.FailureResult<Article>($"Failed to add article: {result.Message}");
        }

        _logger.LogInformation("Article created with id {ArticleId}", result.Value.ArticleId);
        return IResult.SuccessResult(result.Value);
    }

    /// <inheritdoc/>
    public async Task<IResult<IEnumerable<Article>>> GetAllArticlesAsync()
    {
        IResult<List<Article>> result = await _articleRepository.GetAllAsync();
        if (!result.Success)
        {
            _logger.LogDebug("Failed to get articles: {Message}", result.Message);
            return IResult.FailureResult<IEnumerable<Article>>($"Failed to get articles: {result.Message}");
        }

        IEnumerable<Article> articles = result.Value.AsEnumerable();
        return IResult.SuccessResult(articles);
    }

    /// <inheritdoc/>
    public async Task<IResult<Article>> GetAsync(long id)
    {
        IResult<Article> result = await _articleRepository.GetAsync(id);
        if (!result.Success)
        {
            _logger.LogDebug("Failed to get article with id {ArticleId}: {Message}", id, result.Message);
            return IResult.FailureResult<Article>($"Failed to get article with id {id}: {result.Message}");
        }

        return IResult.SuccessResult(result.Value);
    }

    /// <inheritdoc/>
    public async Task<IResult<Article[]>> SearchAsync(string? nameOrDescription = null, string? category = null)
    {
        IResult<Article[]> results = await _articleRepository.SearchAsync(nameOrDescription, category);
        
        if (!results.Success)
        {
            _logger.LogDebug("Failed to search articles: {Message}", results.Message);
            return IResult.FailureResult<Article[]>($"Failed to search articles: {results.Message}");
        }

        return IResult.SuccessResult(results.Value);
    }
}