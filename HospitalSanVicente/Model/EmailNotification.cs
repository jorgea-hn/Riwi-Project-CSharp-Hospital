using System;

namespace HospitalSanVicente.Model
{
    public class EmailNotification
    {
        public Guid Id { get; set; }
        public DateTime SentDate { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailStatus Status { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
