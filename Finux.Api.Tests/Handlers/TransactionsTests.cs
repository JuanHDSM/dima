using Finux.Api.Data;
using Finux.Api.Handlers;
using Finux.Core.Enums;
using Finux.Core.Requests.Categories;
using Finux.Core.Requests.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Finux.Api.Tests.Handlers;

public class TransactionsTests
{
    private readonly  TransactionHandler _handler;
    private readonly  CategoryHandler _handlerCategory;

    public TransactionsTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        _handler = new TransactionHandler(context);
        _handlerCategory = new CategoryHandler(context);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var requestcategory = new CreateCategoryRequest
        {
            Title = "Test Category",
            Description = "Test Description",
            UserId = "user123"
        };

        // Act
        var resultCategory = await _handlerCategory.CreateAsync(requestcategory);

        // Assert intermediário
        Assert.NotNull(_handlerCategory);
        Assert.NotNull(resultCategory);
        Assert.True(resultCategory.IsSuccess);
        Assert.NotNull(resultCategory.Data);

        var request = new CreateTransactionRequest
        {
            UserId = "user123",
            CategoryId = resultCategory.Data!.Id,
            Amount = 100,
            PaidOrReceivedAt = DateTime.Now,
            Title = "Test Transaction",
            Type = ETransactionType.Deposit
        };

        var result = await _handler.CreateAsync(request);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        
        var request = new GetTransactionByIdRequest { Id = 999, UserId = "user123" };

        // Arrange 
        var result = await _handler.GetByIdAsync(request);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnSuccess_WhenCategoryDoesNotExist()
    {
        // Arrange intermediário
        var categoryRequest = new CreateCategoryRequest
        {
            Title = "Test Category",
            Description = "Test Description",
            UserId = "user123"
        };

        // Act intermediário
        var resultCategory = await _handlerCategory.CreateAsync(categoryRequest);

        // Assert intermediário
        Assert.NotNull(_handlerCategory);
        Assert.NotNull(resultCategory);
        
        // Arrange
        var transaction = new CreateTransactionRequest
        {
            UserId = "user123",
            CategoryId = resultCategory.Data!.Id,
            Amount = 100,
            PaidOrReceivedAt = DateTime.Now,
            Title = "Test Transaction",
            Type = ETransactionType.Deposit
        };
        
        //Act
        var result = await _handler.CreateAsync(transaction);
        
        // Asserts
        Assert.True(result.IsSuccess);
    }
}