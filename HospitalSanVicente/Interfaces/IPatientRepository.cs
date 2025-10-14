using HospitalSanVicente.Model;

namespace HospitalSanVicente.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient GetByDocument(string document);
    }
}
