using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public int? UserId { get; set; }

    public string? RequestType { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
