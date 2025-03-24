using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos.User
{
    public class AccountDto
    {
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        
    }
}