namespace Dima.Core.Requests.Orders;

public class GetVoucherByNumberRequest : BaseRequest
{
    public string Number { get; set; } = string.Empty;
}