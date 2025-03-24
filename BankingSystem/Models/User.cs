using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? FailedLoginAttempts { get; set; }

    public bool? AccountLocked { get; set; } = false;

    public string? PasswordResetToken { get; set; }

    public DateTime? PasswordResetExpiry { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }
    public int? RoleId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    public virtual ICollection<Otp> Otps { get; set; } = new List<Otp>();
    public virtual Adminrole? Role { get; set; }
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public virtual ICollection<Transfertransaction> Transfertransactions { get; set; } = new List<Transfertransaction>();
    public virtual UserToken? UserToken { get; set; }
    public virtual ICollection<Userprofile> Userprofiles { get; set; } = new List<Userprofile>();
}
