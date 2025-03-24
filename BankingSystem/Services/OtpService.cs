using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank_api.Services
{
    public class OtpService
    {
        private readonly EBankingonlineContext _context;

        public OtpService(EBankingonlineContext context)
        {
            _context = context;
        }

        public string GenerateOTP(int number)
        {
            Random random= new Random();
            int min = (int)Math.Pow(10, number -1);
            int max = (int)Math.Pow(10, number) -1;
            
            string otpCode = random.Next(min, max).ToString();
            return otpCode;   
        }

        public async Task<IActionResult> SendOtpEmail(string email, string subject, string body)
        {
            try
            {
                string fromEmail = "obankingonline1014@gmail.com";
                string fromEmailPassword = "wfqmhrswhezlcloy";

                MailMessage mailMessage =  new MailMessage();
                mailMessage.From = new MailAddress(fromEmail);
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;  
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
                return new OkObjectResult("Email sent successfully!");
            } catch (Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> GenerateAndSendOtp(int number, string toEmail)
        {
            // Tạo OTP
            string otpCode = GenerateOTP(number);

            // Tạo nội dung email
            string subject = "Your OTP Code";
            string body = GetEmailBody(otpCode);

            // Gửi email chứa OTP
            var result = await SendOtpEmail(toEmail, subject, body);

            if (result is OkObjectResult)
            {
                var otp = new Otp
                {
                    Otpcode = otpCode,
                    CreateAt = DateTime.Now                    
                };
                _context.Otps.Add(otp);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<IActionResult> CheckOtp(string otp, int userId)
        {
            var code = await _context.Otps.FirstOrDefaultAsync(e => e.Otpcode == otp);
            if (code == null)
            {
                var message = new { message = "Mã OTP không tồn tại." };
                return new NotFoundObjectResult(message);
            }

            var currentTime = DateTime.Now;
            var timeDifference = currentTime - code.CreateAt;
            if (timeDifference.TotalMinutes > 5)
            {
                return new BadRequestObjectResult("Mã OTP đã quá hạn, mời bạn gửi lại mã");
            }

            return new OkObjectResult(code);
        }

        private string GetEmailBody(string otpCode)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Services/Form-Email/otp_email_template_otp.html");
            string body = File.ReadAllText(filePath);
            body = body.Replace("{{OTP_CODE}}", otpCode);
            return body;
        }
    }
}