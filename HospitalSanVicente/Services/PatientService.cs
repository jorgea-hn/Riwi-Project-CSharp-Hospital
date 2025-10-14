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

        public Patient Create(Patient patient)
        {
            if (_patientRepository.GetByDocument(patient.Document) != null)
            {
                throw new Exception("A patient with this document ID is already registered.");
            }
            return _patientRepository.Create(patient);
        }

        public Patient Get(string document)
        {
            return _patientRepository.GetByDocument(document);
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patientRepository.GetAll();
        }

        public Patient Update(Patient patient)
        {
            var existingPatient = _patientRepository.GetByDocument(patient.Document);
            if (existingPatient != null && existingPatient.Id != patient.Id)
            {
                throw new Exception("Another patient with the same document ID already exists.");
            }
            return _patientRepository.Update(patient);
        }

        public void Delete(string document)
        {
            var patientToDelete = _patientRepository.GetByDocument(document);
            if (patientToDelete == null)
            {
                throw new Exception("Patient not found.");
            }
            _patientRepository.Delete(patientToDelete.Id);
        }
    }
}
