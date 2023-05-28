using CleanArchitecture.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace CleanArchitecture.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendResetPasswordMailAsync(string to, string userId, string token)
        {
            return await SendAsync(to, "Reset Password", $"{userId}/{token}");
        }

        public async Task<bool> SendForgotPasswordMailAsync(string to, string userId, string token)
        {
            return await SendAsync(to, "Forgot Password", $"{userId}/{token}");
        }

        public async Task<bool> SendAsync(string to, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("host", 25)
                {
                    EnableSsl = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("username", "password")
                };
                var message = GetMailMessage(to, subject, body);
                await smtpClient.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while mail sending.");
                return false;
                throw;
            }
        }

        private static MailMessage GetMailMessage(string to, string subject, string body)
        {
            MailMessage message = new()
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true,
                From = new MailAddress("mailaddress@mail.com")
            };
            message.To.Add(new MailAddress(to));

            return message;
        }
    }
}
