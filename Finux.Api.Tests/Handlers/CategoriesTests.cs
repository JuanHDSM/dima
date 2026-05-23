using Finux.Api.Data;
using Finux.Api.Handlers;
using Finux.Core.Requests.Categories;
using Microsoft.EntityFrameworkCore;

namespace Finux.Api.Tests.Handlers;

public class CategoriesTests
{
    private readonly CategoryHandler _handler;

    public CategoriesTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        _handler = new CategoryHandler(context);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateCategoryRequest
        {
            Title = "Test Category",
            Description = "Test Description",
            UserId = "user123"
        };

        // Act
        var result = await _handler.CreateAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(request.Title, result.Data.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var request = new GetCategoryByIdRequest { Id = 999, UserId = "user123" };

        // Act
        var result = await _handler.GetByIdAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnSuccess_WhenCategoryExists()
    {
        // Arrange
        var createRequest = new CreateCategoryRequest { Title = "To Delete", UserId = "user123" };
        var created = await _handler.CreateAsync(createRequest);
        var deleteRequest = new DeleteCategoryRequest { Id = created.Data!.Id, UserId = "user123" };

        // Act
        var result = await _handler.DeleteAsync(deleteRequest);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Categoria excluir com sucesso", result.Message);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnPagedList()
    {
        // Arrange
        await _handler.CreateAsync(new CreateCategoryRequest { Title = "Cat 1", UserId = "user123" });
        await _handler.CreateAsync(new CreateCategoryRequest { Title = "Cat 2", UserId = "user123" });
        var request = new GetAllCategoriesRequest { UserId = "user123", PageNumber = 1, PageSize = 10 };

        // Act
        var result = await _handler.GetAllAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Data?.Count);
    }
}
