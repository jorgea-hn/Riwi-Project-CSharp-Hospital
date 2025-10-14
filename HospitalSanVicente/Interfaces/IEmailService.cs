namespace HospitalSanVicente.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string recipient, string subject, string body);
    }
}
