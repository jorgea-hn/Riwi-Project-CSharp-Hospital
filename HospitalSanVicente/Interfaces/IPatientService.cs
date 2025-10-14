using HospitalSanVicente.Model;

namespace HospitalSanVicente.Interfaces
{
    public interface IPatientService : ICrudService<Patient, string>
    {
        // The base CRUD methods are inherited.
        // The service layer will use the document (string) as the identifier.

        // We can add patient-specific methods here if needed in the future.
    }
}
