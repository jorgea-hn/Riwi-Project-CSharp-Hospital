using HospitalSanVicente.Interfaces;
using System.Threading.Tasks;

namespace HospitalSanVicente.Services
{
    /// <summary>
    /// An implementation of IEmailService that orchestrates multiple email services.
    /// It sends an email via a primary service (SendGrid) and simultaneously logs it via a secondary service (console).
    /// This pattern is useful for providing multiple outputs from a single action.
    /// </summary>
    public class CombinedEmailService : IEmailService
    {
        private readonly SendGridEmailService _sendGridEmailService;
        private readonly EmailService _consoleEmailService;

        public CombinedEmailService(SendGridEmailService sendGridEmailService, EmailService consoleEmailService)
        {
            _sendGridEmailService = sendGridEmailService;
            _consoleEmailService = consoleEmailService;
        }

        /// <summary>
        /// Asynchronously sends an email using both the SendGrid and console services concurrently.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The content of the email.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains the success status
        /// of the primary email service (SendGrid).
        /// </returns>
        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            // Initiate both email sending tasks to run in parallel.
            var sendGridTask = _sendGridEmailService.SendEmail(to, subject, body);
            var consoleTask = _consoleEmailService.SendEmail(to, subject, body);

            // Wait for both tasks to complete before proceeding.
            // This ensures that we don't return before the console log has finished, for example.
            await Task.WhenAll(sendGridTask, consoleTask);

            // The overall success of the operation is determined by the primary service (SendGrid).
            return await sendGridTask;
        }
    }
}
