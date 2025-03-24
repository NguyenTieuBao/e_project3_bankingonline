using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Adminprofile
{
    public int AdminProfileId { get; set; }

    public int AdminId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Admin Admin { get; set; } = null!;
}
