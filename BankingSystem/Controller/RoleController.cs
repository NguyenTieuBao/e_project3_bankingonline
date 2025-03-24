using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Dtos;
using BankingSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controller
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly EBankingonlineContext _context;
        public RoleController(EBankingonlineContext context)
        {
            _context = context;
        }

        [HttpPost("Role")]
        public async Task<IActionResult> Create([FromBody] RoleDto roleDto)
        {
            var role = await _context.Adminroles.FirstOrDefaultAsync(x => x.RoleName == roleDto.RoleName);
            if (role != null)
            {
                return BadRequest("Role is use, try again");
            }
            var save = new Adminrole
            {
                RoleName = roleDto.RoleName
            };
            _context.Adminroles.Add(save);
            await _context.SaveChangesAsync();
            return Ok(save);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var role = await _context.Adminroles.ToListAsync();
            return Ok(role);
        }

        
    }
}