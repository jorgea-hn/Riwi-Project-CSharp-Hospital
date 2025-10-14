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
        // private readonly IEmailService _emailService;

        public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            // _emailService = emailService;
        }

        public Appointment ScheduleAppointment(string patientDocument, string doctorDocument, DateTime date)
        {
            var patient = _patientRepository.GetByDocument(patientDocument);
            var doctor = _doctorRepository.GetByDocument(doctorDocument);

            if (patient == null || doctor == null)
            {
                throw new Exception("Paciente o doctor no encontrado.");
            }

            if (date.Hour < 7 || date.Hour >= 17)
            {
                throw new Exception("La cita debe ser programada entre las 7am y las 5pm.");
            }

            if (_appointmentRepository.GetByPatientAndDate(patient.Id, date) != null)
            {
                throw new Exception("El paciente ya tiene una cita programada en esa fecha y hora.");
            }

            var appointment = new Appointment
            {
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                AppointmentDate = date,
                Status = AppointmentStatus.Scheduled
            };

            var createdAppointment = _appointmentRepository.Create(appointment);

            // _emailService.SendEmail(patient.Email, "Cita Programada", $"Su cita ha sido programada para el {date} con el doctor {doctor.Name}.");

            return createdAppointment;
        }

        public Appointment CancelAppointment(string patientDocument, DateTime date)
        {
            var patient = _patientRepository.GetByDocument(patientDocument);
            if (patient == null)
            {
                throw new Exception("Paciente no encontrado.");
            }

            var appointment = _appointmentRepository.GetByPatientAndDate(patient.Id, date);

            if (appointment == null)
            {
                throw new Exception("Cita no encontrada.");
            }

            appointment.Status = AppointmentStatus.Canceled;
            _appointmentRepository.Update(appointment);

            // _emailService.SendEmail(patient.Email, "Cita Cancelada", $"Su cita del {date} ha sido cancelada.");

            return appointment;
        }

        public Appointment MarkAppointmentAsAttended(string doctorDocument, string patientDocument, DateTime date)
        {
            var doctor = _doctorRepository.GetByDocument(doctorDocument);
            var patient = _patientRepository.GetByDocument(patientDocument);

            if (doctor == null || patient == null)
            {
                throw new Exception("Doctor o paciente no encontrado.");
            }

            var appointment = _appointmentRepository.GetByPatientAndDate(patient.Id, date);

            if (appointment == null || appointment.DoctorId != doctor.Id)
            {
                throw new Exception("Cita no encontrada.");
            }

            appointment.Status = AppointmentStatus.Attended;
            _appointmentRepository.Update(appointment);

            return appointment;
        }

        public IEnumerable<Appointment> GetAppointmentsByPatient(string patientDocument)
        {
            var patient = _patientRepository.GetByDocument(patientDocument);
            if (patient == null)
            {
                throw new Exception("Paciente no encontrado.");
            }
            return _appointmentRepository.GetByPatient(patient.Id);
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(string doctorDocument)
        {
            var doctor = _doctorRepository.GetByDocument(doctorDocument);
            if (doctor == null)
            {
                throw new Exception("Doctor no encontrado.");
            }
            return _appointmentRepository.GetByDoctor(doctor.Id);
        }
    }
}
