using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos
{
    public class TransferDto
    {
        // public int? AdminId { get; set; }

        public string? FromAccount { get; set; }

        public string? ToAccount { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? TransferDate { get; set; }

        public string? Status { get; set; }
        // public string Username { get; set; }
    }
}