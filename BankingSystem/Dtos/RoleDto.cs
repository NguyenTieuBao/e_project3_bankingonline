using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Dtos
{
    public class RoleDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}