using bank_api.Services;
using BankingSystem.Data;
using BankingSystem.Dtos.User;
using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly EBankingonlineContext _context;
        private readonly TokenService _tokenService;
        private readonly OtpService _otpService;

        public RequestController(EBankingonlineContext context, TokenService tokenService, OtpService otpService)
        {
            _context = context;
            _tokenService = tokenService;
            _otpService = otpService;
        }

        // GET: api/requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
        {
            var requests = await _context.Requests
                .Include(r => r.User)
                .Select(r => new RequestDto
                {
                    Username = r.User.Username,
                    RequestType = r.RequestType,
                    RequestDate = r.RequestDate,
                    Status = r.Status
                })
                .ToListAsync();

            return Ok(requests);
        }

        // GET: api/requests/{username}
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequestsByUsername(string username)
        {
            var userRequests = await _context.Requests
                .Include(r => r.User)
                .Where(r => r.User.Username.ToLower() == username.ToLower())
                .Select(r => new RequestDto
                {
                    Username = r.User.Username,
                    RequestType = r.RequestType,
                    RequestDate = r.RequestDate,
                    Status = r.Status
                })
                .ToListAsync();

            if (!userRequests.Any())
            {
                return NotFound(new { Message = "No requests found for this user." });
            }

            return Ok(userRequests);
        }

        // POST: api/requests
        [HttpPost]
        public async Task<ActionResult<RequestDto>> PostRequest([FromBody] RequestDto requestDto)
        {
            if (requestDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == requestDto.Username);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            var request = new Request
            {
                UserId = user.UserId,
                RequestType = requestDto.RequestType,
                RequestDate = DateTime.Now,
                Status = requestDto.Status
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            requestDto.RequestDate = request.RequestDate;

            return CreatedAtAction(nameof(GetRequestsByUsername), new { username = requestDto.Username }, requestDto);
        }
    }
}
