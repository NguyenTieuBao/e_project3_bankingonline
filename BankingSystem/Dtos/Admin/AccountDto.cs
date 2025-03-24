using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos.Admin
{
    public class AccountDto
    {
        [Required]
        public int? UserId { get; set; }
        [Required]
        public string AccountNumber { get; set; } = null!;
        
    }
}