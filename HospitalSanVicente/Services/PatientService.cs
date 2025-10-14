using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;

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

        public Patient GetPatientByDocument(string document)
        {
            return _patientRepository.GetByDocument(document);
        }
    }
}
