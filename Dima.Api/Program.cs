
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(
    options =>
        {
            options.UseMySql(cnnStr, new MySqlServerVersion(new Version(8, 0, 2)));
        }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => 
{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.MapPost(
    "/v1/categories", 
    async (CreateCategoryRequest request, ICategoryHandler handler) 
        => await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();

app.MapPut("v1/categories/{id}",
    async (UpdateCategoryRequest request, ICategoryHandler handler, long id) => 
    {
        request.Id = id;
        return await handler.UpdateAsync(request);
    })
    .WithName("Categories: Update")
    .WithSummary("Atualizar uma categoria")
    .Produces<Response<Category>>();

app.MapDelete("v1/categories/{id}",
    async (ICategoryHandler handler, long id) =>
    {
        var request = new DeleteCategoryRequest
        {
            Id = id,
            UserId = ""
        };
        return await handler.DeleteAsync(request);
    })
    .WithName("Categories: Delete")
    .WithSummary("Excluir uma categoria")
    .Produces<Response<Category>>();


app.MapGet("v1/categories",
    async (ICategoryHandler handler) =>
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = "holy@holy.io"
        };
        return await handler.GetAllAsync(request);
    })
    .WithName("Categories: Get All Categoies")
    .WithSummary("Retorna todas as categorias de um usuário")
    .Produces<PagedResponse<List<Category>>>();

app.MapGet("v1/categories/{id}",
    async (ICategoryHandler handler, long id) =>
    {
        var request = new GetCategoryByIdRequest
        {
            Id = id,
            UserId = "holy@holy.io"
        };
        return await handler.GetByIdAsync(request);
    })
    .WithName("Categories: Get By Id Category")
    .WithSummary("Retorna uma categoria de um usuário")
    .Produces<Response<Category>>();

app.UseSwagger();
app.UseSwaggerUI(); 

app.Run();