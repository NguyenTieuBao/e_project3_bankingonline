using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual ICollection<Adminprofile> Adminprofiles { get; set; } = new List<Adminprofile>();

    public virtual Adminrole? Role { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
