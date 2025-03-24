using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Tokenblacklist
{
    public int TokenId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreateAt { get; set; }
}
