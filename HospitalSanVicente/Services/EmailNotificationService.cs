using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;

namespace HospitalSanVicente.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IEmailNotificationRepository _notificationRepository;

        public EmailNotificationService(IEmailNotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void CreateNotification(Appointment appointment, bool success)
        {
            var notification = new EmailNotification
            {
                SentDate = DateTime.UtcNow,
                Recipient = appointment.Patient.Email,
                Subject = "Your appointment has been scheduled",
                Body = $"Hello {appointment.Patient.Name}, your appointment with Dr. {appointment.Doctor.Name} on {appointment.AppointmentDate:yyyy-MM-dd HH:mm} has been successfully scheduled.",
                Status = success ? EmailStatus.Sent : EmailStatus.Failed,
                AppointmentId = appointment.Id
            };

            _notificationRepository.Create(notification);
        }

        public IEnumerable<EmailNotification> GetAllNotifications()
        {
            return _notificationRepository.GetAll();
        }
    }
}
