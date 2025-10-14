using System.Threading.Tasks;

namespace HospitalSanVicente.Interfaces
{
    /// <summary>
    /// Defines the contract for an email sending service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Asynchronously sends an email.
        /// </summary>
        /// <param name="recipient">The email address of the recipient.</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The content of the email.</param>
        /// <returns>A Task that resolves to true if the email was sent successfully, and false otherwise.</returns>
        Task<bool> SendEmail(string recipient, string subject, string body);
    }
}
