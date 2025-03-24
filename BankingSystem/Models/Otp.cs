using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Otp
{
    public int Otpid { get; set; }

    public int? UserId { get; set; }

    public string Otpcode { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual User? User { get; set; }
}
