using HospitalSanVicente.Interfaces;
using System;
using System.Threading.Tasks;

namespace HospitalSanVicente.Services
{
    /// <summary>
    /// A mock email service that simulates sending emails by writing their content to the console.
    /// This is useful for development and testing environments where real emails are not needed.
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Simulates sending an email by printing its details to the console.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The content of the email.</param>
        /// <returns>A completed Task containing `true`, simulating a successful asynchronous send.</returns>
        public Task<bool> SendEmail(string to, string subject, string body)
        {
            Console.WriteLine("\n--- SIMULATING EMAIL ---");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("--- EMAIL SIMULATION END ---\n");

            // Return a successfully completed task to match the async interface signature.
            // This mimics an asynchronous operation that completes instantly.
            return Task.FromResult(true);
        }
    }
}
