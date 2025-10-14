using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients = new List<Patient>();

        public Patient Create(Patient entity)
        {
            entity.Id = Guid.NewGuid();
            _patients.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var patient = GetById(id);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetByDocument(string document)
        {
            return _patients.FirstOrDefault(p => p.Document == document);
        }

        public Patient GetById(Guid id)
        {
            return _patients.FirstOrDefault(p => p.Id == id);
        }

        public Patient Update(Patient entity)
        {
            var patient = GetById(entity.Id);
            if (patient != null)
            {
                patient.Name = entity.Name;
                patient.Document = entity.Document;
                patient.Email = entity.Email;
            }
            return patient;
        }
    }
}
