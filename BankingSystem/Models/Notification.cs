using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set;}
        public string ToSender { get; set; }

        public virtual Account? Account { get; set; }
        public virtual User? User { get; set; }

        public virtual Transaction? Transaction{ get; set; }
        public virtual Transfertransaction? Transfertransaction{ get; set; }

        
    }
}