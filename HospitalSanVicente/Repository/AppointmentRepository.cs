using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly List<Appointment> _appointments = new List<Appointment>();
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentRepository(IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public Appointment Create(Appointment entity)
        {
            entity.Id = Guid.NewGuid();
            _appointments.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var appointment = GetById(id);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
            }
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _appointments.Select(a =>
            {
                a.Patient = _patientRepository.GetById(a.PatientId);
                a.Doctor = _doctorRepository.GetById(a.DoctorId);
                return a;
            });
        }

        public IEnumerable<Appointment> GetByDoctor(Guid doctorId)
        {
            return _appointments.Where(a => a.DoctorId == doctorId).Select(a =>
            {
                a.Patient = _patientRepository.GetById(a.PatientId);
                a.Doctor = _doctorRepository.GetById(a.DoctorId);
                return a;
            }).ToList();
        }

        public Appointment GetById(Guid id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                appointment.Patient = _patientRepository.GetById(appointment.PatientId);
                appointment.Doctor = _doctorRepository.GetById(appointment.DoctorId);
            }
            return appointment;
        }

        public IEnumerable<Appointment> GetByPatient(Guid patientId)
        {
            return _appointments.Where(a => a.PatientId == patientId).Select(a =>
            {
                a.Patient = _patientRepository.GetById(a.PatientId);
                a.Doctor = _doctorRepository.GetById(a.DoctorId);
                return a;
            }).ToList();
        }

        public Appointment GetByPatientAndDate(Guid patientId, DateTime date)
        {
            var appointment = _appointments.FirstOrDefault(a => a.PatientId == patientId && a.AppointmentDate == date);
            if (appointment != null)
            {
                appointment.Patient = _patientRepository.GetById(appointment.PatientId);
                appointment.Doctor = _doctorRepository.GetById(appointment.DoctorId);
            }
            return appointment;
        }

        public Appointment Update(Appointment entity)
        {
            var appointment = GetById(entity.Id);
            if (appointment != null)
            {
                appointment.PatientId = entity.PatientId;
                appointment.DoctorId = entity.DoctorId;
                appointment.AppointmentDate = entity.AppointmentDate;
                appointment.Status = entity.Status;
            }
            return appointment;
        }
    }
}
