using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;

namespace HospitalSanVicente.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Patient RegisterPatient(Patient patient)
        {
            if (_patientRepository.GetByDocument(patient.Document) != null)
            {
                throw new Exception("El paciente ya está registrado.");
            }
            return _patientRepository.Create(patient);
        }

        public Patient FindPatientByDocument(string document)
        {
            return _patientRepository.GetByDocument(document);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll();
        }

        public Patient UpdatePatient(Patient patient)
        {
            return _patientRepository.Update(patient);
        }
    }
}
