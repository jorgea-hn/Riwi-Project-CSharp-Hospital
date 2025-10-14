
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using HospitalSanVicente.Interfaces;

namespace HospitalSanVicente.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var senderEmail = _configuration["SendGrid:SenderEmail"];
            var senderName = _configuration["SendGrid:SenderName"];

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(senderEmail) || senderEmail.StartsWith("PON_AQUI"))
            {
                // No envía el correo si la configuración es inválida o no se ha actualizado.
                // Podrías añadir un log aquí para registrar que se intentó enviar un correo sin configuración.
                return false; 
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, senderName);
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, body, $"<p>{body}</p>");
            
            try
            {
                var response = await client.SendEmailAsync(msg);
                // Devuelve true si el correo fue aceptado para envío (ej. StatusCode 202)
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Si ocurre un error en la comunicación con SendGrid, devuelve false.
                return false;
            }
        }
    }
}
