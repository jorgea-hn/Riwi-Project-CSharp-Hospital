using System.Threading.Tasks;

namespace HospitalSanVicente.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string recipient, string subject, string body);
    }
}
