using System;

namespace HospitalSanVicente.Model
{
    public class EmailLog
    {
        public Guid Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
    }
}
