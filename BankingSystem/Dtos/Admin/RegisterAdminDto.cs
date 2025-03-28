using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos.Admin
{
    public class RegisterAdminDto
    {
        [Required]
        public string Username { get; set; } 
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; } 
        [Required]
        public string LastName { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        public string PhoneNumber { get; set; } 
        public string Role { get; set; }
    }
}