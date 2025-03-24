using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos
{
    public class TransactionDto
    {
        public int? AdminId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string? TransactionType { get; set; }
        [Required]
        public decimal? Amount { get; set; }

        public DateTime? TransactionDate { get; set; } = DateTime.Now;
    }
}