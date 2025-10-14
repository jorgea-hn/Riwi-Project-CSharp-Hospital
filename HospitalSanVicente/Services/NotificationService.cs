using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Linq;

namespace HospitalSanVicente.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmailService _emailService;

        public NotificationService(IAppointmentRepository appointmentRepository, IEmailService emailService)
        {
            _appointmentRepository = appointmentRepository;
            _emailService = emailService;
        }

        public void SendAppointmentReminders()
        {
            var tomorrow = DateTime.Now.AddDays(1).Date;
            var upcomingAppointments = _appointmentRepository.GetAll()
                .Where(a => a.AppointmentDate.Date == tomorrow && a.Status == AppointmentStatus.Scheduled);

            foreach (var appointment in upcomingAppointments)
            {
                var patient = appointment.Patient;
                var doctor = appointment.Doctor;
                var subject = "Recordatorio de Cita";
                var body = $"Hola {patient.Name}, le recordamos su cita para mañana a las {appointment.AppointmentDate:HH:mm} con el doctor {doctor.Name}.";
                _emailService.SendEmail(patient.Email, subject, body);
            }
        }
    }
}
