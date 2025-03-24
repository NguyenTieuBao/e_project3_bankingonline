using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Adminrole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();
}
