using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;

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

        public Doctor GetDoctorByDocument(string document)
        {
            return _doctorRepository.GetByDocument(document);
        }
    }
}
