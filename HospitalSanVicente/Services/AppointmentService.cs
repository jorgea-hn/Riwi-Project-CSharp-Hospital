using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailNotificationService _emailNotificationService;

        public AppointmentService(
            IAppointmentRepository appointmentRepository, 
            IPatientRepository patientRepository, 
            IDoctorRepository doctorRepository,
            IEmailService emailService,
            IEmailNotificationService emailNotificationService)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _emailService = emailService;
            _emailNotificationService = emailNotificationService;
        }

        public Appointment ScheduleAppointment(string patientDocument, string doctorDocument, DateTime date)
        {
            var patient = _patientRepository.GetByDocument(patientDocument);
            var doctor = _doctorRepository.GetByDocument(doctorDocument);

            if (patient == null || doctor == null)
            {
                throw new Exception("Patient or doctor not found.");
            }

            if (date.Hour < 7 || date.Hour >= 17)
            {
                throw new Exception("Appointments must be scheduled between 7am and 5pm.");
            }

            if (_appointmentRepository.GetByPatientAndDate(patient.Id, date) != null)
            {
                throw new Exception("The patient already has an appointment scheduled at that date and time.");
            }

            var doctorAppointments = _appointmentRepository.GetByDoctor(doctor.Id);
            if (doctorAppointments.Any(a => a.Status == AppointmentStatus.Scheduled && a.AppointmentDate > date.AddMinutes(-30) && a.AppointmentDate < date.AddMinutes(30)))
            {
                 throw new Exception("The doctor already has an appointment at that time.");
            }

            var appointment = new Appointment
            {
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                AppointmentDate = date,
                Status = AppointmentStatus.Scheduled
            };

            var createdAppointment = _appointmentRepository.Create(appointment);

            // Load navigation properties for notification services
            createdAppointment.Patient = patient;
            createdAppointment.Doctor = doctor;

            // Send confirmation email and record the notification
            bool emailSent = _emailService.SendEmail(
                createdAppointment.Patient.Email, 
                "Your appointment has been scheduled", 
                $"Hello {createdAppointment.Patient.Name}, your appointment with Dr. {createdAppointment.Doctor.Name} on {createdAppointment.AppointmentDate:yyyy-MM-dd HH:mm} has been successfully scheduled."
            );
            _emailNotificationService.CreateNotification(createdAppointment, emailSent);

            return createdAppointment;
        }

        public Appointment CancelAppointment(string patientDocument, DateTime date)
        {
            var patient = _patientRepository.GetByDocument(patientDocument);
            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            var appointment = _appointmentRepository.GetByPatientAndDate(patient.Id, date);

            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }

            appointment.Status = AppointmentStatus.Canceled;
            _appointmentRepository.Update(appointment);

            return appointment;
        }

        public Appointment MarkAppointmentAsAttended(string doctorDocument, string patientDocument, DateTime date)
        {
            var doctor = _doctorRepository.GetByDocument(doctorDocument);
            var patient = _patientRepository.GetByDocument(patientDocument);

            if (doctor == null || patient == null)
            {
                throw new Exception("Doctor or patient not found.");
            }

            var appointment = _appointmentRepository.GetByPatientAndDate(patient.Id, date);

            if (appointment == null || appointment.DoctorId != doctor.Id)
            {
                throw new Exception("Appointment not found.");
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
                throw new Exception("Patient not found.");
            }
            return _appointmentRepository.GetByPatient(patient.Id);
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(string doctorDocument)
        {
            var doctor = _doctorRepository.GetByDocument(doctorDocument);
            if (doctor == null)
            {
                throw new Exception("Doctor not found.");
            }
            return _appointmentRepository.GetByDoctor(doctor.Id);
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAll();
        }
    }
}
