using HospitalSanVicente.Interfaces;
using System;

namespace HospitalSanVicente.Services
{
    // This is a mock implementation for demonstration purposes.
    public class EmailService : IEmailService
    {
        public bool SendEmail(string recipient, string subject, string body)
        {
            Console.WriteLine("\n--- Simulating Email Dispatch ---");
            Console.WriteLine($"To: {recipient}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("--- Email Sent Successfully ---\n");
            
            // In a real application, this would involve connecting to an SMTP server
            // and handling potential exceptions. For this simulation, we'll always assume success.
            return true; 
        }
    }
}
