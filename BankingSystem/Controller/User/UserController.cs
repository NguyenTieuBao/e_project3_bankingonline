using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using BankingSystem.Services;
using BankingSystem.Dtos.User;
using System.Diagnostics;
using bank_api.Services;


namespace BankingSystem.Controller.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EBankingonlineContext _context;
        private readonly TokenService _tokenService;
        private readonly OtpService _otpService;
        public UserController(EBankingonlineContext context,  TokenService tokenService, OtpService otpService)
        {
            _context = context;
            _tokenService = tokenService;
            _otpService = otpService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try 
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEmail = await _context.Userprofiles.FirstOrDefaultAsync(e => e.Email == registerDto.Email);
                if (existingEmail != null)
                {
                    return BadRequest(new {Message ="Email is already in use. Please re-enter another email"});
                }

                var existingUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == registerDto.Username);
                if (existingUsername != null)
                {
                    return BadRequest(new {Message ="Username is already in use. Please re-enter another Username"});
                }

                var existingPhonenumber = await _context.Userprofiles.FirstOrDefaultAsync(u => u.PhoneNumber == registerDto.PhoneNumber);
                if (existingPhonenumber != null)
                {
                    return BadRequest(new {Message ="PhoneNumber is already in use. Please re-enter another PhoneNumber"});
                }

                var role = await _context.Adminroles.FirstOrDefaultAsync(r => r.RoleName == registerDto.Role);
                if (role == null)
                {
                    return BadRequest(new {Message ="Invalid role specified."});
                }

                var user = new BankingSystem.Models.User
                {
                    Username = registerDto.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    RoleId = role.RoleId
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userProfile = new BankingSystem.Models.Userprofile
                {
                    UserId = user.UserId, 
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    
                };

                
                _context.Userprofiles.Add(userProfile);
                await _context.SaveChangesAsync();

                return Ok(new {Message = "Register is successfully!"});

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                Console.WriteLine($"Received login request: Username={loginDto.Username}, Password={loginDto.Password}");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == loginDto.Username);

                if (user == null)
                {
                    return NotFound(new { Message ="Username or Password not valid!, try again"});
                }
                
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                {
                    user.FailedLoginAttempts += 1;

                    if (user.FailedLoginAttempts >= 5)
                    {
                        user.AccountLocked = true;
                    }

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    if (user.AccountLocked == true)
                    {
                        return BadRequest(new { Message = "Your account has been locked due to multiple failed login attempts. Please contact support."});
                    }

                    return BadRequest(new { Message = $"Password not valid, you have {5 - user.FailedLoginAttempts} attempts left, try again"});
                }

                if (user.AccountLocked == true)
                {
                    return BadRequest(new { Message = "This account is locked. Please contact support."});
                }

                user.FailedLoginAttempts = 0;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                var userprofile = await _context.Userprofiles.FindAsync(user.UserId);
                var role = await _context.Adminroles.FirstOrDefaultAsync(x => x.RoleId == user.RoleId);
                if (role == null)
                {
                    return NotFound(new { Message = "User role not found!" });
                }

                return Ok(new 
                {
                    Token = _tokenService.CreateTokenUser(user, userprofile, role),
                    Message = "Login Successfully!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPost("ChangedPassword")]
        public async Task<IActionResult> ChangedPassword([FromBody] ChangedPassDto changedPassDto)
        {
            var user = await _context.Users.Include(t => t.Userprofiles).FirstOrDefaultAsync(x => x.Username == changedPassDto.UserName);
            if (user == null)
            {
                return NotFound( new {
                    Message = "Username not found"
                });
            }

            if (!VerifyPassword(changedPassDto.PasswordCurrent, user.PasswordHash))
            {
                return BadRequest(new {
                    Message = "Current password is incorrect"
                });
            }

            user.PasswordHash = HashPassword(changedPassDto.PasswordNew);

            // _otpService.GenerateAndSendOtp(6, user.Email);
            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new {
                Message = "Password changed successfully"
            });
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            // Implement your password verification logic here
            // This is an example using BCrypt
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }

        private string HashPassword(string password)
        {
            // Implement your password hashing logic here
            // This is an example using BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return NotFound( new {
                    Message = "Username not found"
                });
            }

            var profile = await _context.Userprofiles.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (profile == null)
            {
                return NotFound(new { Message = "Profile not found" });
            }

            return Ok(new
            {
                user.Username,
                profile.FirstName,
                profile.LastName,
                profile.Email,
                profile.PhoneNumber
            });
        }

    }

    
}


// user.Token = CreateJwt(user);
//             var newAccessToken = user.Token;
//             var newRefreshToken = CreateRefreshToken();
//             user.RefreshToken = newRefreshToken;
//             user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
//             await _authContext.SaveChangesAsync();

//             return Ok(new TokenApiDto()
//             {
//                 AccessToken = newAccessToken,
//                 RefreshToken = newRefreshToken
//             });