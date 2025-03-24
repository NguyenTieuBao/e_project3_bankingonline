using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class UserToken
{
    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual User User { get; set; } = null!;
}
