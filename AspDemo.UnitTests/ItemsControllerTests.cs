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
    private readonly Random rand = new Random();
    private readonly Mock<IItemsRepository> RepositoryStub = new Mock<IItemsRepository>();

    [Fact]
    public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
    {
        // Arrange
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync((Item)null);

        ItemsController controller = new ItemsController(RepositoryStub.Object);
        
        // Act
        ActionResult<ItemDTO> result = await controller.GetItemAsync(rand.Next());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
    {
        // Arrange 
        Item expectedItem = CreateRandomItem();
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync(expectedItem);

        ItemsController controller = new ItemsController(RepositoryStub.Object);

        // Act 
        ActionResult<ItemDTO> result = await controller.GetItemAsync(rand.Next());

        // Assert
        // Problems here need refactoring
        var okresult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<ItemDTO>(okresult.Value);
        //result.Value.Should().BeEquivalentTo(expectedItem);
    }

    private Item CreateRandomItem()
    {
        return new Item()
        {
            Id = rand.Next(),
            Name = Guid.NewGuid().ToString(),
            Price = rand.Next(),
            CreatedTime = DateTimeOffset.UtcNow
        };
    }
}