using Finux.Api.Data;
using Finux.Core.Enums;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Requests.Stripe;
using Finux.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finux.Api.Handlers;

public class OrderHandler(AppDbContext context, IStripeHandler stripeHandler) : IOrderHandler
{
    public async Task<Response<Order?>> CancelAsync(CancelOrderRequest request)
    {
        Order? order;
        try
        {
            order = await context
                .Orders
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (order is null)
            {
                return new Response<Order?>(null, 404, "Pedido não encontrado");
            }
        }
        catch 
        {
            return new Response<Order?>(null, 500, "Falha ao obter pedido");
        }

        switch (order.Status)
        {
            case EOrderStatus.Cancelled:
                return new Response<Order?>(order, 400, "Este pedido já foi cancelado");
            case EOrderStatus.WaitingPayment:
                break;
            case EOrderStatus.Paid:
                return new Response<Order?>(order, 400, "Este pedido já foi pago e não pode ser cancelado");
            case EOrderStatus.Refunded:
                return new Response<Order?>(order, 400, "Este pedido já foi reembolsado e não pode mais ser cancelado");
            default:
                return new Response<Order?>(order, 400, "Este pedido não pode ser cancelado");
        }
        
        order.Status = EOrderStatus.Cancelled;
        order.UpdatedAt = DateTime.Now;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(order, 500, "Não foi possível cancelar seu pedido");
        }
        
        return new Response<Order?>(order, 200, "Pedido cancelado");
    }

    public async Task<Response<Order?>> CreateAsync(CreateOrderRequest request)
    {
        Product? product;
        try
        {
            product = await context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId && x.IsActive == true);

            if (product is null)
            {
                return new Response<Order?>(null, 404, "Produto não encontrado");
            }

            context.Attach(product);
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível obter o produto");
        }
        
        Voucher? voucher = null;
        try
        {
            if (request.VoucherId is not null)
            {
                voucher = await context.Vouchers.FirstOrDefaultAsync(x => x.Id == request.VoucherId && x.IsActive == true);


                if (voucher is null)
                {
                    return new Response<Order?>(null, 400, "Voucher inválido ou não encontrado");
                }

                if (voucher.IsActive == false)
                {
                    return new Response<Order?>(null, 400, "Este voucher já foi utilizado");
                }

                voucher.IsActive = false;
                context.Vouchers.Update(voucher);
            }
        }
        catch 
        {
            return new Response<Order?>(null, 500, "Não foi possível obter o voucher");
        }

        var order = new Order
        {
            UserId = request.UserId,
            Product = product,
            ProductId = request.ProductId,
            Voucher = voucher,
            VoucherId = request.VoucherId
        };

        try
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }
        catch 
        {
            return new Response<Order?>(null, 500, "Não foi possível realizar o pedido");
        }
        
        return new Response<Order?>(order, 201, $"Pedido {order.Number} realizado com sucesso");
    }

    public async Task<Response<Order?>> PayAsync(PayOrderRequest request)
    {
        Order? order;
        try
        {
            order = await context.Orders.Include(x => x.Product).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Number == request.Number);

            if (order is null)
            {
                return new Response<Order?>(null, 404, "Pedido não encotrado");
            }
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível obter o pedido");
        }

        switch (order.Status)
        {
            case EOrderStatus.Cancelled:
                return new Response<Order?>(order, 400, "Este pedido já foi cancelado e não pode ser pago");
            
            case EOrderStatus.Paid:
                return new Response<Order?>(order, 400, "Este pedido já foi pago");
            
            case EOrderStatus.Refunded:
                return new Response<Order?>(order, 400, "Este pedido já foi reembolsado e não pode ser pago");
            
            case EOrderStatus.WaitingPayment:
                break;
            
            default:
                return new Response<Order?>(order, 400, "Não foi possível realizaar o pagamento deste pedido");
        }

        try
        {
            var getTransactionsRequest = new GetTransactionsByOrderNumberRequest
            {
                Number = order.Number
            };
            var result = await stripeHandler.GetTransactionsByOrderNumberAsync(getTransactionsRequest);

            if (!result.IsSuccess)
            {
                return new Response<Order?>(null, 500, "Não foi possível localizar o pagamento");
            }

            if (result.Data is null)
            {
                return new Response<Order?>(null, 500, "Não foi possível localizar o pagamento");
            }

            if (result.Data.Any(x => x.Refunded))
            {
                return new Response<Order?>(null, 500, "Este pedido já teve o pagamento estornado.");
            }
            
            if (!result.Data.Any(x => x.Paid))
            {
                return new Response<Order?>(null, 500, "Este pedido não foi pago");
            }
            
            request.ExternalReference = result.Data[0].Id;
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível dar baixa no seu pedido");
        }
        
        order.Status = EOrderStatus.Paid;
        order.UpdatedAt = DateTime.Now;
        order.ExternalReference = request.ExternalReference;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(order, 500, "Não foi possível realizar o pagamento deste pedido");
        }
        
        return new Response<Order?>(order, 200, $"Pedido {order.Number} pago com sucesso");
    }

    public async Task<Response<Order?>> RefundAsync(RefundOrderRequest request)
    {
        Order? order;
        try
        {
            order = await context.Orders.Include(x => x.Product).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

            if (order is null)
            {
                return new Response<Order?>(null, 404, "Pedido não encontrado");
            }
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível obter o pedido");
        }
        
        switch (order.Status)
        {
            case EOrderStatus.Cancelled:
                return new Response<Order?>(order, 400, "Este pedido já foi reembolsado e não pode ser cancelado");
            
            case EOrderStatus.Paid:
                break;
            
            case EOrderStatus.Refunded:
                return new Response<Order?>(order, 400, "Este pedido já foi reembolsado");
            
            case EOrderStatus.WaitingPayment:
                return new Response<Order?>(order, 400, "Este pedido já está aguardando pagamento e não pode ser reembolsado");
            
            default:
                return new Response<Order?>(order, 400, "Não foi possível realizaar o reembolso deste pedido");
        }
        
        order.Status = EOrderStatus.Refunded;
        order.UpdatedAt = DateTime.Now;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(order, 500, "Falha ao reembolsar pagamento");
        }
        
        return new Response<Order?>(order, 200, $"Pedido {order.Number} reembolsado com sucesso");
    }

    public async Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrderRequest request)
    {
        try
        {
            var query = context.Orders
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.CreatedAt);
            
            var order = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var countAsync = await query.CountAsync();
            
            return new PagedResponse<List<Order>?>(order, countAsync, request.PageSize, request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Order>?>(null, 500, "Não foi possível obter os pedidos");
        }
    }

    public async Task<Response<Order?>> GetByNumberAsync(GetOrderByNumberRequest request)
    {
        try
        {
            var orders = await context.Orders.Include(x => x.Voucher).Include(x => x.Product).FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Number == request.Number);

            return orders is null 
                ? new Response<Order?>(null, 404, "Não foi possível obter o pedido") 
                : new Response<Order?>(orders, 200, "Pedido obtido com sucesso");
        }
        catch
        {
            return new Response<Order?>(null, 500, "Falha ao obter o pedido");
        }
    }
}