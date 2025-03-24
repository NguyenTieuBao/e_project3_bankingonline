using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Dtos;
using BankingSystem.Dtos.Admin;
using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controller.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly EBankingonlineContext _context;
        private readonly TokenService _tokenService;
        public AdminController(EBankingonlineContext context,  TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> Register([FromBody] RegisterAdminDto registerAdminDto)
        {
            try 
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEmail = await _context.Adminprofiles.FirstOrDefaultAsync(e => e.Email == registerAdminDto.Email);
                if (existingEmail != null)
                {
                    return BadRequest("Email is already in use. Please re-enter another email");
                }

                var existingUsername = await _context.Admins.FirstOrDefaultAsync(u => u.Username == registerAdminDto.Username);
                if (existingUsername != null)
                {
                    return BadRequest("Username is already in use. Please re-enter another Username");
                }

                var existingPhonenumber = await _context.Adminprofiles.FirstOrDefaultAsync(u => u.PhoneNumber == registerAdminDto.PhoneNumber);
                if (existingPhonenumber != null)
                {
                    return BadRequest("PhoneNumber is already in use. Please re-enter another PhoneNumber");
                }

                var role = await _context.Adminroles.FirstOrDefaultAsync(r => r.RoleName == registerAdminDto.Role);
                if (role == null)
                {
                    return BadRequest("Invalid role specified.");
                }

                var admin = new BankingSystem.Models.Admin
                {
                    Username = registerAdminDto.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerAdminDto.Password),
                    RoleId = role.RoleId
                };
                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();

                var adminProfile = new BankingSystem.Models.Adminprofile
                {
                    AdminId = admin.AdminId, 
                    FirstName = registerAdminDto.FirstName,
                    LastName = registerAdminDto.LastName,
                    Email = registerAdminDto.Email,
                    PhoneNumber = registerAdminDto.PhoneNumber
                };
                _context.Adminprofiles.Add(adminProfile);
                await _context.SaveChangesAsync();
                return Ok(
                    new {
                        Message = "Register Admin successfully!"
                    }
                );

            } catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("loginAdmin")]
        public async Task<IActionResult> Login([FromBody]LoginAdminDto loginAdminDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Username == loginAdminDto.Username);
                if (admin == null)
                {
                    return BadRequest("Username or Password not valid!, try again");
                }

                var adminprofile = await _context.Adminprofiles.FirstOrDefaultAsync(x => x.AdminId == admin.AdminId);
                var adminRole = await _context.Adminroles.FirstOrDefaultAsync(x => x.RoleId == admin.RoleId);


                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginAdminDto.Password, admin.PasswordHash);

                if (!isPasswordValid)
                {
                    return BadRequest("Username or Password not valid!, try again");
                }

                var token = _tokenService.CreateTokenAdmin(admin, adminprofile, adminRole);
                return Ok(new
                {
                    Token = token,
                    Message = "Login Admin Successfully!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}