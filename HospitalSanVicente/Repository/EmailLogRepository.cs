using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Repository
{
    public class EmailLogRepository : IEmailLogRepository
    {
        private readonly List<EmailLog> _logs = new List<EmailLog>();

        public EmailLog Create(EmailLog entity)
        {
            entity.Id = Guid.NewGuid();
            _logs.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var log = GetById(id);
            if (log != null)
            {
                _logs.Remove(log);
            }
        }

        public IEnumerable<EmailLog> GetAll()
        {
            return _logs;
        }

        public EmailLog GetById(Guid id)
        {
            return _logs.FirstOrDefault(l => l.Id == id);
        }

        public EmailLog Update(EmailLog entity)
        {
            var log = GetById(entity.Id);
            if (log != null)
            {
                log.To = entity.To;
                log.Subject = entity.Subject;
                log.Body = entity.Body;
                log.SentDate = entity.SentDate;
            }
            return log;
        }
    }
}
