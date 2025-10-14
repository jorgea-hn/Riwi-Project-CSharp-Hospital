using HospitalSanVicente.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace HospitalSanVicente.Services
{
    /// <summary>
    /// Service responsible for sending emails using the SendGrid API.
    /// </summary>
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Asynchronously sends an email using SendGrid.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The HTML or plain text content of the email.</param>
        /// <returns>A Task representing the asynchronous operation, returning true if the email was sent successfully.</returns>
        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            try
            {
                // Retrieve API Key and sender details from configuration.
                // This keeps sensitive information out of the source code.
                var apiKey = _configuration["SendGrid:ApiKey"];
                var fromEmail = _configuration["SendGrid:FromEmail"];
                var fromName = _configuration["SendGrid:FromName"];

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, fromName);
                var toAddress = new EmailAddress(to);
                var msg = MailHelper.CreateSingleEmail(from, toAddress, subject, body, body);

                // Await the response from the SendGrid API.
                var response = await client.SendEmailAsync(msg);

                // The email is considered sent if the API returns a success status code (2xx).
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                // In a real-world scenario, you would use a formal logging framework.
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
