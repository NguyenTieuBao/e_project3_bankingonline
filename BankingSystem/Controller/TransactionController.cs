using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Dtos;
using BankingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController: ControllerBase
    {
        private readonly EBankingonlineContext _context;
        public TransactionController(EBankingonlineContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("TransactionPost")]
        public async Task<IActionResult> TransactionPost([FromBody] TransactionDto transactionDto)
        {
            var adminId = _context.Admins.FirstOrDefault( x => x.AdminId == transactionDto.AdminId );
            if (adminId == null)
            {
                return NotFound("Admin khong có");
            }

            var accountNumber = _context.Accounts.Include(a => a.User).FirstOrDefault(x => x.AccountNumber == transactionDto.AccountNumber);
            if (accountNumber == null)
            {
                return NotFound(" acountnumber khong co");
            }

            var trans = new Transaction
            {
                AdminId = transactionDto.AdminId,
                UserId = accountNumber.User.UserId,
                AccountId = accountNumber.AccountId,
                Amount = transactionDto.Amount,
                FullName = transactionDto.FullName,
                TransactionType = transactionDto.TransactionType
            };

            accountNumber.Balance += transactionDto.Amount; 
            _context.Transactions.Add(trans);
            await _context.SaveChangesAsync();

            return Ok(new {
                Message = "Transactions successfully!",
                TransactionId = trans.TransactionId,
                UpdatedBalance = accountNumber.Balance                
            });
        } 

        [Authorize()]
        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transferDto)
        {
            var fromAccount = _context.Accounts.FirstOrDefault(x => x.AccountNumber == transferDto.FromAccount);
            if (fromAccount == null)
            {
                return NotFound("Khong co From Account");
            }

            var toAccount = _context.Accounts.FirstOrDefault(x => x.AccountNumber == transferDto.ToAccount);
            if (toAccount == null)
            {
                return NotFound("Khong co To Account");
            }

            var transfer = new Transfertransaction
            {
                FromAccountId = fromAccount.AccountId,
                ToAccountId = toAccount.AccountId,
                Amount = transferDto.Amount,
                Status = transferDto.Status,
                TransferDate = DateTime.Now,
                
            };
            if(fromAccount.Balance < transferDto.Amount)
            {
                return BadRequest( new {
                    Message = "Insufficient account balance, try again "
                });
            }

            fromAccount.Balance -= transferDto.Amount;
            toAccount.Balance += transferDto.Amount;

            _context.Transfertransactions.Add(transfer);
            await _context.SaveChangesAsync();
            return Ok( new {
                Message = "Transfer Successfully"
            });
        } 

        // [Authorize(Roles = "user")]
        [HttpGet("transactionhistory")]
        public async Task<IActionResult> GetHistoryByUser(string username)
        {
            var transactions = await _context.Transactions
                .Include(t => t.Account)
                .Where(t => t.Account.User.Username == username) // Lọc theo username của người dùng
                .OrderByDescending(t => t.TransactionId)
                .ToListAsync();

            var result = transactions.Select(t => new {
                t.FullName,
                t.TransactionId,
                t.Amount,
                t.TransactionDate,
                AccountNumber = t.Account.AccountNumber,
                t.TransactionType
            });

            return Ok(result);
        }


        // [Authorize(Roles = "user")]
        // [HttpGet("transferhistory")]
        // public async Task<IActionResult> GetAllHistoryTransfer(string username)
        // {
        //     try
        //     {
        //         var user = await _context.Users
        //             .Include(u => u.Transfertransactions)
        //                 .ThenInclude(t => t.FromAccount)
        //                     .ThenInclude(a => a.User)
        //             .Include(u => u.Transfertransactions)
        //                 .ThenInclude(t => t.ToAccount)
        //                     .ThenInclude(a => a.User)
        //             .FirstOrDefaultAsync(u => u.Username == username);

        //         if (user == null)
        //         {
        //             return NotFound(new { Message = "Không tìm thấy thông tin người dùng." });
        //         }

        //         var transactionHistory = user.Transfertransactions
        //             .OrderByDescending(t => t.TransferId)
        //             .Select(t => new {
        //                 FullNameFrom = $"{t.FromAccount.User.Userprofiles.FirstOrDefault()?.FirstName} {t.FromAccount.User.Userprofiles.FirstOrDefault()?.LastName}",
        //                 FullNameTo = $"{t.ToAccount.User.Userprofiles.FirstOrDefault()?.FirstName} {t.ToAccount.User.Userprofiles.FirstOrDefault()?.LastName}",
        //                 Amount = t.Amount,
        //                 TransactionDate = t.TransferDate,
        //                 FromAccountNumber = t.FromAccount.AccountNumber,
        //                 ToAccountNumber = t.ToAccount.AccountNumber,
        //                 TransactionType = t.Status // Giả sử 'Status' ở đây biểu thị loại giao dịch
        //             })
        //             .ToList();

        //         return Ok(transactionHistory);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { Message = $"Lỗi khi lấy dữ liệu lịch sử chuyển khoản: {ex.Message}" });
        //     }
        // }

        [HttpGet("transferhistory")]
        public async Task<IActionResult> GetTransferHistoryByFromAccount(string fromAccountNumber)
        {
            try
            {
                var transfers = await _context.Transfertransactions
                    .Include(t => t.FromAccount)
                        .ThenInclude(a => a.User)
                            .ThenInclude(u => u.Userprofiles)
                    .Include(t => t.ToAccount)
                        .ThenInclude(a => a.User)
                            .ThenInclude(u => u.Userprofiles)
                    .Where(t => t.FromAccount.AccountNumber == fromAccountNumber)
                    .OrderByDescending(t => t.TransferId)
                    .ToListAsync();

                if (transfers == null || transfers.Count == 0)
                {
                    return NotFound(new { Message = "Không tìm thấy dữ liệu lịch sử chuyển khoản cho tài khoản này." });
                }

                var transactionHistory = transfers.Select(t => new {
                    FullNameFrom = $"{t.FromAccount.User.Userprofiles.FirstOrDefault()?.FirstName} {t.FromAccount.User.Userprofiles.FirstOrDefault()?.LastName}",
                    FullNameTo = $"{t.ToAccount.User.Userprofiles.FirstOrDefault()?.FirstName} {t.ToAccount.User.Userprofiles.FirstOrDefault()?.LastName}",
                    Amount = t.Amount,
                    TransactionDate = t.TransferDate,
                    FromAccountNumber = t.FromAccount.AccountNumber,
                    ToAccountNumber = t.ToAccount.AccountNumber,
                    TransactionType = t.Status // Giả sử 'Status' ở đây biểu thị loại giao dịch
                }).ToList();

                return Ok(transactionHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Lỗi khi lấy dữ liệu lịch sử chuyển khoản: {ex.Message}" });
            }
    }
    }
}