using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();

        public Doctor Create(Doctor entity)
        {
            entity.Id = Guid.NewGuid();
            _doctors.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var doctor = GetById(id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctors;
        }

        public Doctor GetByDocument(string document)
        {
            return _doctors.FirstOrDefault(d => d.Document == document);
        }

        public IEnumerable<Doctor> GetBySpecialty(string specialty)
        {
            return _doctors.Where(d => d.Specialty.Equals(specialty, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Doctor GetById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public Doctor Update(Doctor entity)
        {
            var doctor = GetById(entity.Id);
            if (doctor != null)
            {
                doctor.Name = entity.Name;
                doctor.Document = entity.Document;
                doctor.Specialty = entity.Specialty;
            }
            return doctor;
        }
    }
}
