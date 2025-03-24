using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos.User
{
    public class RequestDto
    {
        public string? Username { get; set; }

        public string? RequestType { get; set; }

        public DateTime? RequestDate { get; set; }

        public string? Status { get; set; }
    }
}