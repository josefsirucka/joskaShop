// <copyright file="ArticlesControllers.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.Domains;
using JoskaShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoskaShop.Controllers;

/// <summary>
/// Articles controller.
/// </summary>
[ApiController]
[Route("api")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleDomain _articleDomain;
    private readonly ILogger<ArticlesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArticlesController"/> class.
    /// </summary>
    /// <param name="articleDomain">The article domain.</param>
    /// <param name="logger">The logger.</param>
    public ArticlesController(IArticleDomain articleDomain, ILogger<ArticlesController> logger)
    {
        _articleDomain = articleDomain;
        _logger = logger;
    }

    /// <summary>
    /// Creates an article.
    /// </summary>
    /// <returns>Task that represents the asynchronous operation with Action Result.</returns>
    [HttpPost("articles")]
    public async Task<ActionResult<Article>> CreateArticle([FromBody] ArticleDto articleDto)
    {
        IResult<Article> result = await _articleDomain.CreateAsync(articleDto);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetArticle), new { id = result.Value.ArticleId }, result.Value);
    }

    /// <summary>
    /// Gets an article by id.
    /// </summary>
    /// <param name="id">Id of the article.</param>
    /// <returns>Task that represents the asynchronous operation with Action Result.</returns>
    [HttpGet("articles/{id:long}")]
    public async Task<ActionResult> GetArticle(long id)
    {
        IResult<Article> result = await _articleDomain.GetAsync(id);

        if (!result.Success)
        {
            return NotFound(result.Message);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Search an article by name, description, or category..
    /// </summary>
    /// <param name="searchOrDescriptionName">Optional name or description to search for.</param>
    /// <param name="searchCategory">Optional category to search for.</param>
    /// <returns>Task that represents the asynchronous operation with Action Result.</returns>
    [HttpGet("articles")]
    public async Task<ActionResult<Article[]>> GetArticles([FromQuery] string? searchOrDescriptionName = null, [FromQuery] string? searchCategory = null)
    {
        if (string.IsNullOrWhiteSpace(searchOrDescriptionName) && string.IsNullOrWhiteSpace(searchCategory))
        {
            _logger.LogInformation("Getting all articles without filters.");
            IResult<IEnumerable<Article>> result = await _articleDomain.GetAllArticlesAsync();

            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Value.ToArray());
        }
        else
        {
            _logger.LogInformation("Getting articles with filters - Name/Description: {Name}, Category: {Category}", searchOrDescriptionName, searchCategory);
            IResult<Article[]> result = await _articleDomain.SearchAsync(searchOrDescriptionName, searchCategory);

            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Value.ToArray());
        }

    }
}