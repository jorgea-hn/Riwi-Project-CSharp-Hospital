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
                throw new Exception("A patient with this document ID is already registered.");
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
            var existingPatient = _patientRepository.GetByDocument(patient.Document);
            if (existingPatient != null && existingPatient.Id != patient.Id)
            {
                throw new Exception("Another patient with the same document ID already exists.");
            }
            return _patientRepository.Update(patient);
        }
    }
}
