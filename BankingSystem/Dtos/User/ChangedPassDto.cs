using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos.User
{
    public class ChangedPassDto
    {
        public string UserName { get; set; }
        public string PasswordNew { get; set; }
        public string PasswordCurrent { get; set; }

    }
}