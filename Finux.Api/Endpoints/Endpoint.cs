using Finux.Api.Common.Api;
using Finux.Api.Models;
using Finux.Api.Endpoints.Categories;
using Finux.Api.Endpoints.Identity;
using Finux.Api.Endpoints.Orders;
using Finux.Api.Endpoints.Reports;
using Finux.Api.Endpoints.Stocks;
using Finux.Api.Endpoints.Stripe;
using Finux.Api.Endpoints.Transactions;
using stocks.Endpoints;

namespace Finux.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoint = app.MapGroup("api");

            endpoint.MapGroup("")
                .WithTags("Health Check")
                .MapGet("/health-check", () => new { message = "OK" });

            endpoint.MapGroup("v1/categories")
                .WithTags("Categories")
                .RequireAuthorization()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoint.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .RequireAuthorization()
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<GetTransactionByPeriodEndpoint>();

            endpoint.MapGroup("v1/expenses")
                .WithTags("Expenses")
                .MapEndpoint<GetExpensesByPeriodEndpoint>();
            
            endpoint.MapGroup("v1/incomes")
                .WithTags("Expenses")
                .MapEndpoint<GetIncomesByPeriodEndpoint>();

            endpoint.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            endpoint.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapEndpoint<LogoutEndpoint>()
                .MapEndpoint<GetRolesEndpoint>();

            endpoint.MapGroup("v1/reports")
                .WithTags("Reports")
                .RequireAuthorization()
                .MapEndpoint<GetIncomesAndExpensesEndpoint>()
                .MapEndpoint<GetIncomesByCategoryEndpoint>()
                .MapEndpoint<GetExpensesByCategoryEndpoint>()
                .MapEndpoint<GetFinancialSummaryEndpoint>();

            endpoint.MapGroup("v1/stocks")
                .WithTags("Stocks")
                .RequireAuthorization()
                .MapEndpoint<GetAllStocksExternalEndpoint>()
                .MapEndpoint<GetStocksBySymbolExternalEndpoint>()
                .MapEndpoint<CreateStockEndpoint>()
                .MapEndpoint<GetAllStocksEndpoint>()
                .MapEndpoint<GetAssetsInWalletEndpoint>();

            endpoint.MapGroup("v1/orders")
                .WithTags("Orders")
                .RequireAuthorization()
                .MapEndpoint<CancelOrderEndpoint>()
                .MapEndpoint<CreateOrderEndpoint>()
                .MapEndpoint<GetAllOrdersEndpoint>()
                .MapEndpoint<GetOrderByNumberEndpoint>()
                .MapEndpoint<PayOrderEndpoint>()
                .MapEndpoint<RefundOrderEndpoint>();

            endpoint.MapGroup("v1/products")
                .WithTags("Products")
                .RequireAuthorization()
                .MapEndpoint<GetAllProductsEndpoint>()
                .MapEndpoint<GetProductBySlugEndpoint>();

            endpoint.MapGroup("v1/vouchers")
                .WithTags("Vouchers")
                .RequireAuthorization()
                .MapEndpoint<GetVoucherByNumberEndpoint>();

            endpoint.MapGroup("v1/payments/stripe")
                .WithTags("Payments - Stripe")
                .RequireAuthorization()
                .MapEndpoint<CreateSessionEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}