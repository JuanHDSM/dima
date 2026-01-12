using System.ComponentModel.DataAnnotations;

namespace Finux.Core.Requests.Stocks
{
    public class CreateStockRequest : BaseRequest
    {
        [Required(ErrorMessage = "Símbolo inválido")]
        public string Symbol { get; set; } = string.Empty;
    }
}