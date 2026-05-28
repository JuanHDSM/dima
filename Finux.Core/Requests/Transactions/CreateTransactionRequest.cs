using System.ComponentModel.DataAnnotations;
using Finux.Core.Enums;

namespace Finux.Core.Requests.Transactions
{
    public class CreateTransactionRequest : BaseRequest
    {
        [Required(ErrorMessage = "Título inválido")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Tipo inválido")]
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
        [Required(ErrorMessage = "Valor inválido")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Categoria inválida")]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Data inválida")]
        public DateTime? PaidOrReceivedAt { get; set; }

        public EInstallmentsType InstallmentsType { get; set; } = EInstallmentsType.Monthly;
        public ERecurringType? RecurringType { get; set; } = ERecurringType.MonthlyFixed;
        public int Installments { get; set; } = 1;
    }
}