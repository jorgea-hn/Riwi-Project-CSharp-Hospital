using HospitalSanVicente.Model;

namespace HospitalSanVicente.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Doctor GetByDocument(string document);
    }
}
