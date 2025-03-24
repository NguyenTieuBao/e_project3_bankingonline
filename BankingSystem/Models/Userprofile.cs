using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Userprofile
{
    public int UserProfileId { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
