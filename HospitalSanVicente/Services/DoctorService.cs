using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;

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
                throw new Exception("El doctor ya está registrado.");
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
            return _doctorRepository.Update(doctor);
        }
    }
}
