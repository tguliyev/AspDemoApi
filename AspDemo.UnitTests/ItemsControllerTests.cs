using System;
using System.Threading.Tasks;
using AspDemo.Api.Controllers;
using AspDemo.Api.DTOs;
using AspDemo.Api.Models;
using AspDemo.Api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AspDemo.UnitTests;

public class ItemsControllerTests
{
    private readonly Random Rand = new Random();
    private readonly Mock<IItemsRepository> RepositoryStub = new Mock<IItemsRepository>();

    [Fact]
    public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
    {
        // Arrange
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync((Item)null);

        ItemsController Controller = new ItemsController(RepositoryStub.Object);
        
        // Act
        ActionResult<ItemDTO> Result = await Controller.GetItemAsync(Rand.Next());

        // Assert
        Assert.IsType<NotFoundResult>(Result.Result);
    }

    [Fact]
    public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
    {
        // Arrange 
        Item ExpectedItem = CreateRandomItem();
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync(ExpectedItem);

        ItemsController Controller = new ItemsController(RepositoryStub.Object);

        // Act 
        OkObjectResult? Result = (await Controller.GetItemAsync(Rand.Next())).Result as OkObjectResult;

        // Assert
        Result?.Value.Should().BeEquivalentTo(
            ExpectedItem, 
            options => options.ComparingByMembers<Item>());
    }

    [Fact]
    public async Task GetItemsAsync_WithExistingItems_ReturnsAllItems()
    {
        // Arrange
        Item[] ExspectedItems = new Item[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };
        RepositoryStub.Setup(repo => repo.GetItemsAsync()).ReturnsAsync(ExspectedItems);

        ItemsController Controller = new ItemsController(RepositoryStub.Object);

        // Act
        OkObjectResult? Result = (await Controller.GetItemsAsync()).Result as OkObjectResult;

        // Assert
        Result?.Value.Should().BeEquivalentTo(
            ExspectedItems,
            options => options.ComparingByMembers<Item>());
    }

    [Fact]
    public async Task CreateItemAsync_WithItemToCreate_ReturnsCreatedItem()
    {
        // Arrange
        CreateItemDTO ItemToCreate = new CreateItemDTO
        {
            Name = Guid.NewGuid().ToString(),
            Price = Rand.Next()
        };

        ItemsController Controller = new ItemsController(RepositoryStub.Object);

        // Act
        CreatedAtActionResult? Result = (await Controller.CreateItemAsync(ItemToCreate)).Result as CreatedAtActionResult;

        // Assert
        Result?.Value.Should().BeEquivalentTo(
            ItemToCreate,
            options => options.ComparingByMembers<ItemDTO>().ExcludingMissingMembers());
    }

    [Fact]
    public async Task UpdateItemAsync_WithUnexistingItem_ReturnsNotFound()
    {
        // Arrange
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync((Item)null);

        ItemsController Controller = new ItemsController(RepositoryStub.Object);

        UpdateItemDTO UpdateingItem = new UpdateItemDTO()
        {
            Name = Guid.NewGuid().ToString(),
            Price = Rand.Next()
        };
        int Id = Rand.Next();

        // Act
        ActionResult Result = await Controller.UpdateItemAsync(Id, UpdateingItem);

        // Assert
        Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateItemAsync_WithExistingItem_ReturnsNoContent()
    {
        // Arrange
        Item ExistingItem = CreateRandomItem();
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync(ExistingItem);

        ItemsController Controller = new ItemsController(RepositoryStub.Object);

        UpdateItemDTO UpdateingItem = new UpdateItemDTO()
        {
            Name = Guid.NewGuid().ToString(),
            Price = Rand.Next()
        };
        int Id = Rand.Next();

        // Act
        ActionResult Result = await Controller.UpdateItemAsync(Id, UpdateingItem);

        // Assert
        Result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteItemAsync_WithUnexistingItem_ReturnsNotFound()
    {
        // Arrange
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync((Item)null);

        ItemsController Controller = new ItemsController(RepositoryStub.Object);
        
        // Act
        ActionResult Result = await Controller.DeleteItemAsync(Rand.Next());

        // Assert
        Assert.IsType<NotFoundResult>(Result);
    }

    [Fact]
    public async Task DeleteItemAsync_WithExistingItem_ReturnsNoContent()
    {
        // Arrange
        Item ExistingItem = CreateRandomItem();
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync(ExistingItem);

        ItemsController Controller = new ItemsController(RepositoryStub.Object);
        
        // Act
        ActionResult Result = await Controller.DeleteItemAsync(Rand.Next());

        // Assert
        Assert.IsType<NoContentResult>(Result);
    }

    private Item CreateRandomItem()
    {
        return new Item()
        {
            Id = Rand.Next(),
            Name = Guid.NewGuid().ToString(),
            Price = Rand.Next(),
            CreatedTime = DateTimeOffset.UtcNow
        };
    }
}