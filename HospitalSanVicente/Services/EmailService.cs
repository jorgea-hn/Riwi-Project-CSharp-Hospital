using HospitalSanVicente.Interfaces;
using System;
using System.Threading.Tasks;

namespace HospitalSanVicente.Services
{
    public class EmailService : IEmailService
    {
        // Simulación de envío de correo electrónico a la consola
        public Task<bool> SendEmail(string to, string subject, string body)
        {
            Console.WriteLine("\n--- SIMULATING EMAIL ---");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("--- EMAIL SIMULATION END ---\n");

            // Devuelve una tarea completada con éxito. 
            // Esto simula una operación asíncrona que siempre tiene éxito.
            return Task.FromResult(true);
        }
    }
}
