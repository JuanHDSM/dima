using Finux.Core.Enums;

namespace Finux.Core.Models;

public class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public DateTime? PaidOrReceivedAt { get; set; }
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    public decimal Amount { get; set; }
    public ERecurringType? RecurringType { get; set; } = ERecurringType.MonthlyFixed;
    
    public EInstallmentsType InstallmentsType { get; set; } = EInstallmentsType.Monthly;
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public int? Installments { get; set; }
}
