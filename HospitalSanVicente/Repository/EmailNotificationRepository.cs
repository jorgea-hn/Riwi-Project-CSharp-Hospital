using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Repository
{
    public class EmailNotificationRepository : IEmailNotificationRepository
    {
        private readonly List<EmailNotification> _notifications = new List<EmailNotification>();

        public EmailNotification Create(EmailNotification notification)
        {
            notification.Id = System.Guid.NewGuid();
            _notifications.Add(notification);
            return notification;
        }

        public IEnumerable<EmailNotification> GetAll()
        {
            return _notifications.ToList();
        }
    }
}
