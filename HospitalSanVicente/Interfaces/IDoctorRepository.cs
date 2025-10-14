using HospitalSanVicente.Model;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Doctor GetByDocument(string document);
        IEnumerable<Doctor> GetBySpecialty(string specialty);
    }
}
