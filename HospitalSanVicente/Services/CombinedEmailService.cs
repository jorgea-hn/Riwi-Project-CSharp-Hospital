using System.Threading.Tasks;

namespace HospitalSanVicente.Services
{
    public class CombinedEmailService : IEmailService
    {
        private readonly SendGridEmailService _sendGridEmailService;
        private readonly EmailService _consoleEmailService;

        // Recibe las dos implementaciones a través de inyección de dependencias
        public CombinedEmailService(SendGridEmailService sendGridEmailService, EmailService consoleEmailService)
        {
            _sendGridEmailService = sendGridEmailService;
            _consoleEmailService = consoleEmailService;
        }

        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            // Inicia ambas tareas en paralelo
            var sendGridTask = _sendGridEmailService.SendEmail(to, subject, body);
            var consoleTask = _consoleEmailService.SendEmail(to, subject, body);

            // Espera a que ambas terminen
            await Task.WhenAll(sendGridTask, consoleTask);

            // Devuelve el resultado del servicio principal (SendGrid)
            // Si el correo real se envió con éxito, consideramos la operación exitosa.
            return await sendGridTask;
        }
    }
}
