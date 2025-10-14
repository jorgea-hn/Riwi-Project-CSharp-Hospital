using HospitalSanVicente.Model;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IDoctorService : ICrudService<Doctor, string>
    {
        // The base CRUD methods are inherited.
        // The service layer will use the document (string) as the identifier.

        IEnumerable<Doctor> FindDoctorsBySpecialty(string specialty);
    }
}
