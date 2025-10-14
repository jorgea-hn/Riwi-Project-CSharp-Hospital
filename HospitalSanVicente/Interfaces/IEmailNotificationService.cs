using HospitalSanVicente.Model;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IEmailNotificationService
    {
        void CreateNotification(Appointment appointment, bool success);
        IEnumerable<EmailNotification> GetAllNotifications();
    }
}
