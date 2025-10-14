using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;

namespace HospitalSanVicente.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailLogRepository _emailLogRepository;

        public EmailService(IEmailLogRepository emailLogRepository)
        {
            _emailLogRepository = emailLogRepository;
        }

        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Enviando email a: {to}, Asunto: {subject}, Cuerpo: {body}");
            _emailLogRepository.Create(new EmailLog
            {
                To = to,
                Subject = subject,
                Body = body,
                SentDate = DateTime.Now
            });
        }
    }
}
