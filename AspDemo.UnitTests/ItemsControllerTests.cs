using System;
using System.Threading.Tasks;
using AspDemo.Api.Controllers;
using AspDemo.Api.DTOs;
using AspDemo.Api.Models;
using AspDemo.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AspDemo.UnitTests;

public class ItemsControllerTests
{
    [Fact]
    public async Task GetItemAsync_WithUexistingItem_ReturnsNotFound()
    {
        // Arrange
        Random rand = new Random();
        Mock<IItemsRepository> RepositoryStub = new Mock<IItemsRepository>();
        RepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync((Item)null);

        ItemsController controller = new ItemsController(RepositoryStub.Object);
        
        // Act
        ActionResult<ItemDTO> result = await controller.GetItemAsync(rand.Next());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

    }
}