using HospitalSanVicente.Model;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IEmailNotificationRepository
    {
        EmailNotification Create(EmailNotification notification);
        IEnumerable<EmailNotification> GetAll();
    }
}
