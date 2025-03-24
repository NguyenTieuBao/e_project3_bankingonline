using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Dtos.User;
using BankingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controller.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EBankingonlineContext _context;
        public AccountController(EBankingonlineContext context)
        {
            _context = context;
        }

        [Authorize()]
        [HttpPost("createAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto accountDto)
        {
            try{
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingAccountNumber = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountDto.AccountNumber);
                if(existingAccountNumber != null)
                {
                    return BadRequest("Account Number is already in use. Try again");
                }

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == accountDto.Username);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var account = new Account
                {
                    AccountNumber = accountDto.AccountNumber,
                    UserId = user.UserId, // Lấy UserId từ bảng Users
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return Ok(account);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // [Authorize( Roles = "admin, manager, staff")]
        // [HttpGet]
        // public async Task<IActionResult> GetAllAccount()
        // {
        //     try
        //     {
        //         if(!ModelState.IsValid)
        //         {
        //             return BadRequest(ModelState);
        //         }

        //         var listAccount = await _context.Accounts.ToListAsync();
        //         return Ok(listAccount);
        //     } catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        [HttpGet("SearchAccount")]
        public async Task<IActionResult> SearchAccount(string accountNumber)
        {
            try {
                var search = await _context.Accounts.Include(a => a.User).ThenInclude(u => u.Userprofiles).FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
                if(search == null)
                {
                    return NotFound( new {
                        Message = "Account does not exist! try again"
                    });
                }

                var userProfile = search.User.Userprofiles.FirstOrDefault();
                if(userProfile == null)
                {
                    return NotFound(new {
                        Message = "User profile does not exist! try again"
                    });
                }
                var fullName = $"{userProfile.LastName} {userProfile.FirstName}";

                return Ok(new {
                    FullName = fullName
                });

            }catch( Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("GetAccount")]
        public async Task<IActionResult> GetAccountByUsername(string username)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                var accounts = await _context.Accounts
                    .Where(a => a.UserId == user.UserId)
                    .Select(a => new
                    {
                        a.AccountNumber,
                        a.Balance,
                        a.CreateAt
                    })
                    .ToListAsync();

                if (accounts == null || accounts.Count == 0)
                {
                    return NotFound("No accounts found for the user");
                }

                return Ok(accounts);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("accountcount")]
        public async Task<IActionResult> GetAccountCountByUserId(string username)
        {
            try
            {
                // Kiểm tra xem user có tồn tại trong database không
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Đếm số lượng tài khoản của user
                var accountCount = await _context.Accounts.CountAsync(a => a.UserId == user.UserId);

                return Ok(accountCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAccountByIdAccount(int AccountId)
        // {
        //     try
        //     {
        //         if(!ModelState.IsValid)
        //         {
        //             return BadRequest(ModelState);
        //         }
        //         var account = await _context.Accounts
        //             .Where(a => a.AccountId == AccountId)
        //             .ToListAsync();
        //         if(account == null)
        //         {
        //             return NotFound();
        //         }
        //         return Ok(account);
        //     } catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [HttpGet("TotalCurrentBalance")]
        public async Task<IActionResult> GetTotalCurrentBalance(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var total = await _context.Accounts
                            .Where(a => a.UserId == user.UserId)
                            .SumAsync(a => a.Balance);
            return Ok(total);
        }
    }
}