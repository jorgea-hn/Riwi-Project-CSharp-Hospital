using HospitalSanVicente.Interfaces;
using System;

namespace HospitalSanVicente.UI
{
    public class EmailNotificationUI
    {
        private readonly IEmailNotificationService _emailNotificationService;

        public EmailNotificationUI(IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
        }

        public void ShowNotificationHistory()
        {
            Console.WriteLine("\n--- Email Notification History ---");
            var notifications = _emailNotificationService.GetAllNotifications();
            
            if (notifications == null || !notifications.Any())
            {
                Console.WriteLine("No notifications found.");
                return;
            }

            foreach (var notification in notifications)
            {
                Console.WriteLine($"- ID: {notification.Id}");
                Console.WriteLine($"  Date: {notification.SentDate}");
                Console.WriteLine($"  To: {notification.Recipient}");
                Console.WriteLine($"  Subject: {notification.Subject}");
                Console.WriteLine($"  Status: {notification.Status}");
                Console.WriteLine($"  Appointment ID: {notification.AppointmentId}");
                Console.WriteLine("---");
            }
        }
    }
}
