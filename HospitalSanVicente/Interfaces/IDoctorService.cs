using HospitalSanVicente.Model;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IDoctorService
    {
        Doctor RegisterDoctor(Doctor doctor);
        Doctor FindDoctorByDocument(string document);
        IEnumerable<Doctor> GetAllDoctors();
        IEnumerable<Doctor> FindDoctorsBySpecialty(string specialty);
        Doctor UpdateDoctor(Doctor doctor);
    }
}
