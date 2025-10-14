using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;

namespace HospitalSanVicente.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEmailService _emailService;

        public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IEmailService emailService)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _emailService = emailService;
        }

        public Appointment ScheduleAppointment(Guid patientId, Guid doctorId, DateTime date)
        {
            var patient = _patientRepository.GetById(patientId);
            var doctor = _doctorRepository.GetById(doctorId);

            if (patient == null || doctor == null)
            {
                throw new Exception("Paciente o doctor no encontrado.");
            }

            if (date.Hour < 7 || date.Hour >= 17)
            {
                throw new Exception("La cita debe ser programada entre las 7am y las 5pm.");
            }

            if (_appointmentRepository.GetByPatientAndDate(patientId, date) != null)
            {
                throw new Exception("El paciente ya tiene una cita programada en esa fecha y hora.");
            }

            var appointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = date,
                Status = AppointmentStatus.Scheduled
            };

            var createdAppointment = _appointmentRepository.Create(appointment);

            _emailService.SendEmail(patient.Email, "Cita Programada", $"Su cita ha sido programada para el {date} con el doctor {doctor.Name}.");

            return createdAppointment;
        }

        public Appointment CancelAppointment(Guid patientId, DateTime date)
        {
            var appointment = _appointmentRepository.GetByPatientAndDate(patientId, date);

            if (appointment == null)
            {
                throw new Exception("Cita no encontrada.");
            }

            appointment.Status = AppointmentStatus.Canceled;
            _appointmentRepository.Update(appointment);

            var patient = _patientRepository.GetById(patientId);
            _emailService.SendEmail(patient.Email, "Cita Cancelada", $"Su cita del {date} ha sido cancelada.");

            return appointment;
        }

        public Appointment MarkAppointmentAsAttended(Guid doctorId, Guid patientId, DateTime date)
        {
            var appointment = _appointmentRepository.GetByPatientAndDate(patientId, date);

            if (appointment == null || appointment.DoctorId != doctorId)
            {
                throw new Exception("Cita no encontrada.");
            }

            appointment.Status = AppointmentStatus.Attended;
            _appointmentRepository.Update(appointment);

            return appointment;
        }

        public IEnumerable<Appointment> GetAppointmentsByPatient(Guid patientId)
        {
            return _appointmentRepository.GetByPatient(patientId);
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(Guid doctorId)
        {
            return _appointmentRepository.GetByDoctor(doctorId);
        }
    }
}
