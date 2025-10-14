using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Doctor RegisterDoctor(Doctor doctor)
        {
            if (_doctorRepository.GetByDocument(doctor.Document) != null)
            {
                throw new Exception("A doctor with this document ID is already registered.");
            }

            var existingDoctors = _doctorRepository.GetAll();
            if (existingDoctors.Any(d => d.Name.Equals(doctor.Name, StringComparison.OrdinalIgnoreCase) && d.Specialty.Equals(doctor.Specialty, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("A doctor with the same name and specialty already exists.");
            }

            return _doctorRepository.Create(doctor);
        }

        public Doctor FindDoctorByDocument(string document)
        {
            return _doctorRepository.GetByDocument(document);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAll();
        }

        public IEnumerable<Doctor> FindDoctorsBySpecialty(string specialty)
        {
            return _doctorRepository.GetBySpecialty(specialty);
        }

        public Doctor UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = _doctorRepository.GetByDocument(doctor.Document);
            if (existingDoctor != null && existingDoctor.Id != doctor.Id)
            {
                throw new Exception("Another doctor with the same document ID already exists.");
            }

            var allDoctors = _doctorRepository.GetAll();
            if (allDoctors.Any(d => d.Name.Equals(doctor.Name, StringComparison.OrdinalIgnoreCase) && d.Specialty.Equals(doctor.Specialty, StringComparison.OrdinalIgnoreCase) && d.Id != doctor.Id))
            {
                throw new Exception("Another doctor with the same name and specialty already exists.");
            }

            return _doctorRepository.Update(doctor);
        }
    }
}
