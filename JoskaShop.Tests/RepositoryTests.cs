// <copyright file="RepositoryTests.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using JoskaShop.ArticleRepositories;
using JoskaShop.Models;
using PerfectResult;

namespace JoskaShop.Tests;

public class RepositoryTests
{
    private readonly IArticleRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryTests"/> class.
    /// </summary>
    public RepositoryTests()
    {
        _repository = new InMemoryArticleRepository();
    }

    [Test]
    public async Task AddAndGetArticle_ShouldReturnSameArticle()
    {
        ArticleDto dto = new()
        {
            Name = "Branded Memory Stick",
            Description = "Branded 16 GB memory stick.",
            Category = "USB flash drive",
            Price = 17.89m,
            Currency = "NOK",
        };

        ArticleDto mugDto = new()
        {
            Name = "Branded Drinking Mug",
            Description = "Porcelain drinking cup with your logo on it.",
            Category = "Mug",
            Price = 0
        };

        IResult<Article> addResult = await _repository.AddAsync(dto);
        Assert.That(addResult.Success, "Failed to add article.");

        IResult<Article> addMugResult = await _repository.AddAsync(mugDto);
        Assert.That(addMugResult.Success, "Failed to add mug article.");

        long articleId = addResult.Value.ArticleId;
        IResult<Article> getResult = await _repository.GetAsync(articleId);

        Assert.That(getResult.Success, "Failed to get article.");
        Article retrievedArticle = getResult.Value;
        Assert.Multiple(() =>
        {
            Assert.That(dto.Name, Is.EqualTo(retrievedArticle.Name));
            Assert.That(dto.Description, Is.EqualTo(retrievedArticle.Description));
            Assert.That(dto.Category, Is.EqualTo(retrievedArticle.Category));
            Assert.That(dto.Price, Is.EqualTo(retrievedArticle.Price));
            Assert.That(dto.Currency, Is.EqualTo(retrievedArticle.Currency));
        });

        IResult<Article> getNonExistentResult = await _repository.GetAsync(9999);
        Assert.That(getNonExistentResult.Success, Is.False, "Expected failure when getting non-existent article.");

        IResult<List<Article>> getAllResult = await _repository.GetAllAsync();
        Assert.That(getAllResult.Success, "Failed to get all articles.");
        IEnumerable<Article> allArticles = getAllResult.Value;
        Assert.That(allArticles, Is.Not.Empty, "Expected at least one article in the repository.");
    }

    
}