using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }
    public int? AdminId { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public int? UserId { get; set; }
    public int? AccountId { get; set; }
    public string? TransactionType { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public virtual Account? Account { get; set; }
    public virtual Admin? Admin { get; set; }
    public virtual User? User { get; set; }
}
