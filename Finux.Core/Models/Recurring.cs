using Finux.Core.Enums;

namespace Finux.Core.Models;

public class Recurring
{
    public long Id { get; set; }
    public ERecurringType? RecurringType { get; set; } = ERecurringType.MonthlyFixed;
    public EInstallmentsType InstallmentsType { get; set; } = EInstallmentsType.Monthly;
    public int? Installments { get; set; }
    
}